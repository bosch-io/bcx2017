#!/usr/bin/perl

use strict;
use warnings;

use Getopt::Long;
use LWP;
use JSON qw(encode_json);

my $hono_url_template = "http://hono.bosch-iot-suite.com:8080/telemetry/bcx/";
my $hono_url;

my $device_id;
my $ua;
my %prev_stat;
my %stat;

init();
while (1) {
    sleep(10);
    publish(gather_data());
}


sub gather_data {
    my %data;

    $data{freq_cpu} = vcgencmd('measure_clock', 'arm');
    $data{voltage_core} = vcgencmd('measure_volts', 'core');
    $data{voltage_sdram_c} = vcgencmd('measure_volts', 'sdram_c');
    $data{voltage_sdram_i} = vcgencmd('measure_volts', 'sdram_i');
    $data{voltage_sdram_p} = vcgencmd('measure_volts', 'sdram_p');
    $data{temp} = vcgencmd('measure_temp');

    my %meminfo = read_meminfo();
    $data{memory_total} = $meminfo{MemTotal};
    $data{memory_free} = $meminfo{MemFree};

    my %stat = read_cpu_stat(%prev_stat);
    %prev_stat = %stat;

    $data{processes} = $stat{processes} + 0.0;
    $data{processes_running} = $stat{procs_running} + 0.0;
    $data{processes_blocked} = $stat{procs_blocked} + 0.0;

#    foreach my $cpu (grep /^cpu(\d*)/, sort keys %stat) {
#        my $id = $1 || 0;
#        $data{cpu}->[$id]->{usage} = $stat{"cpu$cpu"}->{usage};
#    }

    foreach my $cpu (grep /^cpu\d*/, sort keys %stat) {
        if ($cpu =~ /(\d+)/) {
            $data{cpu_usage_individual}->[$1] = $stat{$cpu}->{usage};
        } else {
            $data{cpu_usage_overall} = $stat{$cpu}->{usage};
        }
    }
    return \%data;
}

sub publish {
    my ($data) = @_;

    my $things = {
        topic => "bcx/rpi" . $device_id . "/things/twin/commands/modify",
        path => "features",
        value => {
            cpu => {
                properties => {
                    frequency => $data->{freq_cpu},
                    voltage => $data->{voltage_core},
                    usage => $data->{cpu_usage_overall},
                    core_usage => $data->{cpu_usage_individual},
                    temp => $data->{temp}
                }
            },
            sdram => {
                properties => {
                    voltage_c => $data->{voltage_sdram_c},
                    voltage_i => $data->{voltage_sdram_i},
                    voltage_p => $data->{voltage_sdram_p}
                }
            },
            memory => {
                properties => {
                    total => $data->{memory_total},
                    free => $data->{memory_free}
                }
            },
            processes => {
                properties => {
                        total => $data->{processes},
                        running => $data->{processes_running},
                        blocked => $data->{processes_blocked} 
                }
            }
        }
    };
    my $output = encode_json($things);

#    my $nice = JSON->new->utf8(1)->pretty(1)->encode($things);
#    print "$nice\n";

    my $response = $ua->put($hono_url, "Content-Type" => "application/json", "Content" => \$output);
    print "[PUT] " . $response->status_line . "\n";
}


sub vcgencmd {
    my $result = `/opt/vc/bin/vcgencmd @_`;
    if ($result =~ /([^=]+)=(.+)/) {
        # for numeric data, remove trailing non-digit characters (e.g. '1.2250V' => '1.2250')
        ($result = $2) =~ s/(\d+\.?\d+?)\D+/$1/;
        return $result + 0.0;
    } else {
        die "Unable to run /opt/vc/bin/vcgencmd @_: $result";
    }
}

sub read_cpuinfo {
    my %cpuinfo;

    open(my $fh, '<', '/proc/cpuinfo') or
        die "Cannot read /proc/cpuinfo: $!\n";
    while (<$fh>) {
        $cpuinfo{$1} = $2 if (/(\S+?)\s+:\s+(.+)/);
    }
    close($fh);

    return %cpuinfo;
}

sub read_meminfo {
    my %meminfo;

    open(my $fh, '<', '/proc/meminfo') or
        die "Cannot read /proc/meminfo: $!\n";
    while (<$fh>) {
        $meminfo{$1} = $2 + 0.0 if (/([^:]+):\D+(\d+)/);
    }
    close($fh);

    return %meminfo;
}

sub read_cpu_stat {
    my (%prevStat) = @_;
    my (%stat);

    open(my $fh, '<', '/proc/stat') or
        die "Cannot read /proc/stat: $!\n";
    while (<$fh>) {
        if (/^(\S+)\s+(.+)$/) {
            my ($key, $value) = ($1, $2);

            if ($key =~ /^cpu\d*/) {
                my @values = split /\s+/, $value;

                my $sum;
                map { $sum += $_ } @values;

                $stat{$key} = {
                    'user' => $values[0],
                    'nice' => $values[1],
                    'system' => $values[2],
                    'idle' => $values[3],
                    'iowait' => $values[4],
                    'irq' => $values[5],
                    'softirq' => $values[6],
                    'sum' => $sum,
                    'sum_idle' => $values[3] + $values[4]
                };

                if (exists $prevStat{$key}) {
                    my $sumDiff = ($stat{$key}->{'sum'} - $prevStat{$key}->{'sum'});
                    my $sumIdleDiff = ($stat{$key}->{'sum_idle'} - $prevStat{$key}->{'sum_idle'});

                    my $cpu_usage = ($sumDiff - $sumIdleDiff) / $sumDiff;
                    $stat{$key}->{'usage'} = $cpu_usage;

#                    print "$key: $cpu_usage\n";
                }

            } else {
                $stat{$key} = $value;
            }
        }
    }
    close($fh);
    return %stat;    
}


sub init {
    my %cpuinfo = read_cpuinfo();
    %prev_stat = read_cpu_stat();

    die "Unable to find out serial number from /proc/cpuinfo\n"
        unless exists $cpuinfo{Serial};
    $device_id = "rpi.$cpuinfo{Serial}";
    $hono_url = "$hono_url_template$device_id";

    print "Publishing to $hono_url\n";
    $ua = LWP::UserAgent->new(agent => 'rpi2hono.pl');
}

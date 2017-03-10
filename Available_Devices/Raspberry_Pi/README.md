![Available in Eclipse Hono](images/shields/Eclipse_Hono-available-green.svg)
![Available in Bosch IoT Things](images/shields/Bosch_IoT_Things-available-green.svg)
![Not available in Bosch IoT Developer Console](images/shields/Bosch_IoT_Developer_Console-not_available-red.svg)
![You can work directly on this device](images/shields/Hacking_on_device-available-green.svg)

# Raspberry Pi

We have a number of Raspberry Pi 3 available that you can use for your projects.

The example Perl script supplied calculates CPU usage and posts CPU usage, temperature, process and memory status as telemetry into Hono. 

## Installation

The script is pre-installed on all Raspberry Pis supplied and is started at boot time.

Use `killall rpi2hono.pl` to quit the running script, then edit the script as you please and start it again with `perl rpi2hono.pl`. If you want to modify the boot time behaviour, use `sudo nano /etc/rc.local` to modify the startup script. The default entry is

```
su pi -c /home/pi/bin/rpi2hono.pl > /home/pi/rpi2hono.log &
```

## Available devices

The Raspberry Pi are identified through their serial number. The serial number is printed when the script starts, e.g. 

````
Publishing to http://hono.bosch-iot-suite.com:8080/telemetry/bcx/rpi.00000000c2f4b9e9
```

### Example telemetry data

```JSON
{
   "value" : {
      "processes" : {
         "properties" : {
            "running" : 1,
            "blocked" : 0,
            "total" : 1639
         }
      },
      "sdram" : {
         "properties" : {
            "voltage_p" : 1.225,
            "voltage_i" : 1.2,
            "voltage_c" : 1.2
         }
      },
      "memory" : {
         "properties" : {
            "free" : 598384,
            "total" : 947732
         }
      },
      "cpu" : {
         "properties" : {
            "usage" : 0.0037359900373599,
            "temp" : 51.5,
            "frequency" : 600000000,
            "voltage" : 1.2,
            "core_usage" : [
               0.005,
               0.00496031746031746,
               0.00398803589232303,
               0.00099601593625498
            ]
         }
      }
   },
   "path" : "features",
   "topic" : "bcx/rpirpi.00000000c2f4b9e9/things/twin/commands/modify"
}
```


# Go Receiver

The example program `hono_receiver.go` connects to the Hono server and registers telemetry and event receivers. Events received from the Hono server are parsed and printed out to the command line.

## Usage

In order to run this program, you will need to install the [Qpid Proton](https://qpid.apache.org/proton/) library.

After successful installation, run the program as follows:

```
go run hono-receiver.go --host myhostname --port 15672 --user me --pass secret
```


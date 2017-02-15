# Node.js Receiver

The example script `Hono_Receiver.js` connects to the Hono server and registers telemetry and event receivers. Events received from the Hono server are parsed and printed out to the command line.

## Usage

The script is based on the [rhea amqp library](https://github.com/grs/rhea). Based on an existing Node.js installation, you can install the rhea library, and run the example script as follows:

```
npm install rhea
npm install minimist

node Hono_Receiver.js --host myhostname --port 15672 --user me --pass secret

```

In order to see debug output, set the `DEBUG` variable accordingly before running the script:

```
DEBUG=rhea* node Hono_Receiver.js 
```

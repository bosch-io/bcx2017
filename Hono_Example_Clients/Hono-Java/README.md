# Hono Java Clients (Consumer and Sender)

Simple Java clients for Hono, description can be found in the [Hono User-guide](https://www.eclipse.org/hono/user-guide/).

## Consumer

### Clone project

**If you have already cloned this repository please skip this step.**

Please clone the git project and change to the current directory

```shell
$ git clone https://github.com/bsinno/bcx2017.git
$ cd Hono_Example_Clients/Hono-Java
```

### Build it
Build the maven based project by

```shell
$ cd honoTelemetryDownstreamConsumer
$ mvn clean compile
```

### Execute it
You need the credentials that were given to you. You then execute the consumer by

```shell
$ mvn exec:java -Dexec.mainClass="org.eclipse.hono.App" -Dexec.args="--user <user> --password <password>"
```

Direct link to documentation of consumer: [Documentation](https://www.eclipse.org/hono/user-guide/java_client_consumer/)

## Sender
For the sender, please ask one of the Hack coaches.

Direct link to documentation of sender: [Documentation](https://www.eclipse.org/hono/user-guide/java_client_sender/)
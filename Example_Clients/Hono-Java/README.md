# Hono Java Clients (Consumer and Sender)

Here you find the Maven projects described on the [User-guide](https://www.eclipse.org/hono/user-guide/) of the Hono website.

## [Consumer](https://www.eclipse.org/hono/user-guide/java_client_consumer/)

### Clone project
Please clone the git project

```shell
$ git clone https://github.com/bsinno/bcx2017.git
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


## [Sender](https://www.eclipse.org/hono/user-guide/java_client_sender/)
For the sender, please ask one of the Hack coaches.

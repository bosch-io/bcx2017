# Welcome to Bosch Connected Experience 2017!

In this repository, you will find all the information needed to get started with the device hub we set up this hackathon.

# Introduction

We have tried to get you up and running as quickly as possible. For that purpose we have selected an array of common sensors and boards as well as some Bosch devices and have connected them to a *Messaging Hub* based on [Eclipse Hono](https://www.eclipse.org/hono) running on the internet. We have also tried to make sure that you can easily access the data and events produced by these devices using multiple APIs.

The following diagram provides an overview of the setup.

![Overview diagram](Overview.png "Overview diagram")

The following sections provide you with details regarding the different ways you can access the device data and use it in your project.

# Connected Devices

You can find information about the devices we have connected to Hono in the [Available_Devices/](Available_Devices/) directory.

# Consuming Telemetry Data & Events

The connected devices publish their telemetry data and events to an [Eclipse Hono](https://www.eclipse.org/hono/) instance accessible at `hono.bosch-iot-suite.com`.

You can consume the device data by connecting an AMQP 1.0 client to Hono and listening in on the `telemetry` or `event` addresses respectively. We have prepared some example client code using various programming languages in the [Example_Clients/](Example_Clients/) directory.

The Eclipse Hono website has further information on the [Telemetry API](https://www.eclipse.org/hono/api/Telemetry-API/) and the [Event API](https://www.eclipse.org/hono/api/Event-API/).

# Retrieving Historical Data

All telemetry data sent in from devices is also fed into an istance of the open source [InfluxDB](https://github.com/influxdata/influxdb) *Time Servies Database*.
 
Credentials for accessing the InfluxDB can be found on our passwords sheet.

Telemetry is fed into the `hono-telemetry` database, events are fed into the `hono-events` database.

You can use the admin console at http://bcx-workhorse.bosch-si.com:8083/ to try out your queries using the web UI
or run them from the command line using `curl`. The following sections provide some examples you can use as a starting point for your own experiments.

## Read out latest 10 telemetry data sets from the XDK with the MAC address 7C:EC:79:D3:33:82

```
# in admin console: select database "hono-telemetry"
SELECT * from xdk.7cec79d33382 ORDER BY time desc LIMIT 10
# from command line
curl -G 'http://bcx-workhorse.bosch-si.com:8086/query?pretty=true&u=USERNAME&p=PASSWORD' --data-urlencode "db=hono_telemetry" --data-urlencode "q=SELECT \"*\" FROM \"xdk.7cec79d33382\" ORDER BY time desc LIMIT 10"
````

## Read out latest entry from all Nexo nutrunners

```
# in admin console
select * from /nexo\..*/ order by time desc limit 1
# from command line
curl -G 'http://bcx-workhorse.bosch-si.com:8086/query?pretty=true&u=USERNAME&p=PASSWORD' --data-urlencode "db=hono_telemetry" --data-urlencode "q=SELECT \"*\" FROM \"/nexo\\..*/\" ORDER BY time desc LIMIT 1"
````

Further guidance can be found in the [Getting Started](https://docs.influxdata.com/influxdb/v1.2/introduction/getting_started/) section of the InfluxDB documentation and on the [InfluxDB API page](https://docs.influxdata.com/influxdb/v1.2/tools/api/) (see "query").



# Digital Twins

## What is [Bosch IoT Things](https://things.apps.bosch-iot-cloud.com/)?

Bosch IoT Things enables applications, cloud services, and devices to manage the data of their IoT assets in a simple, convenient, robust, and secure way. Solutions can store and update the data, properties, and relationships of your domain's assets and get notified of all relevant changes.

## Getting started with the HTTP API

An IoT developer can create, read, update and delete Things via the Bosch IoT Things HTTP API. 

To use the HTTP API you will need:

 * The header for authenticating your solution: ``x-cr-api-token: {apiToken}``
 * The header for authenticating the current user via Basic Auth: ``Authorization: Basic base64({username}:{password})``

The root resource of the Bosch IoT Things HTTP API is located at ``https://things.apps.bosch-iot-cloud.com/cr/1``.
All requests and responses are ``JSON`` based so please use ``application/json`` as the ``Content-Type`` for your 
requests.

### Search things
Let's see which Things your user is allowed to see. By default this request will not return more than 25 things.

> GET /search/things

You can further filter or limit the returned results, see the 
[HTTP API documentation](https://things.apps.bosch-iot-cloud.com/documentation/rest/#) for more information.

### Retrieve a Thing
Retrieve a single Thing by its Thing ID:

> GET /things/{thingId}

You can read only parts of a Thing by specifying the path inside the Thing via the URL path e.g. to read an 
attribute ``location`` use the following path:

> GET /things/{thingId}/attributes/location

### Modify a Thing

You can either update the whole Thing at once (attention, this overwrites all data of a Thing) or only parts of it 
e.g. its attributes or a single property value. 

To update the ``location`` attribute of a Thing use the following request:
> PUT /things/{thingId}/attributes/location

Example of JSON request body:
```json
{
  "longitude": -27.119444,
  "latitude" : -109.354722
}
```
### Further operations

For a complete list of available operations please refer to the 
[HTTP API documentation](https://things.apps.bosch-iot-cloud.com/documentation/rest/#).

In case you are familiar with [Postman](https://www.getpostman.com/) we also have prepared a 
[Postman Collection](https://github.com/bsinno/iot-things-examples/tree/master/postman-collection)
for you to quickly get started (tip: use the environment ``env_CRaaS-prod-environment.json`` to have the correct 
endpoint configured automatically).

## Java API

If you prefer a Java based approach you can also use the 
[Things Integration Client](https://cr.apps.bosch-iot-cloud.com/dokuwiki/doku.php?id=005_dev_guide:005_java_api:005_java_api) 
to access your Things and to subscribe for changes that are made to your Things.
To get started go to the 
[Hello World Example](https://things.apps.bosch-iot-cloud.com/dokuwiki/doku.php?id=005_dev_guide:tutorial:000_hello_world)
of using the Java Client.

## Further information
* Main entry point for 
[Bosch IoT Things documentation](https://things.apps.bosch-iot-cloud.com/dokuwiki/doku.php?id=start)
* Complete documentation of [Things HTTP API](https://cr.apps.bosch-iot-cloud.com/documentation/rest/#)
* Documentation of the 
[Things Integration Client](https://cr.apps.bosch-iot-cloud.com/dokuwiki/doku.php?id=005_dev_guide:005_java_api:005_java_api)
* [Code examples](https://github.com/bsinno/iot-things-examples) of using the Things Integration Client (Java)

# Developer Console

[to do] Documentation around Developer Console

# Connecting your own devices

See [Example_Devices](Example_Devices/).


[BCX Website](http://bcw.bosch-si.com/berlin/bcw-hackathon/) 

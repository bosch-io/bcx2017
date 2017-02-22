# Welcome to Bosch Connected Experience 2017!

In this repository, you will find all the information needed to get started with the device hub we set up this hackathon.

## Getting started

![Overview diagram](Overview.png "Overview diagram")

All available devices are described in the [Available_Devices/](Available_Devices/) directory.

## Receiving telemetry messages

The connected devices feed their telemetry data and events into a messaging hub based on [Eclipse Hono](https://www.eclipse.org/hono/).
You can receive the device data by connecting an AMQP client to Hono and listening in on the respective addresses. We have prepared some example
clients for you in various languages in the [Example_Clients/](Example_Clients/) directory.

The Eclipse Hono website has further information on the [Telemetry API](https://www.eclipse.org/hono/api/Telemetry-API/) and [Event API](https://www.eclipse.org/hono/api/Event-API/).

### Historical data

All telemetry data sent in from devices is also fed into [InfluxDB](https://github.com/influxdata/influxdb), an open-source time series database. 

[to do] Write example queries based on example below.

```
curl -G 'http://localhost:8086/query?pretty=true' --data-urlencode "db=mydb" --data-urlencode "q=SELECT \"value\" FROM \"cpu_load_short\" WHERE \"region\"='us-west'"

curl -G 'http://hono-test.bosch-iot-suite.com:8086/query?pretty=true' --data-urlencode "db=mydb" --data-urlencode "q=SELECT \"value\" FROM \"cpu_load_short\" WHERE \"region\"='us-west'"

select * from /.*/ order by time desc limit 1

http://hono-test.bosch-iot-suite.com:1880/latest
```


Further guidance can be found in the [Getting Started](https://docs.influxdata.com/influxdb/v1.2/introduction/getting_started/) section of the InfluxDB documentation.

## Digital Twins

[todo] Documenation around Bosch IoT Things

## Developer Console

[to do] Documentation around Developer Console

## Connecting your own devices

See [Example_Devices](Example_Devices/).


[BCX Website](http://bcw.bosch-si.com/berlin/bcw-hackathon/) 

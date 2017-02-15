# How can I look at historical device data?

All telemetry data sent in from devices is also fed into an istance of the open source [InfluxDB](https://github.com/influxdata/influxdb) *Time Series Database*. For that purpose, the JSON data structures are flattened into a list of fields (because that's what InfluxDB supports).

If you want to just quickly retrieve historical data without getting too much into InfluxDB, use these simple webservices we created for you:

List all devices with available telemetry data:
> http://bcx-workhorse.bosch-iot-suite.com/telemetry

List all devices with available event data:
> http://bcx-workhorse.bosch-iot-suite.com/events      

Get last ten telemetry data sets from device rrc.655997720 (one of our thermostats):
> http://bcx-workhorse.bosch-iot-suite.com/telemetry?deviceId=rrc.655997720&limit=10

Get last ten events from device esp8266.60019400998b
> http://bcx-workhorse.bosch-iot-suite.com/events?deviceId=esp8266.60019400998b&limit=10

Interested to know more and have more flexible options? Read on.

Telemetry is fed into the `bcx2017_telemetry` database, events are fed into the `bcx2017_events` database. Credentials for accessing the InfluxDB can be found on our passwords sheet.

In order to retrieve data, you need to write queries in the [InfluxQL query language](https://docs.influxdata.com/influxdb/v1.2/query_language/) and execute them via e.g. `curl`, the InfluxDB CLI tools, or another InfluxDB client (e.g. Node-RED InfluxDB nodes).

Here are some examples you can use as a starting point for your own experiments.

## Read out latest 10 telemetry data sets from one of the Bosch wall thermostats

> SELECT * from "rrc.655997720" ORDER BY time desc LIMIT 10

```
curl -G -u USER:PASS --data-urlencode "db=bcx2017_telemetry" http://bcx-workhorse.bosch-iot-suite.com:8086/query --data-urlencode 'q=SELECT * from "rrc.655997720" order by time desc limit 10' --data-urlencode pretty=true
```

## Read out latest entry from all XDKs

> select * from /xdk\..*/ order by time desc limit 1

```
curl -G -u USER:PASS --data-urlencode "db=bcx2017_telemetry" http://bcx-workhorse.bosch-iot-suite.com:8086/query --data-urlencode 'q=SELECT * from /xdk\..*/ order by time desc limit 10' --data-urlencode pretty=true
```

Further guidance can be found in the [Getting Started](https://docs.influxdata.com/influxdb/v1.2/introduction/getting_started/) section of the InfluxDB documentation and on the [InfluxDB API page](https://docs.influxdata.com/influxdb/v1.2/tools/api/) (see "query").

**Note**: If you add new measurement values, InfluxDB will automatically extend the database schema based on the type of the values you submitted.
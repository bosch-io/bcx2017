# Actility LoRaWAN Adapter for Hono

This adapter exposes an endpoint which can be called by the Actility ThingPark Backend. 

#### Endpoints

##### Performing an Uplink from Thingpark
(POST)	 http://hono.bosch-iot-suite.com:10060/bcx?LrnDevEui=...etc..

##### Status and Stats of the Actility Adapter
(GET)	http://hono.bosch-iot-suite.com:10060/status

##### Last Incoming Payload for Device
(GET)	http://hono.bosch-iot-suite.com:10060/status/bcx/[deviceId]/incoming

##### Last Twin Payload for Device
(GET)	http://hono.bosch-iot-suite.com:10060/status/bcx/[deviceId]/twin

## Setting Up

### Pre-requisites

1. You'll need to register your device id on hono via Hono's registration endpoint

2. You can do this via Hono's REST Adapter endpoint for Registration. 
```curl -X POST -i -d 'device_id= lorawan.(deviceEui)' http://hono.bosch-iot-suite:8080/registration/DEFAULT_TENANT```)

3. Devices should have a prefix of "lorawan."

### Configuring the Actility Backend

-- TODO --

## Usage

### Uplink

If you have configured your device's Callback on the Actility Backend, the call to the adapter should automatically be made and you need not do anything further.

You can then just subscribe to any notifications to your device via Hono or IoT Things.

The adapter supports either application/json or application/xml format.

**NOTE: The Device ID for devices via this adapter will be appended with "lorawan."**  
**Example: lorawan.mydevice**  

## Payload Format
### Actility ThingPark XML
```
<?xml version="1.0" encoding="UTF-8"?>
<DevEUI_uplink xmlns="http://uri.actility.com/lora">
    <Time>2015-07-09T16:06:38.49+02:00</Time>
    <DevEUI>000000000F1D8693</DevEUI>
    <FPort>2</FPort>
    <FCntUp>7011</FCntUp>
    <ADRbit>1</ADRbit>
    <ACKbit>1</ACKbit>
    <MType>4</MType>
    <FCntDn>11</FCntDn>
    <payload_hex>0027bd00</payload_hex>
    <mic_hex>38e7a3b9</mic_hex>
    <Lrcid>00000065</Lrcid>
    <LrrRSSI>-60.000000</LrrRSSI>
    <LrrSNR>9.750000</LrrSNR>
    <SpFact>7</SpFact>
    <SubBand>G1</SubBand>
    <Channel>LC2</Channel>
    <DevLrrCnt>2</DevLrrCnt>
    <Lrrid>08040059</Lrrid>
    <Late>0</Late>
    <LrrLAT>48.874931</LrrLAT>
    <LrrLON>2.333673</LrrLON>
    <Lrrs>
        <Lrr>
            <Lrrid>08040059</Lrrid>
            <LrrRSSI>-60.000000</LrrRSSI>
            <LrrSNR>9.750000</LrrSNR>
            <LrrESP>-59.000000</LrrESP>
        </Lrr>
        <Lrr>
            <Lrrid>33d13a41</Lrrid>
            <LrrRSSI>-73.000000</LrrRSSI>
            <LrrSNR>9.750000</LrrSNR>
            <LrrESP>-72.000000</LrrESP>
        </Lrr>
    </Lrrs>
    <CustomerID>100000507</CustomerID>
    <CustomerData>...</CustomerData>
    <ModelCfg>0</ModelCfg>
    <InstantPER>0.02</InstantPER>
    <MeanPER>0.02</MeanPER>
    <DevAddr>0405F519</DevAddr>
    <UplinkDC>0.001</UplinkDC>
    <UplinkDCSubBand>0.009</UplinkDCSubBand>
    <DevLocTime>2015-01-27T10:00:43.336+01:00</DevLocTime>
    <DevLAT>10.11212</DevLAT>
    <DevLON>7.44464</DevLON>
    <DevAlt>50</DevAlt>
    <DevLocRadius>100</DevLocRadius>
    <DevAltRadius>50</DevAltRadius>
    <DevAcc>50</DevAcc>
</DevEUI_uplink>
```

### Actility ThingPark JSON
```
{
   "DevEUI_uplink":{
      "-xmlns":"http://uri.actility.com/lora",
      "Time":"2015-07-09T16:06:38.49+02:00",
      "DevEUI":"000000000F1D8693",
      "FPort":"2",
      "FCntUp":"7011",
      "ADRbit":"1",
      "ACKbit":"1",
      "MType":"4",
      "FCntDn":"11",
      "payload_hex":"0027bd00",
      "mic_hex":"38e7a3b9",
      "Lrcid":"00000065",
      "LrrRSSI":"-60.000000",
      "LrrSNR":"9.750000",
      "SpFact":"7",
      "SubBand":"G1",
      "Channel":"LC2",
      "DevLrrCnt":"2",
      "Lrrid":"08040059",
      "Late":"0",
      "LrrLAT":"48.874931",
      "LrrLON":"2.333673",
      "Lrrs":{
         "Lrr":[
            {
               "Lrrid":"08040059",
               "LrrRSSI":"-60.000000",
               "LrrSNR":"9.750000",
               "LrrESP":"-59.000000"
            },
            {
               "Lrrid":"33d13a41",
               "LrrRSSI":"-73.000000",
               "LrrSNR":"9.750000",
               "LrrESP":"-72.000000"
            }
         ]
      },
      "CustomerID":"100000507",
      "CustomerData":"...",
      "ModelCfg":"0",
      "InstantPER":"0.02",
      "MeanPER":"0.02",
      "DevAddr":"0405F519",
      "UplinkDC":"0.001",
      "UplinkDCSubBand":"0.009"
   }
}
```

### IoT Things Twin Payload
The Bosch IoT Things Payload would look like the following. 

Note the "Data" Feature which contains the raw Actility ThingPark payload encoded in Base64.

```
{
  "topic" : "/bcx/lorawan.null/things/twin/commands/modify",
  "path" : "/features",
  "value" : {
    "features" : {
      "Packet" : {
        "properties" : {
          "InstantPER" : "0.02",
          "Late" : "0",
          "FPort" : "2",
          "Time" : "2015-07-09T16:06:38.49+02:00",
          "MeanPER" : "0.02",
          "UplinkCounter" : "7011",
          "DownlinkCounter" : "11"
        }
      },
      "Device" : {
        "properties" : {
          "ADRBit" : "1",
          "SubBand" : "G1",
          "Channel" : "LC2",
          "SpreadFactor" : "7",
          "EUI" : "000000000F1D8693",
          "ACKBit" : "1",
          "Addr" : "0405F519"
        }
      },
      "Location" : {
        "properties" : {
          "Radius" : null,
          "VerticalRadius" : null,
          "GeoTimestamp" : null,
          "Latitude" : null,
          "Longitude" : null,
          "Altitude" : null
        }
      },
      "LRR" : {
        "properties" : {
          "RSSI" : "-60.000000",
          "SNR" : "9.750000",
          "LRRCount" : "2",
          "Latitude" : "48.874931",
          "Id" : "08040059",
          "Longitude" : "2.333673"
        }
      },
      "LRC" : {
        "properties" : {
          "ID" : "00000065"
        }
      },
      "Data" : {
        "properties" : {
          "MIC" : "38e7a3b9",
          "Payload" : "0027bd00"
        }
      },
      "Customer" : {
        "properties" : {
          "Data" : "...",
          "ID" : "100000507"
        }
      },
      "Uplink" : {
        "properties" : {
          "content-type" : "application/json",
          "value" : "ewogICAiRGV2RVVJX3VwbGluayI6ewogICAgICAiLXhtbG5zIjoiaHR0cDovL3VyaS5hY3RpbGl0eS5jb20vbG9yYSIsCiAgICAgICJUaW1lIjoiMjAxNS0wNy0wOVQxNjowNjozOC40OSswMjowMCIsCiAgICAgICJEZXZFVUkiOiIwMDAwMDAwMDBGMUQ4NjkzIiwKICAgICAgIkZQb3J0IjoiMiIsCiAgICAgICJGQ250VXAiOiI3MDExIiwKICAgICAgIkFEUmJpdCI6IjEiLAogICAgICAiQUNLYml0IjoiMSIsCiAgICAgICJNVHlwZSI6IjQiLAogICAgICAiRkNudERuIjoiMTEiLAogICAgICAicGF5bG9hZF9oZXgiOiIwMDI3YmQwMCIsCiAgICAgICJtaWNfaGV4IjoiMzhlN2EzYjkiLAogICAgICAiTHJjaWQiOiIwMDAwMDA2NSIsCiAgICAgICJMcnJSU1NJIjoiLTYwLjAwMDAwMCIsCiAgICAgICJMcnJTTlIiOiI5Ljc1MDAwMCIsCiAgICAgICJTcEZhY3QiOiI3IiwKICAgICAgIlN1YkJhbmQiOiJHMSIsCiAgICAgICJDaGFubmVsIjoiTEMyIiwKICAgICAgIkRldkxyckNudCI6IjIiLAogICAgICAiTHJyaWQiOiIwODA0MDA1OSIsCiAgICAgICJMYXRlIjoiMCIsCiAgICAgICJMcnJMQVQiOiI0OC44NzQ5MzEiLAogICAgICAiTHJyTE9OIjoiMi4zMzM2NzMiLAogICAgICAiTHJycyI6ewogICAgICAgICAiTHJyIjpbCiAgICAgICAgICAgIHsKICAgICAgICAgICAgICAgIkxycmlkIjoiMDgwNDAwNTkiLAogICAgICAgICAgICAgICAiTHJyUlNTSSI6Ii02MC4wMDAwMDAiLAogICAgICAgICAgICAgICAiTHJyU05SIjoiOS43NTAwMDAiLAogICAgICAgICAgICAgICAiTHJyRVNQIjoiLTU5LjAwMDAwMCIKICAgICAgICAgICAgfSwKICAgICAgICAgICAgewogICAgICAgICAgICAgICAiTHJyaWQiOiIzM2QxM2E0MSIsCiAgICAgICAgICAgICAgICJMcnJSU1NJIjoiLTczLjAwMDAwMCIsCiAgICAgICAgICAgICAgICJMcnJTTlIiOiI5Ljc1MDAwMCIsCiAgICAgICAgICAgICAgICJMcnJFU1AiOiItNzIuMDAwMDAwIgogICAgICAgICAgICB9CiAgICAgICAgIF0KICAgICAgfSwKICAgICAgIkN1c3RvbWVySUQiOiIxMDAwMDA1MDciLAogICAgICAiQ3VzdG9tZXJEYXRhIjoiLi4uIiwKICAgICAgIk1vZGVsQ2ZnIjoiMCIsCiAgICAgICJJbnN0YW50UEVSIjoiMC4wMiIsCiAgICAgICJNZWFuUEVSIjoiMC4wMiIsCiAgICAgICJEZXZBZGRyIjoiMDQwNUY1MTkiLAogICAgICAiVXBsaW5rREMiOiIwLjAwMSIsCiAgICAgICJVcGxpbmtEQ1N1YkJhbmQiOiIwLjAwOSIKICAgfQp9"
        }
      }
    }
  }
}
```
## Limitations

Since Hono currently only support uplink in the form of telemetry and events, 
the adapter is developed mainly for LoRaWAN Class A devices without downlink support.


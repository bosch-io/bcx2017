# Actility LoRaWAN Adapter for Hono

This adapter exposes an endpoint which can be called by the Actility Thingpark backend. 

The url of the adapter is http://hono.bosch-iot-suite.com:10060

#### Exposed Endpoints
(POST)	Telemetry 		http://hono.bosch-iot-suite.com:10060/  
(GET)	Adapter Status	http://hono.bosch-iot-suite.com:10060/status  
(GET)	Last Known Error	http://hono.bosch-iot-suite.com:10060/status/err  
(GET)	Last Device Call	http://hono.bosch-iot-suite.com:10060/status/bcx2017/[deviceId]  

## Setting Up

### Pre-requisites

1. You'll need to register your device id on hono via Hono's registration endpoint. This would be typically the Device's EUI.

### Configuring the Actility Backend

-- TODO --

## Usage

### Uplink

If you have configured your device's Callback on the Actility Backend, the call to the adapter should automatically be made and you need not do anything further.

You can then just subscribe to any notifications to your device via Hono.

The adapter supports either application/json or application/xml format.

### Debugging and Errors

#### Last Device Status
In order to debug/observe the last call made by your device to the adapter, you can call the following endpoint: /status/bcx2017/[deviceid]

The endpoint will return the last packet frame sent by your device to the adapter via the Sigfox Backend.

#### Other Debugging/Status Endpoints

/status		- General status and uptime information
/status/err 	- Last reported/known error/exception encountered by the adapter

## Packet Frame Format
A typical incoming LoRaWAN payload would look something like this:

### XML
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

### JSON
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


## Limitations

Since Hono currently only support uplink in the form of telemetry and events, 
the adapter is developed mainly for LoRaWAN Class A devices without downlink support.


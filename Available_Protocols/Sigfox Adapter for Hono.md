# Sigfox Adapter for Hono

This adapter exposes an endpoint which can be called by the Sigfox adapter. In Sigfox nomenclature, this refers to the Callback endpoint. 

The url of the adapter is http://hono.bosch-iot-suite.com:10050

## Setting Up

### Pre-requisites

1. You'll need to register your device id on hono via Hono's registration endpoint

### Configuring the Sigfox Backend for Uplink

1. From the Sigfox backend, you'll need to create a Callback for your device and configure with the following parameters (New > Custom Callback)
2. Type "Data - Uplink"
3. Channel as "URL"
4. Send Duplicate - Unchecked
5. URL Pattern: http://hono.bosch-iot-suite.com:10050/telemetry/bcx2017/{device}
6. HTTP Method: POST
7. Content-Type: application/json
8. Body: {  
	"time" : "{time}",  
	"device" : "{device}",  
	"duplicate" : "{duplicate}",  
	"snr" : "{snr}",  
	"avgSnr" : "{avgSnr}",  
	"rssi" : "{rssi}",  
	"station" : "{station}",  
	"lat" : "{lat}",  
	"lng" : "{lng}",  
	"seqNumber” : "{seqNumber}",  
	"data" : "{data}"  
}  

### Configuring the Sigfox Backend for Downlink (Optional)

## Usage

### Uplink

If you have configured your device's Callback on the Sigfox Backend, the call to the adapter should automatically be made and you need not do anything further.

### Downlink

Since Hono doesn't support downstream as yet, you can make downstream requests directly to the adapter.

To make a downlink call to your Sigfox device, call the /dl/bcx2017/<deviceid> endpoint with a POST request.

The body of the request should be the data you wish to send downstream.

The next time the Sigfox device calls with an "ack" request, the data intended for downstream will be pushed down during the response.

### Last Device Status (For Debugging)

In order to debug/observe the last call made by your device to the adapter, you can call the following endpoint: /status/bcx2017/<deviceid>

## Limitations

### Downlink

Hono only currently supports telemetry and events. Command and Control (i.e. downlink) is not supported. As such, to perform downlink operations, you'll need to call the adapter's downlink endpoint manually. See Usage.
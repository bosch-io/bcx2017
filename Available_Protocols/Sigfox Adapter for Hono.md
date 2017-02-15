# Sigfox Adapter for Hono

This adapter exposes an endpoint which can be called by the Sigfox adapter. In Sigfox nomenclature, this refers to the Callback endpoint. 

#### Endpoints

##### Performing an Uplink from Sigfox
(POST)	 http://hono.bosch-iot-suite.com:10050/telemetry/bcx/[deviceid]

##### Performing a Downlink			
(POST) http://hono.bosch-iot-suite.com:10050/dl/bcx/[deviceId]

##### Status and Stats of the Sigfox Adapter
(GET)	http://hono.bosch-iot-suite.com:10050/status  

##### Last Incoming Payload for Device
(GET)	http://hono.bosch-iot-suite.com:10050/status/bcx/[deviceId]/incoming

##### Last Twin Payload for Device
(GET)	http://hono.bosch-iot-suite.com:10050/status/bcx/[deviceId]/twin

## Setting Up

### Pre-requisites

1. You'll need to register your device id on hono via Hono's registration endpoint

2. You can do this via Hono's REST Adapter endpoint for Registration. 
```curl -X POST -i -d 'device_id=sigfox.(deviceid)' http://hono.bosch-iot-suite:8080/registration/DEFAULT_TENANT```)

3. Devices should have a prefix of "sigfox."

### Configuring the Sigfox Backend

1. From the Sigfox backend, you'll need to create a Callback for your device and configure with the following parameters (New > Custom Callback)
2. Type "Data - Uplink"
3. Channel as "URL"
4. Send Duplicate - Unchecked
5. URL Pattern: http://hono.bosch-iot-suite.com:10050/telemetry/bcx/{device}
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
	"data" : "{data}"  
}  

## Usage

### Uplink

If you have configured your device's Callback on the Sigfox Backend, the call to the adapter should automatically be made and you need not do anything further.

You can then just subscribe to any notifications to your device via Hono or IoT Things.

**NOTE: The Device ID for devices via this adapter will be appended with "sigfox."**  
**Example: sigfox.mydevice**  

### Downlink

Since Hono doesn't support downstream as yet, you can make downstream requests directly to the adapter.

To make a downlink call to your Sigfox device, call the /dl/bcx/[deviceid] endpoint with a POST request.

The body of the request should be the data you wish to send downstream.

The next time a Callback request from Sigfox is made for the device, the data intended for downstream will be pushed down with the response. (HTTP 200).

A HTTP code of 204 (No-Content) is returned if there's no downlink data for your device.

## Payload Format

### Sigfox Payload
A typical incoming Sigfox payload would look something like this:

```
{
	"time" : "1480717528",
	"device" : "sigfox-device-1",
	"duplicate" : "false",
	"snr" : "18.53"",
	"avgSnr" : "32.50",
	"rssi" : "-123.00",
	"station" : "0DF1",
	"lat" : "49.0",
	"lng" : "3.0",
	"data" : "1234"
}
```

### IoT Things Twin Payload
The Bosch IoT Things Payload would look like the following. 

Note the "Data" Feature which contains the raw Sigfox payload encoded in Base64.

```
{
  "topic" : "/bcx/sigfox.device-1/things/twin/commands/modify",
  "path" : "/features",
  "value" : {
    "features" : {
      "Message" : {
        "properties" : {
          "seqNum" : "31",
          "duplicate" : "false",
          "time" : "14807175012"
        }
      },
      "Location" : {
        "properties" : {
          "lng" : "3.0",
          "station" : "0DF1",
          "lat" : "49.0"
        }
      },
      "Signal" : {
        "properties" : {
          "avgSnr" : "32.50",
          "rssi" : "-123.00",
          "snr" : "18.53"
        }
      },
      "Data" : {
        "properties" : {
          "value" : "1234"
        }
      },
      "Uplink" : {
        "properties" : {
          "content-type" : "application/json",
          "value" : "ewogICJ0aW1lIiA6ICIxNDgwNzE3NTAxMiIsIAogICJkZXZpY2UiIDogInNpZ2ZveC1kZXZpY2UtMSIsCiAgImR1cGxpY2F0ZSIgOiAiZmFsc2UiLAogICJzbnIiIDogIjE4LjUzIiwKICAiYXZnU25yIiA6ICIzMi41MCIsCiAgInJzc2kiIDogIi0xMjMuMDAiLAogICJzdGF0aW9uIiA6ICIwREYxIiwKICAibGF0IiA6ICI0OS4wIiwKICAibG5nIiA6ICIzLjAiLAogICJzZXFOdW0iIDogIjMxIiwKICAiZGF0YSIgOiAiMTIzNCIKfQ=="
        }
      }
    }
  }
}
```

## Limitations

### Downlink

- Hono only currently supports telemetry and events. Command and Control (i.e. downlink) is not supported. As such, to perform downlink operations, you'll need to call the adapter's downlink endpoint manually. See Usage.
- Downlink messages are also limited to 8 bytes. This is a Sigfox limitation.
- Calls to the downlink endpoints do not currently check if a device is registered on Hono
## Send telemetry data

You can send telemetry data to Hono via different protocol adapters. In order to find out how the payload is supposed to look like that you need to send to Hono in order to be accepted by Bosch IoT Things and thus being able to visualize the data, the Developer Console is the perfect way to help you here.

**Prerequisite**

You have successfully registered your device in the Bosch IoT Suite. 

**Steps**

- Go to _Thing Browser_ and select the device for which you would like to send telemetry data to the Bosch IoT Suite. This opens the thing details.
- Select the feature from the dropdown list 
- Open the tab _Hono Message Format_ to see the JSON with a device-specific sample payload that you need to send to the HTTP Connector. It gives you examples to either modify an entire feature or a specific feature property.

_Example_:

![Sample Payload](images/SendPayloadSample.png)

- Implement your device client according to the sample JSON payload
- Verify the received Hono message, sent from your device, in the Developer Console by switching to the tab _Feature_ 

## What's next ?

[Visualize the device data](visualize_data.md)
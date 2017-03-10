## Send telemetry data

You can send telemetry data to Hono via different protocol adapters. In order to find out how the payload is supposed to look like that you need to send to Hono in order to be accepted by Bosch IoT Things, the Developer Console is the perfect tool to assist you.

**Prerequisite**

You have successfully registered your device in the Bosch IoT Suite. 

**Steps**

- Sign in to the [Developer Console](https://console.bosch-iot-suite.com)
- Go to _Thing Browser_ and select the device from which you would like to push data to the Cloud backend This opens the thing details.
- Select the feature from the dropdown list in the info tab
- Open the tab _Hono Message Format_ to see the JSON with a device-specific sample payload that you need to send to the HTTP Connector. It gives examples to either modify an entire feature or a specific feature property

_Example_:

![Sample Payload](images/SendPayloadSample.png)

- Implement your device client according to the given sample JSON payload
- Verify the incoming device message by switching to the tab _Feature_.

## What's next ?

[Visualize the device data](visualize_data.md)
## Register new device in the Bosch IoT Suite

Here we are going to walk you through, how to register a new device to the Bosch IoT Suite. Once you have successfully registered it, you are able to send device payload to Hono. 

**Prerequisite**

You have logged on to the developer console with your user credentials.

**Steps**

- Go to _Thing Browser_ and choose _Connect thing_ 
- Optionally search and select the type of thing you would like to register. If you do not find the type in the list, you have to possibility to [create a new thing type](create_thingtype.md) for your device and let the Suite learn the semantics that way. You can also skip this part and proceed to the next step.
- Choose the type of connector that your device is going to connect with the Bosch IoT Suite
- Type in the technical device ID, e.g. mac address or serial number, of your device. For example: xdk.<macaddress>
- Select _Complete_ to finish the device registration
- Select _View Thing Details_ to take a look at the device that you have just registered.

## What's next ?

[Send Telemetry data](send_telemetry_data.md)






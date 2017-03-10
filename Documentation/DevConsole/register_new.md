## Register a new device in the Bosch IoT Suite

Before you can push device data to the Suite, you must register your device.
We are going to show you how easy it is with using the Developer Console.

**Steps**

- Sign in to the [Developer Console](https://console.bosch-iot-suite.com)
- Go to _Thing Browser_ and choose _Connect thing_ 
- In the first screen, you have the option to select a specific type of thing you would like to register. If you do not find the type in the list, you have to possibility to [create a new thing type](create_thingtype.md) or proceed to the next step.
- Choose the type of connector that your device is going to connect to the backend.
- Key in the technical device ID, e.g. mac address or serial number of your device. Example format: `xdk.<macaddress>`
- Select _Complete_ to finish the device registration
- Select _View Thing Details_ to take a look at the device that you have just registered.

**Fantastic!** Your device is registered in the Suite and is now ready to push data.  

## What's next ?

[Send Telemetry data to Things via Hono](send_telemetry_data.md)






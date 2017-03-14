![Available in Eclipse Hono](images/shields/Eclipse_Hono-available.png)
![Available in Bosch IoT Things](images/shields/Bosch_IoT_Things-available.png)
![Available in Bosch IoT Developer Console](images/shields/Bosch_IoT_Developer_Console-available.png)
![You can work directly on this device](images/shields/Hacking_on_device-available.png)

# Bosch XDK

I'm a programmable sensor device & a prototyping platform for any IoT use case you can imagine.

<img src="images/Bosch_XDK_21.png " width="255">
	
## Available devices

The XDK Device are identified through their MAC address, printed at the back of the device. A lot of these devices are available!

XDKs publish telemetry and events into Hono using a device ID  `xdk.<macaddress>`.

## Device capabilities

The XDK has a number of sensors. All of these values are transmitted via LWM2M every 10 seconds:

- Temperature
- Humidity
- Pressure
- Accelerometer
- Gyroscope
- Magnetometer 
- Digital light sensor 

In addition, the device provides the following events

- (2x) Button press

## Ideas for using this device

The XDK110 is a universal programmable sensor device that enables rapid prototyping of sensor based products and small batch applications for the Internet of Things (IoT). Inclusive of multiple Micro-Electromechanical Systems (MEMS) sensors, the XDK110 enables time and cost effective realization of IoT applications while offering users the freedom of programming from a basic to an advanced level. The XDK110 is an integrated product that offers access to the XDK developer community for active information exchange, knowledge sharing, technical support and more. The device was designed to allow users an easy transition from prototype to mass production by providing a clear road to product development.

## Getting Started

Just turn on the XDK and it will send all sensor data to **Hono**, **Things** and to the **Developer Console**.

Part of the XDK is a battery that can be recharged by using the included micro usb-cable. You can connect it to any power source of your choice (e.g. your Notebook). Please make sure that the battery is charged when using the XDK.

As default configuration the XDK sends every 10 seconds all sensor values to the systems mentioned above. If you have the need to change this interval to a lower/higher value please get in contact with a **Hack Coach**.

If you want to change the preflashed firmware of the XDK you need the XDK Workbench which can be downloaded by the the provided **Nextcloud** link.
Also in **Nextcloud** are the sources of the preflashed firmware. Just import it to the XDK Workbench and adapt it to your needs. Afterwards you can easily flash the adapted firmware to the XDK by using the usb-cable connected to your notebook.

Of course you can also flash any other custom firmware if you want to. Check the example projects of the XDK Workbench as inspiration.

## Additional information

Detailed technical specifications can be found on the [product page](http://xdk.io). XDK API Documentation [API page](https://xdk.bosch-connectivity.com/xdk-api).

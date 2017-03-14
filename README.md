
# Welcome to [Bosch Connected Experience 2017](http://bcw.bosch-si.com/berlin/bcw-hackathon/)!

In this repository, you will find all the information needed to get started with the device hub we set up for this hackathon. The device hub itself is a custom setup for this hackathon and composed of components from the [Eclipse IoT](https://iot.eclipse.org) community, services from the [Bosch IoT Suite](https://bosch-iot-suite.com) and other open source and custom components.

**Note:** To access the systems, you will need credentials that are shared on-site on the whiteboard.

# Introduction

The general idea of this setup is: We want you to be able to prototype IoT ideas and solutions as quickly as possible. For that, we provide you with data from many different “Things”: A number of Bosch devices from various domains, as well as some commonly-used prototyping boards.

All of these devices are already connected to a *Messaging Hub* based on [Eclipse Hono](https://www.eclipse.org/hono) running on the Internet. We have also tried to make sure that you can easily access the data and events produced by these devices using multiple APIs.

By the way – many of the available devices allow you to work not just with the data on the backend, but directly on the device itself.

The following diagram provides an overview of the setup.

![Overview diagram](Overview.png "Overview diagram")

# So, what kind of devices are available?

You can find information about the devices we have connected to Hono in the [Available_Devices/](Available_Devices/) directory. (There's also quite a number of sensors and other devices available in the “Gadget Library”, but these are not (yet) connected to our backend.)

# What kind of data do these devices send?

Most devices supply both telemetry data (information on the current device status: temperature, humidity, …) and events (e.g. buttons pressed, the device being moved, …). All devices supply their data as JSON data structures. In a number of cases, the device data also contains additional meta-information that allows us to display the information better in our services.

# Great. Now, what can I do?

- [The first step: Find out how to receive device messages](1_Receive_Device_Messages.md)
- [Find out how to use the time series database for historical data](2_Time_Series_Data.md)
- [Find out how to work with the Digital Twin](3_Digital_Twin.md)
- [Find out how to view devices in the Developer Console](4_Developer_Console.md)


# What else do I need to get started?

A cup of coffee. A good idea. And a great team to work with.

If you need any help, we have a team of hack coaches available to help you. You'll find information on who to refer to for each topic on the tables at the hackathon site. If you want to connect one of your own devices to this backend, please approach one of the hack coaches.

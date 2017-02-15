# How can I receive current device data from the messaging hub?

The connected devices publish their telemetry data and events to an [Eclipse Hono](https://www.eclipse.org/hono/) instance accessible at `hono.bosch-iot-suite.com`.

You can consume the device data by connecting an AMQP 1.0 client to Hono and listening in on the `telemetry` or `event` addresses respectively. We have prepared some example client code using various programming languages in the [Example_Clients/](Example_Clients/) directory.

Hono is actually payload-agnostic, so you will just receive the raw payload in the AMQP client in the same way as it was fed into Hono by the device.

The Eclipse Hono website has further information on the [Telemetry API](https://www.eclipse.org/hono/api/Telemetry-API/) and the [Event API](https://www.eclipse.org/hono/api/Event-API/).


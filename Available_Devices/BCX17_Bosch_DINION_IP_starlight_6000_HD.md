# Bosch DINION IP starlight 6000 HD

The Bosch DINION IP starlight 6000 HD is a excellent low-light performance surveillance camera that can be used to detect autonomous scenarios. In addition, with the Microsoft cloud you can analyse emotion in faces.

![Bosch DINION IP starlight 6000 HD](images/DINION_IP_starlight_6000_HD.jpg "Bosch DINION IP starlight 6000 HD")

## Available devices

The DINION cameras are identified through their IP address, printed on the of the device. The following devices are available:

- (name) 192.168.1.200-192.168.1.220 (available on every table)
- (name).X (in the hono cloud)

## Device capabilities

On top of recording video, the cameras can also
- analyse ambient audio
- receive external alarm signals
- detect shaking of the camera

These signals are then processed and can be used to generate _events_. By default, cameras detect objects
entering the frame and sent out an event `object_seen`. Additional events (including video analytics (IVA))
can be configured directly inside the camera.

### Example telemetry data


## Ideas for using this device

- Take a picture with camera when a face is detected and send this picture for analysis into the cloud
- Alarm when the noise level is too high

## Additional information

Detailed technical specifications can be found on the [product page](https://us.boschsecurity.com/en/products/videosystems/ipcameras/hdmpfixedcameras/dinionipstarlight6000hd_1/dinionipstarlight6000hd_1_products_42121).
More infos for recognize emotions in images with Microsoft Cognitive Services [API page](https://www.microsoft.com/cognitive-services/en-us/emotion-api).
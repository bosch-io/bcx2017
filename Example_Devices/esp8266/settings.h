#ifndef SETTINGS_H
#define SETTINGS_H

// ---- Wifi Configuration ----

const char *wifi_ssid = "SSID";
const char *wifi_pass = "PASS";

const char *hono_host = "hono.bosch-iot-suite.com";
const int hono_port = 8080;

const char *template_uri_telemetry = "/telemetry/bcx/esp8266.%02x%02x%02x%02x%02x%02x";
const char *template_uri_event = "/event/bcx/esp8266.%02x%02x%02x%02x%02x%02x";

const char *template_payload_topic = "bcx/esp8266.%02x%02x%02x%02x%02x%02x/things/twin/commands/modify";

// ---- Hardware Configuration ----

ADC_MODE(ADC_VCC); // enable reading in VCC of ESP8266

const int sensorUpdateRateMS = 10000; // Send updated sensor value every 10 seconds

#define PIN_PIR           16   // Use GPIO16 for PIR sensor
#define PIN_BUTTON        4    // Use GPIO4 for button input


#endif


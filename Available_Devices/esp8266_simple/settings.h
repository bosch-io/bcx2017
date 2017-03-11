#ifndef SETTINGS_H
#define SETTINGS_H

#include "ArduinoJson.h" // Make sure you have the ArduinoJson library installed
#include "RestClient.h" // Make sure you have the ESP8266RestClient library installed

#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BME280.h>
#include <Adafruit_NeoPixel.h>

// ---- Hono Configuration ----
const char *hono_host = "hono.bosch-iot-suite.com";
const int hono_http_port = 8080;
const char *hono_tenant = "bcx";

enum Hono_message_type {
  TELEMETRY,
  EVENT 
};


// ---- Wifi Configuration ----

//const char *wifi_ssid = "BCX17 OpenHack";
//const char *wifi_pass = "BCX17.opnh";

const char *wifi_ssid = "sazgw00056";
const char *wifi_pass = "7aEbcvPdShz7guIQfqRg";

const char *template_uri_telemetry = "/telemetry/bcx/esp8266.%02x%02x%02x%02x%02x%02x";
const char *template_uri_event = "/event/bcx/esp8266.%02x%02x%02x%02x%02x%02x";

const char *template_payload_topic = "bcx/esp8266.%02x%02x%02x%02x%02x%02x/things/twin/commands/modify";

// ---- Hardware Configuration ----

ADC_MODE(ADC_VCC); // enable reading in VCC of ESP8266

const int sensorUpdateRateMS = 10000; // Send updated sensor value every 10 seconds

#define PIN_BUTTON        0    // Use GPIO4 for button input


#endif


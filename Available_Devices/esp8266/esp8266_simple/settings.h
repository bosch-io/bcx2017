#ifndef SETTINGS_H
#define SETTINGS_H

#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266WebServer.h>
#include <ESP8266mDNS.h>

#include "ArduinoJson.h" // Make sure you have the ArduinoJson library installed
#include "RestClient.h" // Make sure you have the ESP8266RestClient library installed

// ---- Wifi Configuration ----

const char *wifiSsid = "BCX17 OpenHack";
const char *wifiPass = "BCX17.opnh";

// ---- Hono Configuration ----
const char *honoHost = "hono.bosch-iot-suite.com";
const int honoHttpPort = 8080;
const char *honoTenant = "bcx";

// ---- Hardware Configuration ----

ADC_MODE(ADC_VCC); // enable reading in VCC of ESP8266

const int sensorUpdateRateMS = 10000; // Send updated sensor value every 10 seconds

#define PIN_BUTTON        0    // Use GPIO0 for button input


// ---- Types ----

enum HonoMessageType {
    TELEMETRY,
    EVENT 
};

// ----- Functions ----

const char * getDeviceId();
const char * getHostName();
void logmsg(const char * component, const String message);   
void logmsgln(const char * component, const String message);    
void setupSensors();
void setupWebserver();
bool setupHono();
void loopSensors();
void loopWebserver();

#endif


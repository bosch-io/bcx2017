#include "settings.h"

// ---- Initial values ----

bool prevButton = false;
unsigned long lastMillis = 0;

// ---- Functions to retrieve sensor values ----

float getVcc () {
  return ESP.getVcc() / 1000.0;
}

bool getButton() {
  return !digitalRead(PIN_BUTTON);
}

void setLed(bool value) {
    digitalWrite(PIN_LED, value);
}

void publish_sensors() {
    StaticJsonBuffer<512> jsonBuffer;
    JsonObject& root = jsonBuffer.createObject();
    root["voltage"] = getVcc();

    // Create more complex structures using nested objects:
    // JsonObject& subelement = value.createNestedObject("elementkey");

    publish(TELEMETRY, root);
}

// ---- Initialization & loop ----

void setup_sensors() {
    pinMode(PIN_BUTTON, INPUT);
    pinMode(PIN_LED, OUTPUT);
}

void loop_sensors() {
    bool button = getButton();
    if (prevButton != button) {
        logmsg("Button", "Value changed to ");
        Serial.println(button);
        prevButton = button;
        publish_bool_event("button_flash", button);
    }

    if (millis() - lastMillis > sensorUpdateRateMS) {
        lastMillis = millis();
        publish_sensors();
    }
}


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

void publishSensors() {
    StaticJsonBuffer<512> jsonBuffer;
    JsonObject& root = jsonBuffer.createObject();
    root["voltage"] = getVcc();

    // Create more complex structures using nested objects:
    // JsonObject& subelement = value.createNestedObject("elementkey");

    publish(TELEMETRY, root);
}

// ---- Initialization & loop ----

void setupSensors() {
    pinMode(PIN_BUTTON, INPUT);
}

void loopSensors() {
    bool button = getButton();
    if (prevButton != button) {
        logmsg("Button", "Value changed to ");
        Serial.println(button);
        prevButton = button;
        publishBoolEvent("button_flash", button);
    }

    if (millis() - lastMillis > sensorUpdateRateMS) {
        lastMillis = millis();
        publishSensors();
    }
}


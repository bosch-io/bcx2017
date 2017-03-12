#include "settings.h"

#define SEALEVELPRESSURE_HPA (1013.25)
 
Adafruit_BME280 bme; // I2C
Adafruit_NeoPixel strip = Adafruit_NeoPixel(1, PIN_NEOPIXEL, NEO_GRB + NEO_KHZ800);

// ---- Initial values ----

bool prevButton = false;
unsigned long lastMillis = 0;
bool bme280_initialized = false;

float getVcc () {
  return ESP.getVcc() / 1000.0;
}

bool getButton() {
  return !digitalRead(PIN_BUTTON);
}

void setNeoPixelColor(char red, char green, char blue) {
    uint32_t c = strip.Color(red, green, blue);
    
    for(uint16_t i=0; i<strip.numPixels(); i++) {
        strip.setPixelColor(i, c);
    }
    strip.show();
}

void publish_sensors() {
    StaticJsonBuffer<512> jsonBuffer;
    JsonObject& root = jsonBuffer.createObject();
    root["voltage"] = getVcc();
    
    JsonObject& bme280 = root.createNestedObject("bme280");
    JsonObject& values = bme280.createNestedObject("values");
    values["temperature"] = bme.readTemperature();
    values["pressure"] = bme.readPressure() / 100.0F;  // in hPa
    values["altitude"] = bme.readAltitude(SEALEVELPRESSURE_HPA);
    values["humidity"] = bme.readHumidity();    
    JsonObject& units = bme280.createNestedObject("units");
    units["temperature"] = "°C";
    units["pressure"] = "hPa";
    units["altitude"] = "m";
    units["humidity"] = "%";

    publish(TELEMETRY, root);
}

// ---- Initialization & loop ----

void setup_sensors() {
    pinMode(PIN_BUTTON, INPUT);
//    pinMode(PIN_LED, OUTPUT);

    logmsg("Sensors", "Searching for BME280: ");
    if (bme.begin()) {
        bme280_initialized = true;
        Serial.println("OK");
    } else {
        Serial.println("Not found");

    }
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


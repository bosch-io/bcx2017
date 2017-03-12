#include "settings.h"

#define SEALEVELPRESSURE_HPA (1013.25)
 
Adafruit_BME280 bme; // I2C
Adafruit_NeoPixel strip = Adafruit_NeoPixel(1, PIN_NEOPIXEL, NEO_GRB + NEO_KHZ800);

// ---- Initial values ----

bool prevButton = false;
unsigned long lastMillis = 0;
bool bme280Initialized = false;

bool prevHighHumidity = false;
double humidityThreshold = 50.0;

float getVcc () {
  return ESP.getVcc() / 1000.0;
}

void setNeoPixelColor(char red, char green, char blue) {
    uint32_t c = strip.Color(red, green, blue);
    
    for(uint16_t i=0; i<strip.numPixels(); i++) {
        strip.setPixelColor(i, c);
    }
    strip.show();
}

void publish_value_alert(String key, bool state, double value) {
    StaticJsonBuffer<512> jsonBuffer;
    JsonObject& root = jsonBuffer.createObject();
    JsonObject& keyObject = root.createNestedObject(key);
    keyObject["alert"] = state;
    keyObject["value"] = value;
    root["uptime"] = millis();

    publish(EVENT, root);
}

void publish_sensors() {
    StaticJsonBuffer<512> jsonBuffer;

    logmsgln("Sensors", "Collecting telemetry data");

    JsonObject& root = jsonBuffer.createObject();
    root["voltage"] = getVcc();

    if (bme280Initialized) {
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
    }
    publish(TELEMETRY, root);
}

// ---- Initialization & loop ----

void setup_sensors() {
    strip.begin();
    strip.show(); // Initialize all pixels to 'off'
 
    logmsg("Sensors", "Searching for BME280: ");
    delay(500);
    if (bme.begin()) {
        bme280Initialized = true;
        delay(100);
        Serial.println("OK");
    } else {
        Serial.println("Not found");
    }
}

void loop_sensors() {
    double h = bme.readHumidity();
    if (h > humidityThreshold) {
        if (!prevHighHumidity) {
            setNeoPixelColor(255, 0, 0);
            logmsgln("Sensors", "High humidity detected");
            prevHighHumidity = true;
            publish_value_alert("humidity_alert", true, h);
        }
    } else {
        if (prevHighHumidity) {
            setNeoPixelColor(0, 255, 0);
            logmsgln("Sensors", "Humidity no longer high");
            prevHighHumidity = false;
            publish_value_alert("humidity_alert", false, h);
        }            
    }

    if (millis() - lastMillis > sensorUpdateRateMS) {
        lastMillis = millis();
        publish_sensors();
    }
}

void setHumidityThreshold(double th) {
    humidityThreshold = th;
}


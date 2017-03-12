#include "settings.h"

#define PIN_NEOPIXEL 13
 
Adafruit_BME280 bme;
Adafruit_NeoPixel strip = Adafruit_NeoPixel(1, PIN_NEOPIXEL, NEO_GRB + NEO_KHZ800);

bool has_bme280 = false;

void init_sensors() {
  if (!bme.begin()) {
    has_bme280 = true;
  }
}


void loop_sensors() {
  if (has_bme280) {
    Serial.print("Temperature = ");
    Serial.print(bme.readTemperature());
    Serial.println(" *C");
  }
}


/*
 * Bosch SI Example Code License Version 1.0, January 2016
 *
 * Copyright 2016 Bosch Software Innovations GmbH ("Bosch SI"). All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the
 * following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following
 * disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the
 * following disclaimer in the documentation and/or other materials provided with the distribution.
 *
 * BOSCH SI PROVIDES THE PROGRAM "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. THE ENTIRE RISK AS TO THE
 * QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU. SHOULD THE PROGRAM PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL
 * NECESSARY SERVICING, REPAIR OR CORRECTION. THIS SHALL NOT APPLY TO MATERIAL DEFECTS AND DEFECTS OF TITLE WHICH BOSCH
 * SI HAS FRAUDULENTLY CONCEALED. APART FROM THE CASES STIPULATED ABOVE, BOSCH SI SHALL BE LIABLE WITHOUT LIMITATION FOR
 * INTENT OR GROSS NEGLIGENCE, FOR INJURIES TO LIFE, BODY OR HEALTH AND ACCORDING TO THE PROVISIONS OF THE GERMAN
 * PRODUCT LIABILITY ACT (PRODUKTHAFTUNGSGESETZ). THE SCOPE OF A GUARANTEE GRANTED BY BOSCH SI SHALL REMAIN UNAFFECTED
 * BY LIMITATIONS OF LIABILITY. IN ALL OTHER CASES, LIABILITY OF BOSCH SI IS EXCLUDED. THESE LIMITATIONS OF LIABILITY
 * ALSO APPLY IN REGARD TO THE FAULT OF VICARIOUS AGENTS OF BOSCH SI AND THE PERSONAL LIABILITY OF BOSCH SI'S EMPLOYEES,
 * REPRESENTATIVES AND ORGANS.
 */

#include "settings.h"

byte espMacAddress[6];

WiFiClient wifi;

char uri_telemetry[512];
char uri_event[512];
char payload_topic[512];

bool prevPIR = false;
bool prevButton = false;

unsigned long lastMillis = 0;

float getVcc () {
  return ESP.getVcc() / 1000.0;
}

int getPIR() {
  return digitalRead(PIN_PIR);
}

bool getButton() {
  return !digitalRead(PIN_BUTTON);
}

void setup() {
  Serial.begin(115200);

  Serial.println();
  Serial.println();
  Serial.println("--- esp8266 example starting --- ");
  logmsg("Reset reason", ESP.getResetReason());
  Serial.println();

  // ---- HW Setup ---- 
  pinMode(PIN_BUTTON, INPUT);
  init_sensors();

  // ---- Wifi Setup ----
  WiFi.macAddress(espMacAddress);

  logmsg("MAC address", "");
  Serial.printf("%02x:%02x:%02x:%02x:%02x:%02x\n",
    espMacAddress[0],
    espMacAddress[1],
    espMacAddress[2],
    espMacAddress[3],
    espMacAddress[4],
    espMacAddress[5]);

  sprintfMacAddress(uri_telemetry, template_uri_telemetry, espMacAddress);
  sprintfMacAddress(uri_event, template_uri_event, espMacAddress);
  sprintfMacAddress(payload_topic, template_payload_topic, espMacAddress);

  logmsg("Wifi", "Connecting");
  WiFi.begin(wifi_ssid, wifi_pass);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println();
  logmsg("Wifi", "Connected, IP adress:");
  Serial.println(WiFi.localIP());    

  init_hono();
}

void loop() {
  loop_sensors();
  
  bool pir = getPIR();
  if (prevPIR != pir) {
    logmsg("PIR", "Value changed to ");
    Serial.println(pir);
    prevPIR = pir;
    publish_pir_event(pir);
  }

  bool button = getButton();
  if (prevButton != button) {
    logmsg("Button", "Value changed to ");
    Serial.println(button);
    prevButton = button;
    publish_button_event(button);
  }

  if (millis() - lastMillis > sensorUpdateRateMS) {
    lastMillis = millis();
    publish_sensors();
  }  
}


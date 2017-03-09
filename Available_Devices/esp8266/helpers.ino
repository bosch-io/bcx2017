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

void logmsg(const char * component, const String message) {
  Serial.printf("[%s] ", component);
  Serial.print(message);
}

void sprintfMacAddress(char * target, const char * tmpl, const byte * macAddress) {
  sprintf(target, tmpl, macAddress[0], macAddress[1], macAddress[2], macAddress[3], macAddress[4], macAddress[5]);
}

void publish_json(const char * title, const char * uri, JsonObject& root) {
  char buffer[200];

  root.printTo(buffer, sizeof(buffer));
  Serial.printf("[%s] HTTP PUT %s\n%s\n", title, uri, buffer);
  int result = http.put(uri, buffer);
  Serial.printf("[%s] HTTP PUT result: %i\n", title, result);
}

void publish_sensors() {
  StaticJsonBuffer<512> jsonBuffer;
  JsonObject& root = jsonBuffer.createObject();
  root["topic"] = payload_topic;
  root["path"] = "/features";
  JsonObject& value = root.createNestedObject("value");
  JsonObject& vcc = value.createNestedObject("vcc");
  JsonObject& vcc_properties = vcc.createNestedObject("properties");
  vcc_properties["Voltage"] = getVcc();

  JsonObject& time = value.createNestedObject("time");
  JsonObject& time_properties = time.createNestedObject("properties");
  time_properties["Uptime"] = millis();

  publish_json("Sensors", uri_telemetry, root);
}

void publish_pir_event(int pirValue) {
  StaticJsonBuffer<512> jsonBuffer;
  JsonObject& root = jsonBuffer.createObject();
  root["topic"] = payload_topic;
  root["path"] = "/features";
  JsonObject& value = root.createNestedObject("value");
  JsonObject& pir = value.createNestedObject("pir");
  JsonObject& pir_properties = pir.createNestedObject("properties");
  pir_properties["state"] = pirValue;

  // Not strictly necessary, just to aid in debugging
  JsonObject& time = value.createNestedObject("time");
  JsonObject& time_properties = time.createNestedObject("properties");
  time_properties["Uptime"] = millis();

  publish_json("PIR", uri_event, root);
}

void publish_button_event(int buttonValue) {
  StaticJsonBuffer<512> jsonBuffer;
  JsonObject& root = jsonBuffer.createObject();
  root["topic"] = payload_topic;
  root["path"] = "/features";
  JsonObject& value = root.createNestedObject("value");
  JsonObject& button = value.createNestedObject("button");
  JsonObject& button_properties = button.createNestedObject("properties");
  button_properties["state"] = buttonValue;

  // Not strictly necessary, just to aid in debugging
  JsonObject& time = value.createNestedObject("time");
  JsonObject& time_properties = time.createNestedObject("properties");
  time_properties["Uptime"] = millis();

  publish_json("Button", uri_event, root);
}


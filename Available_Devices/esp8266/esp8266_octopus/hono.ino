
#include "settings.h"

const char *templateHonoDeviceId = "esp8266.%02x%02x%02x%02x%02x%02x";
char honoDeviceId[15];

bool setupHono() {
    bool result = false;
    byte macAddress[6];

    WiFi.macAddress(macAddress);
    sprintf(honoDeviceId, templateHonoDeviceId, macAddress[0], macAddress[1], macAddress[2], macAddress[3], macAddress[4], macAddress[5]);

    Serial.printf("[Hono]         Registering device '%s' with Hono server %s:%i: ", honoDeviceId, honoHost, honoHttpPort);

    RestClient http = RestClient(honoHost, honoHttpPort);
    char uri[512];
    char buffer[512];
    sprintf(uri, "http://%s:%i/registration/%s", honoHost, honoHttpPort, honoTenant);
    sprintf(buffer, "device_id=%s&creater=self-registered", honoDeviceId);
    int statusCode = http.post(uri, buffer);
    if (statusCode == 201 || statusCode == 409) {
      result = true;
      Serial.println("OK");
    } else {
      Serial.printf("Statuscode %i - unknown result\n", statusCode);
    }

    publishStringEvent("startup", WiFi.localIP().toString());

    return result;
}

bool publish(HonoMessageType msgType, JsonObject& root) {
    bool result = false;
    char uri[512];
    char buffer[512];

    root["uptime"] = millis();
    root.printTo(buffer, sizeof(buffer));


    if (msgType == EVENT) {
        sprintf(uri, "http://%s:%i/event/%s/%s", honoHost, honoHttpPort, honoTenant, honoDeviceId);
    } else {
        sprintf(uri, "http://%s:%i/telemetry/%s/%s", honoHost, honoHttpPort, honoTenant, honoDeviceId);
    }    
  
    RestClient http = RestClient(honoHost, honoHttpPort);
    Serial.printf("[Hono]         HTTP PUT %s: ", uri);
    int statusCode = http.put(uri, buffer);
    if (statusCode == 202) {
        result = true;
        Serial.println("OK");
    } else if (statusCode == 503) {
        result = false;
        Serial.println("503 - Try again later");
    } else {
        Serial.printf("Statuscode %i - unknown result\n", statusCode);
    }
    Serial.print("               ");
    Serial.println(buffer);
  
    return result;
}

void publishBoolEvent(const char * key, bool value) {
    StaticJsonBuffer<512> jsonBuffer;
    JsonObject& root = jsonBuffer.createObject();
    root[key] = value;
    root["uptime"] = millis();

    publish(EVENT, root);
}

void publishStringEvent(const char * key, String value) {
    StaticJsonBuffer<512> jsonBuffer;
    JsonObject& root = jsonBuffer.createObject();
    root[key] = value;
    root["uptime"] = millis();

    publish(EVENT, root);
}

const char * getDeviceId() {
    return honoDeviceId;
}

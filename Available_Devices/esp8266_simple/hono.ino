
#include "settings.h"

const char *template_hono_device_id = "esp8266.%02x%02x%02x%02x%02x%02x";
char hono_device_id[15];

bool init_hono() {
    bool result = false;
    byte macAddress[6];

    WiFi.macAddress(macAddress);
    sprintf(hono_device_id, template_hono_device_id, macAddress[0], macAddress[1], macAddress[2], macAddress[3], macAddress[4], macAddress[5]);

    Serial.printf("[Hono] Registering device '%s' with Hono server %s:%i: ", hono_device_id,hono_host, hono_http_port);

    RestClient http = RestClient(hono_host, hono_http_port);
    char uri[512];
    char buffer[512];
    sprintf(uri, "http://%s:%i/registration/%s", hono_host, hono_http_port, hono_tenant);
    sprintf(buffer, "device_id=%s&creater=self-registered", hono_device_id);
    int statusCode = http.post(uri, buffer);
    if (statusCode == 201 || statusCode == 409) {
      result = true;
      Serial.println("OK");
    } else {
      Serial.printf("Statuscode %i - unknown result\n", statusCode);
    }
    
  return result;
}

bool publish(Hono_message_type msgType, const char * buffer) {
  bool result = false;

  char uri[512];
  if (msgType == EVENT) {
    sprintf(uri, "http://%s:%i/event/%s/%s", hono_host, hono_http_port, hono_tenant, hono_device_id);
  } else {
    sprintf(uri, "http://%s:%i/telemetry/%s/%s", hono_host, hono_http_port, hono_tenant, hono_device_id);
  }    
  
  RestClient http = RestClient(hono_host, hono_http_port);
  Serial.printf("[Hono] HTTP PUT %s:", uri);
  int statusCode = http.put(uri, buffer);
    if (statusCode == 202) {
      result = true;
      Serial.println("OK");
    } else {
      Serial.printf("Statuscode %i - unknown result\n", statusCode);
    }
  
  return result;
}





 

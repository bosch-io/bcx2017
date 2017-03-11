ESP8266WebServer server(80);

char mdns_hostname[100];
const char *template_hostname = "esp8266-%02x%02x%02x%02x%02x%02x";

void handleRoot() {
    String message = "Hi. I'm '";
    message += getDeviceId();
    message += "'.";
        
    server.send(200, "text/plain", message);
}

void handleNotFound(){
    String message = "404 Not Found\n\n";
    message += "URI: ";
    message += server.uri();
    message += "\nMethod: ";
    message += (server.method() == HTTP_GET)?"GET":"POST";
    message += "\nArguments: ";
    message += server.args();
    message += "\n";

    for (uint8_t i=0; i<server.args(); i++){
        message += " " + server.argName(i) + ": " + server.arg(i) + "\n";
    }
  
    server.send(404, "text/plain", message);
}


void setup_webserver() {
    byte macAddress[6];
    
    server.on("/", handleRoot);
    server.onNotFound(handleNotFound);
    server.begin();
    logmsgln("Webserver", "Listening on port 80");

    logmsg("Webserver", "My hostname is: ");
    Serial.println(WiFi.hostname());

    WiFi.macAddress(macAddress);
    sprintf(mdns_hostname, template_hostname, macAddress[0], macAddress[1], macAddress[2], macAddress[3], macAddress[4], macAddress[5]);

    if (MDNS.begin(mdns_hostname)) {
        MDNS.addService("http", "tcp", 80);
        logmsg("mDNS", "Visit me at ");
        Serial.printf("http://%s.local/\n", mdns_hostname);
    }
}


void loop_webserver() {
      server.handleClient();
}


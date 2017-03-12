ESP8266WebServer server(80);

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


void setupWebserver() {
    server.on("/", handleRoot);
    server.onNotFound(handleNotFound);
    server.begin();
    logmsgln("Webserver", "Listening on port 80");

    if (MDNS.begin(getHostName())) {
        MDNS.addService("http", "tcp", 80);
    }
}


void loopWebserver() {
    MDNS.update();
    server.handleClient();
}


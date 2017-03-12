ESP8266WebServer server(80);

void handleRoot() {
    String message = "Hi. I'm '";
    message += getDeviceId();
    message += "'.";
        
    server.send(200, "text/plain", message);
}

void handleSet() {
    String message = "Settings change\n\n";
    for (uint8_t i=0; i<server.args(); i++){
        if(server.argName(i).equals("humidityThreshold")) {
            message += "New humidity threshold: " + server.arg(i) + "\n";
            double th = server.arg(i).toFloat();
            Serial.println(th);
            setHumidityThreshold(th);
        } else {
            message += "Unknown argument: " + server.argName(i) + "\n";
        }
    }
  
    server.send(404, "text/plain", message);
    logmsg("Webserver", message);
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
    server.on("/set", handleSet);
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

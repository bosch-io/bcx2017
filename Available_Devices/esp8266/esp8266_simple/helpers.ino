void logmsg(const char * component, const String message) {    
    Serial.printf("[%s] ", component);
    for (int i = 0; i < 12-strlen(component); i++) {
        Serial.print(" ");
    }
    Serial.print(message);
}

void logmsgln(const char * component, const String message) {    
    Serial.printf("[%s] ", component);
    for (int i = 0; i < 12-strlen(component); i++) {
        Serial.print(" ");
    }
    Serial.println(message);
}


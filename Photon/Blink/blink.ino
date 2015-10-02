void setup()
{
    Serial.begin(9600);
    delay(2000);
    pinMode(D7, OUTPUT);
}

void loop()
{
    delay(1000);
    digitalWrite(D7, HIGH);
    Serial.println("ON");
    delay(2000);
    digitalWrite(D7, LOW);
    Serial.println("OFF");
}

#pragma SPARK_NO_PREPROCESSOR
#include "HTU21D.h"

//WEATHERSHIELD
HTU21D htu = HTU21D();

int interval = 2000; //default sensor measurement interval

void setup()
{
    Serial.begin(9600);
    pinMode(D7, OUTPUT);
    delay(2000);
    for(int i = 5;i > 0;i--)
    {
        Serial.println("Starting in " + String(i,DEC));
        delay(1000);
    }
    digitalWrite(D7, HIGH);
    htu.begin();

    /* THIS DID NOT WORK FOR ME...
  	while(! htu.begin()){
  	    Serial.println("HTU21D not found");
  	    delay(1000);
  	}*/
}

//Receive data in each loop to fire the onMessage method when data arrives
void loop()
{
      //Read sensor.
      float c = htu.readTemperature();
      float h = htu.readHumidity();

      //build JSON
      char payload[64];
      snprintf(payload, sizeof(payload), "{ \"Humidity\":\"%f\",\"Temperature\":\"%f\" }", h,c);
      Serial.println(payload);
      delay(interval);
}

#pragma SPARK_NO_PREPROCESSOR
#include <PhotonSocketClient.h>
#define SERVER_IP "hackzurich.cloudapp.net"
#define SERVER_PORT 8080

PhotonSocketClient client;

//When a message arrives we take action depending on the topic
void onMessage(PhotonSocketClient client, String data) {
  String topic = client.getValueAtIx(data,1);
  if(topic == "led")
  {
    String v = client.getValueAtIx(data,2);
    if(v == "true")
      digitalWrite(D7, HIGH);
    else
      digitalWrite(D7, LOW);
    client.send("photon","ledstate",v);
  }
}

//Connect to the XSockets server
void setup()
{
    Serial.begin(9600);
    delay(2000);
    pinMode(D7, OUTPUT);
    for(int i = 3;i > 0;i--)
    {
        Serial.println("Starting in " + String(i,DEC));
        delay(1000);
    }

    if(client.connect(SERVER_IP,SERVER_PORT)){
      client.openController("photon");
      client.setOnMessageDelegate(onMessage);
    }
    else{
      Serial.println("Could not open connection :(");
    }
}

//Receive data in each loop to fire the onMessage method when data arrives
void loop()
{
  client.receiveData();
  delay(250);
}

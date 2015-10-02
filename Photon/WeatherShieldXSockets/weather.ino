#pragma SPARK_NO_PREPROCESSOR
#include <PhotonSocketClient.h>
#include "HTU21D.h"
//COMMUNICATION
#define SERVER_IP "hackzurich.cloudapp.net" //"192.168.254.154" //
#define SERVER_PORT 8080
#define CLIENTID "230DBDC6-4F0D-4C83-BC5A-BBEC3E1E0A7E"
#define CONTROLLER "sensor"
PhotonSocketClient client;

//WEATHERSHIELD
HTU21D htu = HTU21D();
//Client information
char Org[] = "Team XSockets.NET";
char Name[] = "Zurich";
char Lat[] = "47.367347";
char Lng[] = "8.5500025";

int interval = 2000; //default sensor measurement interval
bool enabled = false; //Set to true when the server allows

//When a message arrives we take action depending on the topic
void onMessage(PhotonSocketClient client, String data) {
  String topic = client.getValueAtIx(data,1);
  Serial.println(data);
  if(topic == "ready"){
    enabled = true;
  }
  if(topic == "sensorinfo"){
    char clientInfo[128];
    snprintf(clientInfo, sizeof(clientInfo), "{ \"Lat\":\"%s\",\"Lng\":\"%s\",\"Name\":\"%s\",\"Organization\":\"%s\" }", Lat,Lng,Name,Org);
    client.send(CONTROLLER,"ci",clientInfo);
  }
  if(topic == "on"){
    digitalWrite(D7, HIGH);
    enabled = true;
  }
  if(topic == "off"){
    digitalWrite(D7, LOW);
    enabled = false;
  }
  if(topic == "interval"){
    interval = client.getValueAtIx(data,2).toInt();
  }
}

void onConnected(){
  //Socket is open, now open the controller.
  client.openController(CONTROLLER);
}

bool ensureConnection(){
  if(client.connected()) return true;
  Serial.println("connecting...");
  return client.connect(SERVER_IP,SERVER_PORT);
}

//Connect to the XSockets server
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

  	delay(2000);
    client.SetClientId(CLIENTID);
    client.setOnMessageDelegate(onMessage);
    client.setOnConnectedDelegate(onConnected);
    ensureConnection();
    //Wait for the serever to send "ready"!
    while(!enabled){
      client.receiveData();
      Serial.println("receiving...");
      delay(500);
    }
}

//Receive data in each loop to fire the onMessage method when data arrives
void loop()
{
  if(ensureConnection()){
    //Connected, we can read sensor and send/receive data

    client.receiveData();

    if(enabled){
      //Read sensor.
      float c = htu.readTemperature();
      float h = htu.readHumidity();

      //build JSON
      char payload[64];
      snprintf(payload, sizeof(payload), "{ \"Humidity\":\"%f\",\"Temperature\":\"%f\" }", h,c);

      //send to XSockets
      client.send("sensor","wd", payload);
      Serial.println(payload);
    }
  }
  delay(interval);
}

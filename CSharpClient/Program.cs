using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = new XSockets.Client40.XSocketClient("ws://hackzurich.cloudapp.net:8080", "http://localhost", "sensor");
            conn.QueryString.Add("sensortype", "photon");
            conn.OnConnected += (e, i) => { Console.WriteLine("Connected"); };

            var sensor = conn.Controller("sensor");

            sensor.On("ready", () =>
            {
                while (true)
                {
                    var d = new SensorData()
                    {
                        Humidity = 2.334,
                        Temperature = 23.000,
                    };

                    sensor.Invoke("wd", d);
                    System.Threading.Thread.Sleep(5000);
                }
            });

            sensor.OnOpen += (a, b) =>
            {


                var d = new SensorClientData()
                {
                    Lat = 47.367347,
                    Lng = 8.5500025,
                    Name = "Demo",
                    Organization = "XSockets",
                    SensorId = conn.PersistentId.ToString()
                };

                sensor.Invoke("ci", d);
                System.Threading.Thread.Sleep(3000);

            };

            //monitor.OnOpen += (e, i) => {
            //    Console.WriteLine("Monitor Controller Opened");
            //};
            //monitor.On<SensorClientData>("wd", (data) => {
            //    Console.WriteLine("Id:   {0}",data.SensorId);
            //    Console.WriteLine("Org:  {0}", data.Organization);
            //    Console.WriteLine("Name: {0}", data.Name);
            //    Console.WriteLine("Tmp:  {0}", data.Temperature);
            //    Console.WriteLine("Hum:  {0}", data.Humidity);
            //    Console.WriteLine("Lat:  {0}", data.Lat);
            //    Console.WriteLine("Lng:  {0}", data.Lng);
            //});

            conn.Open();

            Console.ReadLine();
        }
    }
}

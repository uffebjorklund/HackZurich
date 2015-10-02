using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Microsoft.Samples.Kinect.DiscreteGestureBasics
{
    public enum LEDState
    {
        On, Off
    }
    public static class XSocketsCommands
    {
        public static LEDState LEDState { get; set; }

        public static void Command(Dictionary<JointType, Point> jointPoints, Body body)
        {
            if (!MainWindow.xsocketClient.IsConnected) return;
            if (jointPoints[JointType.HandLeft].Y < jointPoints[JointType.ShoulderLeft].Y && jointPoints[JointType.HandRight].Y < jointPoints[JointType.ShoulderRight].Y)
            {
                //Maybe we need to send a message...
                if (body.HandLeftState == HandState.Open && body.HandRightState == HandState.Open)
                {
                    if (LEDState == LEDState.Off)
                    {
                        //Turn on LED
                        LEDState = LEDState.On;
                        System.Diagnostics.Debug.WriteLine("TURN LED ON AND START SENDING");
                        MainWindow.xsocketClient.Controller("monitor").Invoke("command", "on");
                    }
                }
                if (body.HandLeftState == HandState.Closed && body.HandRightState == HandState.Closed)
                {
                    if (LEDState == LEDState.On)
                    {
                        //Turn off LED
                        LEDState = LEDState.Off;
                        System.Diagnostics.Debug.WriteLine("TURN LED OFF AND STOP SENDING");
                        MainWindow.xsocketClient.Controller("monitor").Invoke("command", "off");
                    }
                }
            }
        }
    }
}

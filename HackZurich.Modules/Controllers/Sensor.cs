using XSockets.Core.XSocket;
using XSockets.Core.Common.Socket.Attributes;
using HackZurich.Modules.Model;

namespace HackZurich.Modules.Controllers
{
    //PuttyProtocol?PersistentId=1382a1a1-8443-4727-8a9d-6f0206dd66d8&clienttype=kinect
    public partial class Sensor : XSocketController
    { 
        public SensorType SensorType { get; set; }

        /// <summary>
        /// Static information about the sensor.
        /// No need to send that every time from the sensor. Once is enough!
        /// We have a full duplex connection, so use state!
        /// </summary>
        [PersistentProperty]
        public SensorInfo SensorInfo { get; set; }
    }
}

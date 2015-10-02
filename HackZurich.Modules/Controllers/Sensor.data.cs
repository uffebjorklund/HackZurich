using HackZurich.Modules.Controller;
using HackZurich.Modules.Model;
using System.Threading.Tasks;
using XSockets.Core.Common.Socket;
using XSockets.Core.Common.Socket.Event.Attributes;
using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;
using XSockets.Plugin.Framework;

namespace HackZurich.Modules.Controllers
{
    public partial class Sensor : XSocketController
    {         
        /// <summary>
        /// Sensors will send weatherinfo to this method        
        /// </summary>
        /// <param name="data"></param>
        [ControllerEvent("wd")]
        public async Task OnWeatherData(SensorData data)
        {
            if (this.SensorInfo == null) return;
            var o = new SensorClientData(this.SensorInfo, data);

            //Tell monitors about the new data (not the sensors)
            await this.InvokeTo<Monitor>(p => p.ClientType == ClientType.Monitor && p.TempThreshold < o.Temperature, o, "wd");

            await this.ScaleOut(o, "swd");

            //Store data on Azure Storage
            await this.StorageSet(o.SensorId, o);
        }

        [ControllerEvent("swd")]
        public async Task OnWeatherData(SensorClientData data)
        {
            await this.InvokeTo<Monitor>(p => p.ClientType == ClientType.Monitor && p.TempThreshold < data.Temperature, data, "wd");
        }

        /// <summary>
        /// Set specific info about the client. Location, Name etc
        /// Sent by the sensor when connected
        /// </summary>
        /// <param name="clientInfo"></param>
        [ControllerEvent("ci")]
        public async Task SetClientInfo(SensorInfo sensorInfo)
        {
            this.SensorInfo = sensorInfo;
            this.SensorInfo.SensorId = this.PersistentId;

            await this.SendReady();

            //Write instantly to storage since arduino boards and socket does not detect disconnect well, and we do not have a heartbeat...
            await Composable.GetExport<IPersistentPropertyStorage>().WriteToPropertyStorage(this);
        }


        [ControllerEvent("sci")]
        public async Task SendReady(SensorInfo si)
        {
            await this.InvokeTo<Monitor>(p => p.ClientType == ClientType.Monitor, this.SensorInfo, "ci");
        }
    }
}
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
    //PuttyProtocol?PersistentId=1382a1a1-8443-4727-8a9d-6f0206dd66d8
    public partial class Sensor : XSocketController
    {
        /// <summary>
        /// Fires when the controller is opened
        /// </summary>
        /// <returns></returns>
        public override async Task OnOpened()
        {
            //Ask for client info it it is missing, otherwise tell the client to start sendind data
            if (this.SensorInfo == null)
            {
                await this.Invoke("sensorinfo");
            }
            else
            {
                await this.SendReady();
            }
        }        

        private async Task SendReady()
        {
            //Notify all monitoring client
            await this.InvokeTo<Monitor>(p => p.ClientType == ClientType.Monitor, this.SensorInfo, "ci");

            await this.ScaleOut(this.SensorInfo, "sci");
            //Notify sensor about ready state
            await this.Invoke("ready");
        }
    }
}
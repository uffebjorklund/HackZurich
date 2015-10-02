
using HackZurich.Modules.Controllers;
using HackZurich.Modules.Model;
using System.Threading.Tasks;
using XSockets.Core.Common.Socket.Event.Attributes;
using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;

namespace HackZurich.Modules.Controller
{
    /// <summary>
    /// Implement/Override your custom actionmethods, events etc in this real-time MVC controller
    /// </summary>
    public partial class Monitor : XSocketController
    {
        /// <summary>
        /// Pass any command to the sensors
        /// For example, on or off to control the onboard LED
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public async Task Command(string cmd)
        {
            //Send to all sensors of type Photon
            await this.InvokeTo<Sensor>(p => p.SensorType == SensorType.Photon, cmd);
            //Send to all monitoring clients
            await this.InvokeTo<Monitor>(p => p.ClientType == ClientType.Monitor, cmd);

            await this.ScaleOut(cmd, "scmd");
        }

        [ControllerEvent("scmd")]
        public async Task ScaleCommand(string cmd)
        {
            //Send to all sensors of type Photon
            await this.InvokeTo<Sensor>(p => p.SensorType == SensorType.Photon, cmd);
            //Send to all monitoring clients
            await this.InvokeTo<Monitor>(p => p.ClientType == ClientType.Monitor, cmd);
        }
    }
}
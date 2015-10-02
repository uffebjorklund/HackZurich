using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;
using System.Threading.Tasks;

namespace HackZurich.Modules.Controllers
{
    /// <summary>
    /// Implement/Override your custom actionmethods, events etc in this real-time MVC controller
    /// </summary>
    public class Photon : XSocketController
    {
        public async Task State(bool b)
        {
            await this.InvokeToAll<Led>(b,"state");
        }
    }
}

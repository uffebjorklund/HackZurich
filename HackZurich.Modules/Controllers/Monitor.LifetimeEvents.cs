using HackZurich.Modules.Model;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using XSockets.Core.Common.Enterprise;
using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;
using XSockets.Plugin.Framework;

namespace HackZurich.Modules.Controller
{
    /// <summary>
    /// Implement/Override your custom actionmethods, events etc in this real-time MVC controller
    /// </summary>
    public partial class Monitor : XSocketController
    {
        public override async Task OnOpened()
        {
            //Check if ClientType was passed in
            if (this.HasParameterKey("clienttype"))
            {
                this.ClientType = this.GetParameter("clienttype").ToEnum<ClientType>();
                if (this.ClientType == ClientType.Kinect)
                {
                    await this.InvokeTo(p => p.ClientType == ClientType.Monitor, true, "kinect");
                    await this.ScaleOut(true, "kinect");
                }
                //Just for showing that we are on separate servers when scaling.
                if (this.ClientType == ClientType.Monitor)
                {
                    await this.Invoke(SID, "scaleoutid");
                }
            }
        }

        public override async Task OnClosed()
        {
            if (this.ClientType == ClientType.Kinect)
            {
                await this.InvokeTo(p => p.ClientType == ClientType.Monitor, false, "kinect");

                await this.ScaleOut(false, "kinect");
                //Manua scaleout since only messages going in is scaled by default.
                //await Composable.GetExport<IXSocketsScaleOut>().Publish(XSockets.Core.Common.Protocol.MessageDirection.Out, new Message(false, "kinect", "monitor"));
            }
        }

        public async Task Kinect(bool state)
        {
            await this.InvokeTo(p => p.ClientType == ClientType.Monitor, state, "kinect");
        }
    }
}
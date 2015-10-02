using HackZurich.Modules.Model;
using System;
using XSockets.Core.Common.Socket;
using XSockets.Core.Common.Socket.Attributes;
using XSockets.Core.XSocket;
using XSockets.Plugin.Framework;

namespace HackZurich.Modules.Controller
{ 
// PuttyProtocol?PersistentId=1382a1a1-8443-4727-8a9d-6f0206dd66d8&clienttype=kinect

    /// <summary>
    /// Implement/Override your custom actionmethods, events etc in this real-time MVC controller
    /// </summary>
    public partial class Monitor : XSocketController
    {
        private static readonly Guid SID = Composable.GetExport<IXSocketServerContainer>().ContainerId;
        /// <summary>
        /// Both sensors and monitoring client may connect to the controller, this can separate them.
        /// </summary>        
        public ClientType ClientType { get; set; }

        /// <summary>
        /// Only send data to monitoring client when temp is over the threshold
        /// Only for temp... humidity goes when temp goes... (LazyDev)
        /// </summary>
        [PersistentProperty]
        public int TempThreshold { get; set; }        
    }
}
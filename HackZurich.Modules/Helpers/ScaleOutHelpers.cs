using System;
using System.Threading.Tasks;
using XSockets.Core.Common.Enterprise;
using XSockets.Core.Common.Socket;
using XSockets.Core.XSocket.Model;
using XSockets.Plugin.Framework;

namespace HackZurich.Modules.Helpers
{
    public static class ScaleOutHelpers
    {
        public static async Task Scale<T>(this T controller, object o, string topic) where T : class, IXSocketController
        {
            var m = new Message(o,topic,controller.Alias);
            await Composable.GetExport<IXSocketsScaleOut>().Publish(XSockets.Core.Common.Protocol.MessageDirection.Out,m);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSockets.Core.Common.Globals;
using XSockets.Core.Common.Protocol;
using XSockets.Core.Common.Socket.Event.Arguments;
using XSockets.Core.Common.Socket.Event.Interface;
using XSockets.Core.Common.Utility.Serialization;
using XSockets.Core.XSocket.Model;
using XSockets.Plugin.Framework;

namespace HackZurich.Modules.Protocols
{
    public class PhotonProtocolProxy : IProtocolProxy
    {
        private IXSocketJsonSerializer JsonSerializer { get; set; }

        public PhotonProtocolProxy()
        {
            JsonSerializer = Composable.GetExport<IXSocketJsonSerializer>();
        }
        public IMessage In(IEnumerable<byte> payload, MessageType messageType)
        {
            var data = Encoding.UTF8.GetString(payload.ToArray());
            //Sanity checks...
            if (data.Length == 0) return null;
            var d = data.Split('|');
            if (d.Length != 3) return null;

            switch (d[1])
            {
                //Not needed if you do not use the storage features
                case Constants.Events.Storage.Set:
                    var kv = d[2].Split(',');
                    return new Message(new XStorage { Key = kv[0], Value = kv[1] }, Constants.Events.Storage.Set, d[0], JsonSerializer);
                case Constants.Events.Storage.Get:
                    return new Message(new XStorage { Key = d[2] }, Constants.Events.Storage.Get, d[0], JsonSerializer);
                case Constants.Events.Storage.Remove:
                    return new Message(new XStorage { Key = d[2] }, Constants.Events.Storage.Remove, d[0], JsonSerializer);
                default:
                    //Only thing needed if not using storage features
                    return new Message(d[2], d[1], d[0]);

            }
        }

        public byte[] Out(IMessage message)
        {
            //Just for not writing JSON back, but it does not really matter in this case
            if (message.Topic == Constants.Events.Controller.Opened)
            {
                var c = this.JsonSerializer.DeserializeFromString<ClientInfo>(message.Data);
                var d = string.Format("PI:{0},CI:{1}", c.PersistentId, c.ConnectionId);
                message = new Message(d, message.Topic, message.Controller);
            }
            var result = new List<byte>();
            result.Add(0x00);
            result.AddRange(Encoding.UTF8.GetBytes(string.Format("{0}|{1}|{2}", message.Controller, message.Topic, message.Data)));
            result.Add(0xff);
            return result.ToArray();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSockets.Core.Common.Protocol;
using XSockets.Plugin.Framework;
using XSockets.Plugin.Framework.Attributes;
using XSockets.Protocol;

namespace HackZurich.Modules.Protocols
{
    [Export(typeof(IXSocketProtocol), Rewritable = Rewritable.No)]
    public class PhotonProtocol : XSocketProtocol
    {
        public PhotonProtocol()
        {
            this.ProtocolProxy = new PhotonProtocolProxy();
        }
        public override async Task<bool> Match(IList<byte> handshake)
        {
            var m = Encoding.UTF8.GetString(handshake.ToArray());
            return m.StartsWith("PhotonProtocol");
        }

        public override bool CanDoHeartbeat()
        {
            return false;
        }

        public override string HostResponse
        {
            get { return "welcome"; }
        }

        public override IXSocketProtocol NewInstance()
        {
            return new PhotonProtocol();
        }
    }
}
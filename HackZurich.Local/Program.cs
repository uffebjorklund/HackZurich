using System;
using XSockets.Core.Common.Socket;
using XSockets.Core.Common.Utility.Serialization;
using XSockets.Plugin.Framework;

namespace HackZurich.Local
{
    class Program
    {
        static void Main(string[] args)
        {
            XSockets.Core.XSocket.PersistentPropertyStorage.TimeoutInSeconds = 300;

            Composable.AddErrorAction((e) => {
                Console.WriteLine("Ex " + e.Message);
            });                      

            using (var container = Composable.GetExport<IXSocketServerContainer>())
            {
                container.Start();
                Console.ReadLine();
            }
        }
    }
}

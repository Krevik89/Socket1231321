using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socket1231321
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //////////////////////    Broadcast      /////////////////////////////////////////
            Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse("192.168.255.255"), 1234);
            socket.Connect(iPEnd);

            string message = "Hello world";
            socket.Send(Encoding.Default.GetBytes(message));
            //////////////////////////////////////////////////////////////////////////////////

            //////////////////////    Multicast      /////////////////////////////////////////
            Socket socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket1.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);

           

        }
    }
}

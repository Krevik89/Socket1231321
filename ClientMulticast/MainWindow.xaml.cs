using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientMulticast
{
    delegate void AppendText(string text);

    public partial class MainWindow : Window
    {
        void AppendTextProc(string text)
        {
            outputData.Text = text;
        }

        void Listener()
        {
            while (true)
            {
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint ipen =new IPEndPoint(IPAddress.Any, 4567);
                soc.Bind(ipen);

                IPAddress ip = IPAddress.Parse("224.5.5.5");
                soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                    new MulticastOption(ip, IPAddress.Any));

                byte[] buffer = new byte[1024];
                soc.Receive(buffer);
                Dispatcher.Invoke(new AppendText(AppendTextProc), Encoding.Default.GetString(buffer));
                soc.Close();
            }
        }

        Thread thread;

        public MainWindow()
        {
            InitializeComponent();
            thread = new Thread(new ThreadStart(Listener));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}

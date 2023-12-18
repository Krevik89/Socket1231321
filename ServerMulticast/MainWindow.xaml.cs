using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
using System.Threading;

namespace ServerMulticast
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string message = "hello world";
        static int interval = 1800;

        static void Send()
        {
            while (true)
            {
                Thread.Sleep(interval);
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                soc.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.MulticastTimeToLive, 2);
                IPAddress dest = IPAddress.Parse("224.5.5.5");
                soc.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.AddMembership,new MulticastOption(dest));

                IPEndPoint ipend = new IPEndPoint(dest,4567);
                soc.Connect(ipend);
                soc.Send(Encoding.Default.GetBytes(message));
                soc.Close();
            }
        }

        Thread Sender = new Thread(new ThreadStart(Send));


        public MainWindow()
        {
            InitializeComponent();
            Sender.IsBackground = true;
            Sender.Start();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            message=textBox1.Text;
        }
    }
}

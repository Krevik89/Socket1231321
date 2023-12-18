using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;


namespace MulticastChat
{
    
    public partial class MainWindow : Window
    {
        UdpClient udpClient;
        IPEndPoint iPend;
        void Listener()
        {
            udpClient = new UdpClient();
            IPAddress multiAddress = IPAddress.Parse("224.5.5.5");
            udpClient.JoinMulticastGroup(multiAddress);
            iPend  = new IPEndPoint(multiAddress,4567);
            udpClient.Connect(iPend);

            Thread reciveThread = new Thread(() => 
            {           
                while (true)
                {
                    byte[] data = udpClient.Receive(ref iPend);
                    //inputText.Text = Encoding.Default.GetString(data);
                }
            });
            reciveThread.Start();
            byte[] senData = Encoding.Default.GetBytes("Hello world =)");
            udpClient.Send(senData, senData.Length, iPend);

            reciveThread.Join();
            udpClient.DropMulticastGroup(multiAddress);
            udpClient.Close();
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

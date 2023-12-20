using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Threading;


namespace MulticastChat
{
    
    public partial class MainWindow : Window
    {
        private const int _port = 4567;
        private UdpClient udpClient;
        private Thread reciveThread;
        

        void Listener()
        {
            while (true)
            {
                IPEndPoint iPend = null;
                byte[] data; 
                try
                {

                  data = udpClient.Receive(ref iPend);

                }catch(SocketException ex)
                {
                    return;
                }
                string message = Encoding.Default.GetString(data);

                Dispatcher.Invoke(() => inputText.Text = message);
            }          
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] data = Encoding.Default.GetBytes(outputText.Text);
            udpClient.Send(data,data.Length, "224.5.5.5", _port);

            outputText.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            udpClient = new UdpClient(_port);
            IPAddress multiAddress = IPAddress.Parse("224.5.5.5");
            udpClient.JoinMulticastGroup(multiAddress);

            reciveThread = new Thread(Listener);
            reciveThread.Start();

        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            udpClient.Close();
            reciveThread?.Join();
        }
    }
}

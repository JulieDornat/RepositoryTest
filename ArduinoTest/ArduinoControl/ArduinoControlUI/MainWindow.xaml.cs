using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.Threading;

namespace ArduinoControlUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int clickcount1;
        string Response;

        System.IO.Ports.SerialPort ArduinoPort = new();

        public MainWindow()
        {
            InitializeComponent();
            OnBtn.IsEnabled = false;
            OffBtn.IsEnabled = false;
        }

        // Marche
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ArduinoPort.WriteLine("on");
                Thread.Sleep(10);
                Response = ArduinoPort.ReadLine();
                if (Response.Contains("on"))
                {
                    OffBtn.IsEnabled = true;
                    OnBtn.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ConnectButtonClick(object sender, RoutedEventArgs e)
        {
            string pn = PortBox.SelectedValue.ToString();
            
            try
            {
                if (!ArduinoPort.IsOpen)
                {
                    ArduinoPort.PortName = pn;
                    ArduinoPort.Open();
                    OffBtn.IsEnabled = false;
                    OnBtn.IsEnabled = true;
                }
                else
                {
                    ArduinoPort.Close();
                    OffBtn.IsEnabled = true;
                    OnBtn.IsEnabled = false;
                }
            }
            catch
            {
                Console.WriteLine("Erreur lors de la connexion/deconnexion");
            }
            
        }

        // Arrêt
        private void OffButtonClick(object sender, RoutedEventArgs e)
        {

            try
            {
                ArduinoPort.WriteLine("off");
                Thread.Sleep(10);
                Response = ArduinoPort.ReadLine();
                if (Response.Contains("off"))
                {
                    OffBtn.IsEnabled = false;
                    OnBtn.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Baud rate = vitesse de transmission.
            ArduinoPort.BaudRate = (115200);
            ArduinoPort.ReadTimeout = (2000);
            ArduinoPort.WriteTimeout = (2000);

        }
    }
}
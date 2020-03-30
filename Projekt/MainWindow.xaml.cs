using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.AspNet.SignalR.Client;


namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //server connection variable
        private HubConnection _connection;
        public MainWindow()
        {
            InitializeComponent();
            _connection = new HubConnection("https://kopernikus20200210091600.azurewebsites.net/ship"); //giving the connection the URL
            await connection.Start();  
               
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            _connection.On<string>("Connected",(connectionid) =>
            (//Connection check 
            tbMain.Text = connectionid
                ));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Shut_Down_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

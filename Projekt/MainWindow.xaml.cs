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
using Microsoft.AspNetCore.SignalR.Client;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //server connection variable
        HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder()
                .WithUrl("https://kopernikus20200210091600.azurewebsites.net/ship")//giving the connection the URL
                 .Build();
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            //Connection Check


        }


        private async void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.StartAsync();
                connect_test.Text=("Connection started");
                Connect.IsEnabled = false;
                if (connection.State == HubConnectionState.Connected)
                {
                    connect_test.Text = "Connected";
                }
                else
                    connect_test.Text = "NOT Connected";
            }
            catch (Exception ex)
            {
                connect_test.Text = (ex.Message);
            }

            //Connect c = new Connect();
            //c.Show();
            //this.Close();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Shut_Down_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ön lecsatlakozott a szerverről.");
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            game g = new game();
            g.Show();
            this.Close();
        }
    }
}

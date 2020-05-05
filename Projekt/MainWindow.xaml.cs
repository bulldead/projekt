using System;
using System.Collections.Generic;
using System.IO;
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
using System.Threading;


namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //server connection variable
        
        public MainWindow()
        {
            InitializeComponent();
            counter();
            connect_test.Text = ""; 
        }
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            connect_test.Text = "";
            Connect.IsEnabled = false;
            connect_test.Text = "Connecting to server. Please wait.";
            
            Game gamewindow = new Game();
            gamewindow.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void counter()
        {
            var path = "counts.txt";
            int counter;

            string counter1 = File.ReadAllText("counts.txt");

           
            counter = Convert.ToInt32(counter1);
            counter++;
            counting.Text = "Tests run: "+ Convert.ToString(counter) + ", and still counting...";

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (var tofile = new StreamWriter(fs))
                {
                    tofile.WriteLine(counter.ToString());
                    tofile.Close();
                }
            }

        }
        
    }
}

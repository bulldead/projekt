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

namespace Projekt
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
   
    public partial class Game : Window
    {
        HubConnection myhub;
        public Game()
        {
            InitializeComponent();
            kiirat.Text = "Waiting on server response.";
            connect();
            //kiiratas();
            
        }
        private async void connect()
        {
            myhub = new HubConnectionBuilder()
            .WithUrl("https://kopernikus20200210091600.azurewebsites.net/ship")//giving the connection the URL
            .Build();
            myhub.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await myhub.StartAsync();
            };
            
            try
            {                
                await myhub.StartAsync();
                //usermake();
                //connection Checking
                if (myhub.State == HubConnectionState.Connected)
                {
                    kiirat.Text = "Connected";
                    //new window open
                }
                else
                    kiirat.Text = "";
            }
            //If couldnt connect, exception msg int he text block
            catch (Exception ex)
            {
                kiirat.Text = (ex.Message);
            }
        }
        //private async void kiiratas()
        //{
        //    try
        //    {
        //        await myhub.
        //    }
        //    catch (Exception ex)
        //    {
        //        //change it after
        //        kiirat.Text = (ex.Message);
        //    }
        //}
        private void usermake()
        {
            User user = new User();
            user.Name = "Rikka";
            user.PublicId = 432;
            user.Group = 4242;
            user.PrivateId = 5201;
            string userdata = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            string path = @"user.json";
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (var tofile = new StreamWriter(fs))
                {
                    tofile.WriteLine(userdata.ToString());
                    tofile.Close();
                }
            }
        }
    }
}

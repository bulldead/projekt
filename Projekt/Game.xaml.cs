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
using System.Drawing;
using System.Threading;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>

    public partial class Game : Window
    {
        HubConnection myhub;
        public List<string> messagesList = new List<string>();
        public Game()
        {
            InitializeComponent();
            kiirat.Text = "Waiting on server response.";
            connect();
            usermake();
            broadcast();
            keringes();
            shoot();
            //shipmove();
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


                //connection Checking
                if (myhub.State == HubConnectionState.Connected)
                {
                    kiirat.Text = "Connected";
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
        private async void usermake()
        {
            //User data
            User user = new User();
            user.Name = "Rikka";
            user.PublicId = 432;
            user.Group = 4242;
            user.PrivateId = 5201;
            //serialize to json
            string userdata = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            //write to test json file
            string path = @"user.json";
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (var tofile = new StreamWriter(fs))
                {
                    tofile.WriteLine(userdata.ToString());
                    tofile.Close();
                }
            }
            try
            {
                //await myhub.InvokeAsync("user", userdata);
                await myhub.SendAsync("user", userdata);
            }
            //Exception message if something went wrong.
            catch (Exception ex)
            {
                kiirat.Text = (ex.Message);
            }

        }
        private void broadcast()
        {
            //broadcast message deserialization and usage
            myhub.On<string, string>("ReceiveMessage", (user, message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    //break broadcast message to two parts
                    var newMessage = $"{user}: {message}";
                    var message_begining = $"{user}";
                    //Adding to list
                    messagesList.Add(newMessage);
                    //convert it to string
                    string message_part = Convert.ToString(newMessage);
                    string message_part2 = Convert.ToString(user);
                    string path = @"test.txt";
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        using (var tofile = new StreamWriter(fs))
                        {
                            tofile.WriteLine(newMessage.ToString());
                            tofile.WriteLine(user.ToString());
                            tofile.Close();
                        }
                    }
                });
            });
        }
        private void shipmove()
        {

            //RotateTransform transform = FriendlyShip.RenderTransform as RotateTransform;
            //transform.Angle = 90.0;
            //FriendlyShip.RenderTransform = transform;

        }
        private void keringes()
        {
            //default orbiting
            var sb = FindResource("ellipseSB") as Storyboard;
            if (sb != null) sb.Begin();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //exits to main screen
            game_exit();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        private async void game_exit()
        {
            //on exit sends private id
            User user = new User();
            user.PrivateId = 5201;
            string userdata_exit = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            try
            {
                await myhub.SendAsync("exit", userdata_exit);
            }
            //Exception message if something went wrong.
            catch (Exception ex)
            {
                kiirat.Text = (ex.Message);
            }
        }
        private async void shoot()
        {
            //Shoot with json code, -1 outside +1 inside shoot.
            User user = new User();
            user.PrivateId = 5201;
            //későbbi implementálásra:
            //if (kifelegomb)
            //{
            //    user.Shoot = -1;
            //}
            //if (befelegomb)
            //{
            //    user.Shtoot = 1;
            //}
            user.Shoot = 1;
            string userdata_shoot = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            try
            {
                await myhub.SendAsync("shoot", userdata_shoot);
            }
            //Exception message if something went wrong.
            catch (Exception ex)
            {
                kiirat.Text = (ex.Message);
            }
        }
    }
}

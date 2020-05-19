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
using System.Windows.Documents.DocumentStructures;

namespace Projekt

{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        HubConnection myhub;
        public List<string> messagesList = new List<string>();
        public List<Ship> ships_data = new List<Ship>();
        public Game()
        {
            InitializeComponent();
            kiirat.Text = "Waiting on server response.";
            connect();            
            usermake();
            broadcast();
            keringes();
            shipmove();
            shoot();


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
        private async void usermake()
        {
            //User data
            User user = new User();
            user.Name = "Rikka";
            user.PublicId = 43242;
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
                    string message_part1 = Convert.ToString(newMessage);
                    string message_part2 = Convert.ToString(user);
                    string path = @"test.txt"; 
                    string path_ships = @"test1.txt";
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        using (var tofile = new StreamWriter(fs))
                        {
                            tofile.WriteLine(newMessage.ToString());
                            tofile.WriteLine(user.ToString());
                            tofile.Close();
                        }
                    }
                    //Testing to get the data
                    ships_data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Ship>>(message);
                    //Player point writer
                    using (TextWriter tw = new StreamWriter(path_ships))
                    {
                        for (int i = 0; i < ships_data.Count; i++)
                        {   
                            tw.WriteLine(string.Format("Points: {0}", ships_data[i].Points.ToString()));
                            
                            if (i == 0)
                            {
                                Player1.Text = "Player1";
                                Player1_Score.Text = ships_data[i].Points.ToString();
                            }
                            if (i == 1)
                            {
                                Player2.Text = "Player2";
                                Player2_Score.Text= ships_data[i].Points.ToString();
                            }
                            if (i == 2)
                            {
                                Player3.Text = "Player3";
                                Player3_Score.Text = ships_data[i].Points.ToString();
                            }
                            if (i == 3)
                            {
                                Player4.Text = "Player4";
                                Player4_Score.Text = ships_data[i].Points.ToString();
                            }
                            if (i == 4)
                            {
                                Player5.Text = "Player5";
                                Player5_Score.Text = ships_data[i].Points.ToString();
                            }
                            if (i == 5)
                            {
                                Player6.Text = "Player6";
                                Player6_Score.Text = ships_data[i].Points.ToString();
                            }
                            if (i == 6)
                            {
                                Player7.Text = "Player7";
                                Player7_Score.Text = ships_data[i].Points.ToString();
                            }
                            if (i == 7)
                            {
                                Player8.Text = "Player8";
                                Player8_Score.Text = ships_data[i].Points.ToString();
                            }
                            if (i == 8)
                            {
                                Player9.Text = "Player9";
                                Player9_Score.Text = ships_data[i].Points.ToString();
                            }
                            if (i == 9)
                            {
                                Player10.Text = "Player10";
                                Player10_Score.Text = ships_data[i].Points.ToString();
                            }
                        }

                        //foreach (var item in ships_data)
                        //{
                           
                        //    tw.WriteLine(string.Format("Points: {0}", item.Points.ToString()));                            
                         
                        //}
                    }
                    
                });
            });
        }
        private async void shipmove()
        {

            User user = new User();
            user.PrivateId = 5201;
            //move testing
            //PathGeometry path = new PathGeometry();            
            //Pathmove.SetValue(Canvas.LeftProperty,400.0);
            //Pathmove.SetValue(Canvas.TopProperty, 40.0);

            if (true)//Still need the event to work. On pressing button 'A' moves closer to the center
            {
                user.Orbit = 1;
                string userdata_move = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                try
                {
                    await myhub.SendAsync("orbit", userdata_move);
                }
                //Exception message if something went wrong.
                catch (Exception ex)
                {
                    kiirat.Text = (ex.Message);
                }
            }            
            
            if (true)//Still need the event to work. On pressing button 'D' moves more far from the center
            {
                user.Orbit = -1;
                string userdata_move = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                try
                {
                    await myhub.SendAsync("orbit", userdata_move);
                }
                    //Exception message if something went wrong.
                catch (Exception ex)
                {
                        kiirat.Text = (ex.Message);
                }
            }
            
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
            if (true)//On pressing left arrow, the ship shoots to the center
            {
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
            if (true)//On pressing right arrow, the ship shoots outside
            {
                user.Shoot = -1;
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
}

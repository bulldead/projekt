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
using System.Windows.Markup;
using Path = System.Windows.Shapes.Path;
using Point = System.Windows.Point;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            user.PublicId = 4324;
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
                await myhub.InvokeAsync("SendMessage", "user", userdata);

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
                    string message_test = @"message_test.txt";
                    string ship_points = @"point_test.txt";
                    
                    using (FileStream fs = new FileStream(message_test, FileMode.Create))
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
                    using (TextWriter tw = new StreamWriter(ship_points))
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
                                Player2_Score.Text = ships_data[i].Points.ToString();
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

                    }
                   
                });
            });
        }
        private void keringes()
        {
            //default orbiting
            var sb = FindResource("ellipseSB") as Storyboard;
            if (sb != null) sb.Begin();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //exits to main screen and saves score
            string highscores = @"highscores.txt";
            using (TextWriter tw = new StreamWriter(highscores, true))
            {
                User user1 = new User();
                user1.PublicId = 4324;
                for (int i = 0; i < ships_data.Count; i++)
                {

                    string test123 = string.Format("PublicId: {0}", ships_data[i].PublicId.ToString());
                    if (test123 == "PublicId: 4324")
                    {
                        tw.WriteLine(string.Format("Points: {0}", ships_data[i].Points.ToString()));
                    }
                }
            }
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
                await myhub.SendAsync("SendMessage", "exit", userdata_exit);
            }
            //Exception message if something went wrong.
            catch (Exception ex)
            {
                kiirat.Text = (ex.Message);
            }
        }
        private void move_energy()
        {
            for (int i = 0; i < 10; i++)
            {
                Energybar.Value--;
            }
            Energy_Number.Text = Convert.ToString(Energybar.Value);
        }
        private void shoot_energy()
        {
            for (int i = 0; i < 3; i++)
            {
                Energybar.Value--;
            }
            Energy_Number.Text = Convert.ToString(Energybar.Value);
        }
        private async void move_left()
        {
            var pm = FindResource("path") as PathGeometry;
            pm.Clear();
            string a = "M 750,333 A 150,150 0 1 1 750,332.99";
            pm.AddGeometry(StreamGeometry.Parse(a));
            var sb = FindResource("ellipseSB") as Storyboard;
            if (sb != null) sb.Begin();
            User user = new User();
            user.Orbit = -1;
            user.PrivateId = 5201;
            string userdata_move = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            try
            {
                await myhub.SendAsync("SendMessage", "exit", userdata_move);
            }
            //Exception message if something went wrong.
            catch (Exception ex)
            {
                kiirat.Text = (ex.Message);
            }
            move_energy();
        }
        private async void move_right()
        {
            var pm = FindResource("path") as PathGeometry;
            pm.Clear();
            string a = "M 850,333 A 250,250 0 1 1 850,332.99";
            pm.AddGeometry(StreamGeometry.Parse(a));
            var sb = FindResource("ellipseSB") as Storyboard;
            if (sb != null) sb.Begin();
            User user = new User();
            user.Orbit = 1;
            user.PrivateId = 5201;
            string userdata_move = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            try
            {
                await myhub.SendAsync("SendMessage", "exit", userdata_move);
            }
            //Exception message if something went wrong.
            catch (Exception ex)
            {
                kiirat.Text = (ex.Message);
            }
            move_energy();
        }
        private async void shoot_left()
        {
            //user.Shoot = -1;
            User user = new User();
            user.Shoot = -1;
            user.PrivateId = 5201;
            string userdata_shoot = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            try
            {
                await myhub.SendAsync("SendMessage", "shoot", userdata_shoot);
            }
            //Exception message if something went wrong.
            catch (Exception ex)
            {
                kiirat.Text = (ex.Message);
            }
            shoot_energy();
        }
        private async void shoot_right()
        {
            User user = new User();
            user.Shoot = 1;
            user.PrivateId = 5201;
            string userdata_shoot = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            try
            {
                await myhub.SendAsync("SendMessage", "shoot", userdata_shoot);
            }
            //Exception message if something went wrong.
            catch (Exception ex)
            {
                kiirat.Text = (ex.Message);
            }
            shoot_energy();
        }
        private void robot_pilot()
        {
            Random rnd = new Random();
            int random = 0;
                random = rnd.Next(1, 100);
                if (random % 2 == 0)
                {
                    move_left();
                    random = rnd.Next(1, 100);
                    if (random % 2 == 0)
                    {
                        shoot_left();
                    }
                    else
                    {
                        shoot_right();                        
                    }
                }
                else
                {
                    move_right();
                    random = rnd.Next(1, 100);
                    if (random % 2 == 0)
                    {
                        shoot_left();
                    }
                    else
                    {
                        shoot_right();
                    }
                }
        }
        private void Move_Left_Click(object sender, RoutedEventArgs e)
        {
            move_left();
        }
        private void Move_Right_Click(object sender, RoutedEventArgs e)
        {
            move_right();
        }
        private void Shoot_Out_Click(object sender, RoutedEventArgs e)
        {
            shoot_right();
        }
        private void Shoot_In_Click(object sender, RoutedEventArgs e)
        {
            shoot_left();
        }
        private void AutoPlay_Click(object sender, RoutedEventArgs e)
        {            
                robot_pilot();          
        }
        private async void Shoot_Test_Click(object sender, RoutedEventArgs e)
        {
            //For testing, shoots own ship.
            User user = new User();
            user.Shoot = 0;
            user.PrivateId = 5201;
            string userdata_shoot = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            try
            {
                await myhub.SendAsync("SendMessage", "shoot", userdata_shoot);
            }
            //Exception message if something went wrong.
            catch (Exception ex)
            {
                kiirat.Text = (ex.Message);
            }
            shoot_energy();
        }
    }
}

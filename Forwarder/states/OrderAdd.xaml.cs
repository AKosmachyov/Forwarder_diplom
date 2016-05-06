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
using System.Windows.Shapes;
using Forwarder.DB;
using Forwarder.Entity;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi;
using System.Data.Entity;

namespace Forwarder.states
{
    /// <summary>
    /// Interaction logic for OrderAdd.xaml
    /// </summary>
    public partial class OrderAdd : Window
    {
        public OrderAdd()
        {
            InitializeComponent();            
        }
        public Order order=new Order();

        //открывает форма выбора клиента
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var clientFormWindow = new ClientForm();
            this.Hide();
            clientFormWindow.ShowDialog();
            if (clientFormWindow.choice != null)
            {
                order.Client = clientFormWindow.choice;
                lbClient.Content = clientFormWindow.choice.Name;
            }
            this.Show();
        }

        //собирает класс 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            order.Status = Convert.ToByte(cbStatus.SelectedIndex);
            order.Start = datePicker1.SelectedDate;
            order.Finish = datePicker2.SelectedDate;
            this.Close();
        }
        
        //Обработка маршрута
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string[] words = this.texBoxRoute.Text.Split(':');
            var myRange = words.ToList().GetRange(1, words.Length - 2).ToArray();
            var _request = new DirectionsRequest { Origin = words[0], Destination = words[words.Length - 1], Waypoints = myRange, OptimizeWaypoints = true };
            try
            {
                var _result = GoogleMaps.Directions.Query(_request);
                if (_result.Status == Status.OK)
                {
                    var km = 0;
                    texBoxRoute.Text = "";
                    foreach (var el in _result.Routes.First().Legs)
                    {
                        km += el.Distance.Value;

                        order.Routes.Add(new Route { Name = el.StartAddress, Lat = el.StartLocation.Latitude, Lng = el.StartLocation.Longitude });
                        texBoxRoute.Text = texBoxRoute.Text + el.StartAddress + ":";
                    }
                    var lastAd = _result.Routes.First().Legs.Last();
                    order.Routes.Add(new Route { Name = lastAd.EndAddress, Lat = lastAd.EndLocation.Latitude, Lng = lastAd.EndLocation.Longitude });
                    texBoxRoute.Text = texBoxRoute.Text + lastAd.EndAddress;
                    order.Way = km / 1000;
                    this.lb1.Content = "Расстояние: "+(km / 1000).ToString()+" км";
                };
                if (_result.Status == Status.NOT_FOUND)
                {
                    this.lb1.Content = "Город не найден";
                }
                if (_result.Status == Status.ZERO_RESULTS)
                {
                    this.lb1.Content = "Не удалось проложить маршрут";
                }
            }
            catch
            {                
               this.lb1.Content="Ошибка Соединения";              
            }    
        }

        //Добавить груз
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var addOrderWindow = new CargoAdd();
            this.Hide();
            addOrderWindow.ShowDialog();           
            if (addOrderWindow.cargo!=null)
            {
                ForwarderDB._db.Cargos.Add(addOrderWindow.cargo);
                ForwarderDB._db.SaveChanges();
                order.Cargos.Add(addOrderWindow.cargo);
                dataGridCargo.Items.Refresh();
            }
            this.ShowDialog();
        }

        //Удалиит груз
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dataGridCargo.SelectedItems.Count>0)
            {
                var cargoDb = dataGridCargo.SelectedItems[0] as Cargo;
                ForwarderDB._db.Entry(cargoDb).State = EntityState.Deleted;
                ForwarderDB._db.SaveChanges();
                order.Cargos.Remove(cargoDb);
                dataGridCargo.Items.Refresh();
            }
        }

        //Расход изменение
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (dataGridCargo.SelectedItems.Count > 0)
            {                
                var cargoDg = dataGridCargo.SelectedItems[0] as Cargo;
                var cargoDb = ForwarderDB._db.Cargos.Where(x => x.Id == cargoDg.Id).First();
                var addOrderWindow = new CargoAdd(cargoDb);
                this.Hide();
                addOrderWindow.ShowDialog();
                if (addOrderWindow.cargo !=null)
                {                    
                    ForwarderDB._db.Entry(addOrderWindow.cargo).State = EntityState.Modified;
                    ForwarderDB._db.SaveChanges();
                    cargoDg = addOrderWindow.cargo;
                    dataGridCargo.Items.Refresh();
                }
                this.ShowDialog();
            }
        }

        //Расход добавление
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var addRacxodWindow = new RacxodAdd();
            addRacxodWindow.ShowDialog();
            if (addRacxodWindow.racxod != null)
            {
                ForwarderDB._db.Racxods.Add(addRacxodWindow.racxod);
                ForwarderDB._db.SaveChanges();
                order.Racxod.Add(addRacxodWindow.racxod);
                dataGridRacxod.Items.Refresh();
            }
        }

        //Расход удаление
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (dataGridRacxod.SelectedItems.Count > 0)
            {
                var racxodDg = dataGridRacxod.SelectedItems[0] as Racxod;
                ForwarderDB._db.Entry(racxodDg).State = EntityState.Deleted;
                ForwarderDB._db.SaveChanges();
                order.Racxod.Remove(racxodDg);
                dataGridRacxod.Items.Refresh();
            }
        }

        //Расход изменение
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (dataGridRacxod.SelectedItems.Count > 0)
            {
                var racxodDg = dataGridRacxod.SelectedItems[0] as Racxod;
                var racxodDb = ForwarderDB._db.Racxods.Where(x => x.Id == racxodDg.Id).First();
                var addRacoxWindow = new RacxodAdd(racxodDb);
                this.Hide();
                addRacoxWindow.ShowDialog();
                if (addRacoxWindow.racxod != null)
                {
                    ForwarderDB._db.Entry(addRacoxWindow.racxod).State = EntityState.Modified;
                    ForwarderDB._db.SaveChanges();
                    racxodDg = addRacoxWindow.racxod;
                    dataGridRacxod.Items.Refresh();
                }
                this.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridCargo.ItemsSource = order.Cargos;
            dataGridRacxod.ItemsSource = order.Racxod;
        }     
                
    }
}

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
            var t = new ClientForm();
            t.ShowDialog();            
            order.Client = t.choice;
            lbClient.Content = t.choice.Name;       
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
                var t = dataGridCargo.SelectedItems[0] as Cargo;
                ForwarderDB._db.Entry(t).State = EntityState.Deleted;
                ForwarderDB._db.SaveChanges();
                order.Cargos.Remove(t);
                dataGridCargo.Items.Refresh();
            }
        }

        //Расход изменение
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (dataGridCargo.SelectedItems.Count > 0)
            {
                var addOrderWindow = new CargoAdd();
                var t = dataGridCargo.SelectedItems[0] as Cargo;
                var a = ForwarderDB._db.Cargos.Where(x => x.Id == t.Id).First();
                addOrderWindow.tbName.Text = t.Name;
                addOrderWindow.tbCap.Text = t.Capacity.ToString();
                addOrderWindow.tbWeig.Text = t.Weight.ToString();
                addOrderWindow.cargo.Car = t.Car;
                addOrderWindow.tbStart.Text = t.StartRoute;
                addOrderWindow.tbFinish.Text = t.FinishRoute;
                addOrderWindow.label1.Content = t.Car.Number;
                addOrderWindow.dp1.SelectedDate = t.DataTake;
                addOrderWindow.dp2.SelectedDate = t.DataReturn;
                addOrderWindow.cbStatus.SelectedIndex = t.Status;
                
                addOrderWindow.ShowDialog();
                if (addOrderWindow.cargo !=null)
                {
                    a.Name=addOrderWindow.tbName.Text;
                    a.Capacity=Convert.ToDouble(addOrderWindow.tbCap.Text);
                    a.Weight=Convert.ToDouble(addOrderWindow.tbWeig.Text);                    
                    a.StartRoute=addOrderWindow.tbStart.Text;
                    a.FinishRoute=addOrderWindow.tbFinish.Text;
                    a.Car=addOrderWindow.cargo.Car;
                    a.DataTake = addOrderWindow.dp1.DisplayDate;
                    a.DataReturn = addOrderWindow.dp2.DisplayDate;
                    a.Status=Convert.ToByte(addOrderWindow.cbStatus.SelectedIndex);
                
                    ForwarderDB._db.Entry(a).State = EntityState.Modified;
                    ForwarderDB._db.SaveChanges();
                    t.Name = addOrderWindow.tbName.Text;
                    t.Capacity =Convert.ToDouble(addOrderWindow.tbCap.Text);
                    t.Weight = Convert.ToDouble(addOrderWindow.tbWeig.Text);
                    t.StartRoute = addOrderWindow.tbStart.Text;
                    t.FinishRoute = addOrderWindow.tbFinish.Text;
                    t.Car = addOrderWindow.cargo.Car;
                    t.DataTake = addOrderWindow.dp1.DisplayDate;
                    t.DataReturn = addOrderWindow.dp2.DisplayDate;
                    t.Status = Convert.ToByte(addOrderWindow.cbStatus.SelectedIndex);
                    dataGridCargo.Items.Refresh();
                }
            }
        }

        //Расход добавление
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var addRacoxWindow = new RacxodAdd();
            addRacoxWindow.ShowDialog();
            if (addRacoxWindow.racxod != null)
            {
                ForwarderDB._db.Racxods.Add(addRacoxWindow.racxod);
                ForwarderDB._db.SaveChanges();
                order.Racxod.Add(addRacoxWindow.racxod);
                dataGridRacxod.Items.Refresh();
            }
        }

        //Расход удаление
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            var t = dataGridRacxod.SelectedItems[0] as Racxod;
            
            ForwarderDB._db.Entry(t).State = EntityState.Deleted;
            ForwarderDB._db.SaveChanges();
            order.Racxod.Remove(t);
            dataGridRacxod.Items.Refresh();
        }

        //Расход изменение
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            var addRacoxWindow = new RacxodAdd();
            var t = dataGridRacxod.SelectedItems[0] as Racxod;
            var a = ForwarderDB._db.Racxods.Where(x => x.Id == t.Id).First();
            addRacoxWindow.textBox1.Text = t.Name;
            addRacoxWindow.textBox2.Text = t.Price.ToString();
            addRacoxWindow.ShowDialog();
            if (addRacoxWindow.racxod != null)
            {
                a.Name = addRacoxWindow.racxod.Name;
                a.Price = addRacoxWindow.racxod.Price;
                ForwarderDB._db.Entry(a).State = EntityState.Modified;
                ForwarderDB._db.SaveChanges();
                t.Name = addRacoxWindow.racxod.Name;
                t.Price = addRacoxWindow.racxod.Price;
                dataGridRacxod.Items.Refresh();
            }        
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridCargo.ItemsSource = order.Cargos;
            dataGridRacxod.ItemsSource = order.Racxod;
        }

        
                
    }
}

using Forwarder.DB;
using Forwarder.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Forwarder.states
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public enum statusOrder:int { Выполняется=0, Завершён=1, Проблема=2 };
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }
        private List<Order> orders;        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new OrderAdd();
            window.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var addOrderWindow = new OrderAdd();
            addOrderWindow.ShowDialog();
            if (addOrderWindow.order != null)
            {
                ForwarderDB._db.Orders.Add(addOrderWindow.order);
                orders.Add(addOrderWindow.order);
                ForwarderDB._db.SaveChanges();
                dataGridOrders.Items.Refresh();
            }
            this.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            orders = ForwarderDB._db.Orders.ToList();
            dataGridOrders.ItemsSource = orders;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dataGridOrders.SelectedItems.Count > 0)
            {
                var t = dataGridOrders.SelectedItems[0] as Order;                
                ForwarderDB._db.Entry(t).State = EntityState.Deleted;
                ForwarderDB._db.SaveChanges();
                orders.Remove(t);
                dataGridOrders.Items.Refresh();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (dataGridOrders.SelectedItems.Count > 0)
            {
                var addOrderWindow = new OrderAdd();
                var t = dataGridOrders.SelectedItems[0] as Order;
                var a = ForwarderDB._db.Orders.Where(x => x.Id == t.Id).First();

                addOrderWindow.cbStatus.SelectedIndex = t.Status;
                addOrderWindow.datePicker1.SelectedDate = t.Start;
                addOrderWindow.datePicker2.SelectedDate = t.Finish;
                addOrderWindow.order.Client = t.Client;
                addOrderWindow.order.Cargos = t.Cargos;
                addOrderWindow.order.Racxod = t.Racxod;
                addOrderWindow.lbClient.Content = t.Client.Name;
                addOrderWindow.lb1.Content = "Расстояние: "+t.Way.ToString()+" км";
                addOrderWindow.texBoxRoute.Text = "";
                foreach (var el in t.Routes)
                {
                    addOrderWindow.texBoxRoute.Text = addOrderWindow.texBoxRoute.Text + el.Name+":";
                }
                
                addOrderWindow.order.Routes = t.Routes;

                addOrderWindow.ShowDialog();
                if (addOrderWindow.order != null)
                {
                    a.Status=Convert.ToByte(addOrderWindow.cbStatus.SelectedIndex);
                    a.Start=addOrderWindow.datePicker1.SelectedDate;
                    a.Finish=addOrderWindow.datePicker2.SelectedDate;
                    a.Client=addOrderWindow.order.Client ;
                    a.Cargos=addOrderWindow.order.Cargos;
                    a.Racxod=addOrderWindow.order.Racxod;
                    a.Routes=addOrderWindow.order.Routes;
                    a.Way = addOrderWindow.order.Way;

                    ForwarderDB._db.Entry(a).State = EntityState.Modified;
                    ForwarderDB._db.SaveChanges();
                    t.Status = Convert.ToByte(addOrderWindow.cbStatus.SelectedIndex);
                    t.Start = addOrderWindow.datePicker1.SelectedDate;
                    t.Finish = addOrderWindow.datePicker2.SelectedDate;
                    t.Client = addOrderWindow.order.Client;
                    t.Cargos = addOrderWindow.order.Cargos;
                    t.Racxod = addOrderWindow.order.Racxod;
                    t.Routes = addOrderWindow.order.Routes;
                    t.Way = Convert.ToInt32(addOrderWindow.order.Way);

                    dataGridOrders.Items.Refresh();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }       
    }
}

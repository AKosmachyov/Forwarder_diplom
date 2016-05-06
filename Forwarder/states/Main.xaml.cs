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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            orders = ForwarderDB._db.Orders.ToList();
            dataGridOrders.ItemsSource = orders;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {            
            var addOrderWindow = new OrderAdd();
            this.Hide();
            addOrderWindow.ShowDialog();
            if (addOrderWindow.order != null)
            {
                ForwarderDB._db.Orders.Add(addOrderWindow.order);
                ForwarderDB._db.SaveChanges();
                orders.Add(addOrderWindow.order);                
                dataGridOrders.Items.Refresh();
            }
            this.ShowDialog();
        }
        
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dataGridOrders.SelectedItems.Count > 0)
            {
                var orderDg = dataGridOrders.SelectedItems[0] as Order;                
                ForwarderDB._db.Entry(orderDg).State = EntityState.Deleted;
                ForwarderDB._db.SaveChanges();
                orders.Remove(orderDg);
                dataGridOrders.Items.Refresh();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (dataGridOrders.SelectedItems.Count > 0)
            {               
                var orderDg = dataGridOrders.SelectedItems[0] as Order;
                var orderDb = ForwarderDB._db.Orders.Where(x => x.Id == orderDg.Id).First();
                var addOrderWindow = new OrderAdd(orderDb);
                this.Hide();
                addOrderWindow.ShowDialog();
                if (addOrderWindow.order != null)
                {               

                    ForwarderDB._db.Entry(addOrderWindow.order).State = EntityState.Modified;
                    ForwarderDB._db.SaveChanges();
                    orderDg = addOrderWindow.order;
                    dataGridOrders.Items.Refresh();
                }
                this.ShowDialog();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }       
    }
}

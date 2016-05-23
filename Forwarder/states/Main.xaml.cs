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

        public Main(User userDb)
        {
            InitializeComponent();
            user = userDb;
            var a = ForwarderDB._db.Orders.Include(b => b.Cargos).Include(b => b.Racxod).Include(b => b.Client).Include(b => b.Routes).Where(b => b.User.Id == userDb.Id);
            orders = a.ToList();            
            dataGridOrders.ItemsSource = orders;
            textBoxKomissia.Text = user.Komissia.ToString();
        }
        private User user;
        private List<Order> orders;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {            
            var addOrderWindow = new OrderAdd();
            this.Hide();
            addOrderWindow.ShowDialog();
            if (addOrderWindow.order != null)
            {
                addOrderWindow.order.User = user;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            user.Komissia = Convert.ToDecimal(textBoxKomissia.Text);
            ForwarderDB._db.Entry(user).State = EntityState.Modified;
            ForwarderDB._db.SaveChanges();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (textBoxNewPass.Password.Length > 0)
            {
                return;
            }
            labelPass.Content = "";
            if (textBoxNewPass.Password != textBoxRepitPass.Password)
            {
                labelPass.Content = "Введены 2 разных пароля";
                return;
            }
            if (user.Password != Core.generatePasswordHash(textBoxOldPass.Password))
            {
                labelPass.Content = "Пароль не верный";
                return;
            }            
            user.Password=Core.generatePasswordHash(textBoxNewPass.Password);
            ForwarderDB._db.Entry(user).State = EntityState.Modified;
            ForwarderDB._db.SaveChanges();
            textBoxOldPass.Password = "";
            textBoxNewPass.Password = "";
            textBoxRepitPass.Password = "";
        }       
    }
}

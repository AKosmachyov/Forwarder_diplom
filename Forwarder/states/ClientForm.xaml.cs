using Forwarder.DB;
using Forwarder.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class ClientForm : Window
    {
        public ClientForm()
        {
            InitializeComponent();                       
        }
        private List<Client> clients;
        public Client choice;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var newWindow = new ClientAdd();
            newWindow.ShowDialog();
            if (newWindow.client != null)
            {
                ForwarderDB._db.Clients.Add(newWindow.client);
                clients.Add(newWindow.client);                
                ForwarderDB._db.SaveChanges();
                dataGrid.Items.Refresh();
            }
            this.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                var t = dataGrid.SelectedItems[0] as Client;
                //
                ForwarderDB._db.Entry(t).State = EntityState.Deleted;
                ForwarderDB._db.SaveChanges();
                clients.Remove(t);
                dataGrid.Items.Refresh();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var newWindow = new ClientAdd();            
            var t = dataGrid.SelectedItems[0] as Client;
            var a=ForwarderDB._db.Clients.Where(x=>x.Id==t.Id).First();   
            newWindow.textBox1.Text = t.Name;
            newWindow.textBox2.Text = t.Phone;
            newWindow.ShowDialog();
            if (newWindow.client != null) 
            {
                a.Name=newWindow.client.Name;
                a.Phone=newWindow.client.Phone;
                ForwarderDB._db.Entry(a).State=EntityState.Modified;
                ForwarderDB._db.SaveChanges();
                t.Name = newWindow.client.Name;
                t.Phone = newWindow.client.Phone;
                dataGrid.Items.Refresh();               
            }        
       }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            clients = ForwarderDB._db.Clients.ToList();
            dataGrid.ItemsSource =clients ;
            dataGrid.Items.Refresh();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                choice = dataGrid.SelectedItems[0] as Client;
                this.Close();
            }
        }

  }
}

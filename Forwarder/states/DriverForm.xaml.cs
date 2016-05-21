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
    /// Interaction logic for DriverForm.xaml
    /// </summary>
    public partial class DriverForm : Window
    {
        public DriverForm()
        {
            InitializeComponent();
        }

        public Driver choice;
        private List<Driver> drivers;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            drivers = ForwarderDB._db.Drivers.ToList();
            dataGridDriver.ItemsSource = drivers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var driverAddWindow = new DriverAdd();
            this.Hide();
            driverAddWindow.ShowDialog();
            if (driverAddWindow.driver != null)
            {
                ForwarderDB._db.Drivers.Add(driverAddWindow.driver);
                ForwarderDB._db.SaveChanges();
                drivers.Add(driverAddWindow.driver);
                dataGridDriver.Items.Refresh();
            }
            this.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGridDriver.SelectedItems.Count > 0)
            {
                var driverdg = dataGridDriver.SelectedItems[0] as Driver;
                ForwarderDB._db.Entry(driverdg).State = EntityState.Deleted;
                ForwarderDB._db.SaveChanges();
                drivers.Remove(driverdg);
                dataGridDriver.Items.Refresh();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dataGridDriver.SelectedItems.Count > 0)
            {
                this.Hide();
                var driverDg = dataGridDriver.SelectedItems[0] as Driver;
                var driverDb = ForwarderDB._db.Drivers.Where(x => x.Id == driverDg.Id).First();
                var driverAddWindow = new DriverAdd(driverDb);
                driverAddWindow.ShowDialog();
                if (driverAddWindow.driver != null)
                {
                    ForwarderDB._db.Entry(driverAddWindow.driver).State = EntityState.Modified;
                    ForwarderDB._db.SaveChanges();
                    driverDg = driverAddWindow.driver;
                    dataGridDriver.Items.Refresh();
                }
                this.ShowDialog();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (dataGridDriver.SelectedItems.Count > 0)
            {
                choice = dataGridDriver.SelectedItems[0] as Driver;                            
                this.Close();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}

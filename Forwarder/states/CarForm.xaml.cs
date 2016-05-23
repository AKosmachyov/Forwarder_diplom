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
    /// Interaction logic for CarForm.xaml
    /// </summary>
    public partial class CarForm : Window
    {
        public CarForm()
        {
            InitializeComponent();
        } 
  
        public Car choice;
        private List<Car> listCar= new List<Car>();     

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listCar = ForwarderDB._db.Cars.Include(b => b.Drivers).Include(c=>c.Firm).ToList();
            dataGridCar.ItemsSource = listCar;
        }
          
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var carAddWindow = new CarAdd(); 
            this.Hide();
            carAddWindow.ShowDialog();              
            if (carAddWindow.car != null)
            {
                ForwarderDB._db.Cars.Add(carAddWindow.car);
                ForwarderDB._db.SaveChanges();
                listCar.Add(carAddWindow.car);
                dataGridCar.Items.Refresh();
            }            
            this.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGridCar.SelectedItems.Count > 0)
            {
                var carDg = dataGridCar.SelectedItems[0] as Car;
                ForwarderDB._db.Entry(carDg).State = EntityState.Deleted;
                ForwarderDB._db.SaveChanges();
                listCar.Remove(carDg);
                dataGridCar.Items.Refresh();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dataGridCar.SelectedItems.Count > 0)
            {
                this.Hide();
                var carDg = dataGridCar.SelectedItems[0] as Car;
                var carDb = ForwarderDB._db.Cars.Where(x => x.Id == carDg.Id).First();
                var carAddWindow = new CarAdd(carDb);
                carAddWindow.ShowDialog();
                if (carAddWindow.car != null)
                {
                    ForwarderDB._db.Entry(carAddWindow.car).State = EntityState.Modified;
                    ForwarderDB._db.SaveChanges();
                    carDg = carAddWindow.car;
                    dataGridCar.Items.Refresh();                    
                }
                this.ShowDialog();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (dataGridCar.SelectedItems.Count > 0)
            {
                choice = dataGridCar.SelectedItems[0] as Car;
                this.Close();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            choice = null;
            this.Close();
        }
    }
}

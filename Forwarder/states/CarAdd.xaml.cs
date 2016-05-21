using Forwarder.Entity;
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

namespace Forwarder.states
{
    /// <summary>
    /// Interaction logic for CarAdd.xaml
    /// </summary>
    public partial class CarAdd : Window
    {
        public CarAdd()
        {
            InitializeComponent();
        }
        public CarAdd(Car carDb)
        {
            InitializeComponent();
            car = carDb;
            textBoxNumber.Text = carDb.Number;
            textBoxPrice.Text = carDb.Price.ToString();          
            textBoxWeight.Text=carDb.MaxWeight.ToString();
            textBoxCapacity.Text = carDb.MaxCapacity.ToString();
            labelFirm.Content = carDb.Firm.Name;
            labelDrivers.Content = carDb.Drivers.Name;            
        }

        public Car car = new Car();
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var firmWindow = new FirmForm();            
            firmWindow.ShowDialog();        
    
            if (firmWindow.choice != null)
            {
                car.Firm = firmWindow.choice;
                labelFirm.Content = firmWindow.choice.Name;
            }
            this.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var driverWindow = new DriverForm();            
            driverWindow.ShowDialog();            
            if (driverWindow.choice != null)
            {
                car.Drivers = driverWindow.choice;
                labelDrivers.Content = driverWindow.choice.Name;
            }
            this.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (textBoxNumber.Text.Length > 0 && textBoxCapacity.Text.Length > 0 && textBoxPrice.Text.Length > 0 && textBoxWeight.Text.Length > 0 && car.Firm != null && car.Drivers!=null)
            {
                try
                {
                    car.IsReady = true;
                    car.MaxCapacity = Convert.ToDouble(textBoxCapacity.Text);
                    car.MaxWeight = Convert.ToDouble(textBoxWeight.Text);
                    car.Price = Convert.ToDecimal(textBoxPrice.Text);
                    car.Number = textBoxNumber.Text;
                    this.Close();
                }
                catch {
                    MessageBox.Show("Не верно заполнены поля");
                }
            }
            else
            {
                MessageBox.Show("Поля не заполнены");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            car = null;
            this.Close();
        }

        private void textBoxCapacity_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;            
            //var t = KeyInterop.VirtualKeyFromKey(e.Key);
            if (e.Key == Key.OemComma || e.Key == Key.Decimal || e.Key == Key.Tab)               
            {                
                e.Handled=false;   
            }
            // Пропускаем цифровые кнопки
            if ((e.Key >= Key.D0) && (e.Key <= Key.D9)) e.Handled = false;
            // Пропускаем цифровые кнопки с NumPad'а
            if ((e.Key >= Key.NumPad0) && (e.Key <= Key.NumPad9)) e.Handled = false;
            // Пропускаем Delete, Back, Left и Right
            if ((e.Key == Key.Delete) || (e.Key == Key.Back) ||
                (e.Key == Key.Left) || (e.Key == Key.Right)) e.Handled = false;
        }


    }
}

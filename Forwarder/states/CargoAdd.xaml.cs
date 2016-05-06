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
    /// Interaction logic for CargoAdd.xaml
    /// </summary>
    public partial class CargoAdd : Window
    {
        public CargoAdd()
        {
            InitializeComponent();
        }
        public CargoAdd(Cargo cargoDb)
        {
            InitializeComponent();
            cargo = cargoDb;

            tbName.Text = cargoDb.Name;
            tbCap.Text = cargoDb.Capacity.ToString();
            tbWeig.Text = cargoDb.Weight.ToString();
            cargo.Car = cargoDb.Car;
            tbStart.Text = cargoDb.StartRoute;
            tbFinish.Text = cargoDb.FinishRoute;
            label1.Content = cargoDb.Car.Number;
            dp1.SelectedDate = cargoDb.DataTake;
            dp2.SelectedDate = cargoDb.DataReturn;
            cbStatus.SelectedIndex = cargoDb.Status;
        }
        public Cargo cargo = new Cargo();
        //Выбор машины
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var CarFormWindow = new CarForm();
            this.Hide();
            CarFormWindow.ShowDialog();
            if (CarFormWindow.choice != null)
            {
                cargo.Car = CarFormWindow.choice;
                label1.Content = cargo.Car.Number;
            }
            this.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tbCap.Text.Length > 0 && tbName.Text.Length > 0 && tbWeig.Text.Length > 0 && tbStart.Text.Length > 0 && tbFinish.Text.Length > 0 && cargo.Car != null)
            {
                try
                {
                    cargo.Status = Convert.ToByte(cbStatus.SelectedIndex);
                    cargo.Capacity = Convert.ToDouble(tbCap.Text);
                    cargo.Name = tbName.Text;
                    cargo.Weight = Convert.ToDouble(tbWeig.Text);
                    cargo.DataTake = dp1.SelectedDate;
                    cargo.DataReturn = dp2.SelectedDate;
                    cargo.StartRoute = tbStart.Text;
                    cargo.FinishRoute = tbFinish.Text;
                    this.Hide();
                }
                catch
                {
                    MessageBox.Show("Поля заполнены не корректно");
                }
            }
            else
            {
                MessageBox.Show("Поля не заполнены");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            cargo = null;
            this.Close();
        }

        private void tbCap_KeyDown(object sender, KeyEventArgs e)
        {         
            e.Handled = true;            
            //var t = KeyInterop.VirtualKeyFromKey(e.Key);
            if (e.Key == Key.OemComma || e.Key == Key.Decimal || e.Key ==Key.Tab )               
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
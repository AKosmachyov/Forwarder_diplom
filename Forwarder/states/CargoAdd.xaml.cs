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
        public Cargo cargo=new Cargo();
        //Выбор машины
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var formCarWindow = new CarForm();
            this.Hide();
            var asf=formCarWindow.ShowDialog();    
                if (formCarWindow.choice != null)
                {
                    cargo.Car = formCarWindow.choice;
                    label1.Content = cargo.Car.Number;
                }            
            this.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
    }
}

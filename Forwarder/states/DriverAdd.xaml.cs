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
    /// Interaction logic for DriverAdd.xaml
    /// </summary>
    public partial class DriverAdd : Window
    {
        public DriverAdd()
        {
            InitializeComponent();
        }
        public DriverAdd(Driver driverDb)
        {
            InitializeComponent();
            driver = driverDb;
            textBox1.Text = driver.Name;
            textBox2.Text = driver.Phone;
        }

        public Driver driver = new Driver();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length>0 && textBox2.Text.Length>0)
            {
                driver.Name = textBox1.Text;
                driver.Phone = textBox2.Text;
                this.Close();
            }else
            {
                MessageBox.Show("Поля не заполнены");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            driver = null;
            this.Close();
        }
    }
}

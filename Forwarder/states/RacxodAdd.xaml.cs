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
    /// Interaction logic for RacxodAdd.xaml
    /// </summary>
    public partial class RacxodAdd : Window
    {
        public Racxod racxod =new Racxod();
        public RacxodAdd()
        {
            InitializeComponent();
        }
        public RacxodAdd(Racxod racxodDb)
        {
            InitializeComponent();
            racxod = racxodDb;
            textBox1.Text = racxodDb.Name;
            textBox2.Text = racxodDb.Price.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                racxod.Name = textBox1.Text;
                try
                {
                    racxod.Price = Convert.ToDecimal(textBox2.Text);
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Не корректно заполнены поля");
                }
            }
            else
            {
                MessageBox.Show("Поля не заполнены");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            racxod = null;
            this.Close();
        }

    }
}

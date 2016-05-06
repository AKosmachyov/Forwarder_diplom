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

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
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

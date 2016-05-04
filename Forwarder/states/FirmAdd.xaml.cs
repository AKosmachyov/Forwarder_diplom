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
    /// Interaction logic for FirmAdd.xaml
    /// </summary>
    public partial class FirmAdd : Window
    {
        public FirmAdd()
        {
            InitializeComponent();
        }
        public FirmAdd(Firm firmDb) 
        {                                
            InitializeComponent();
            firm = firmDb;
            textBox1.Text = firm.Name;
            textBox2.Text = firm.Phone;
        }

        public Firm firm = new Firm();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                firm.Name = textBox1.Text;
                firm.Phone = textBox2.Text;
                this.Close();
            }else
            {
                MessageBox.Show("Поля не заполнены");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            firm = null;
            this.Close();   
        }
    }
}

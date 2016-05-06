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
    /// Interaction logic for ClientAdd.xaml
    /// </summary>
    public partial class ClientAdd : Window
    {
        public ClientAdd()
        {
            InitializeComponent();
        }
        public ClientAdd(Client clientDb)
        {
            InitializeComponent();
            client = clientDb;
            textBox1.Text = clientDb.Name;
            textBox2.Text = clientDb.Phone;
        }
        public Client client = new Client();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                client.Name = textBox1.Text;
                client.Phone = textBox2.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Поля не заполнены");
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            client = null;
            this.Close();
        }
        
               
    }
}

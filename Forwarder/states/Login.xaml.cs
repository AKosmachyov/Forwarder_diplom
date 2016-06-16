using System.Linq;
using System;
using System.Windows;
using Forwarder.Entity;
using Forwarder.states;
using Forwarder.DB;

namespace Forwarder.States
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            User user = ForwarderDB._db.Users.FirstOrDefault(x => x.Login == loginBox.Text);
            if (user == null)
            {
                MessageBox.Show("Введён неверный логин");
                return;
            }
            if (user.Password != Core.generatePasswordHash(passwordBox.Password))
            {
                MessageBox.Show("Введён неверный пароль");
                return;
            }           
            var window = new Main(user);
            this.Close();
            window.Show();                       
        }      

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

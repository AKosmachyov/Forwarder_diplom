using System.Linq;
using System;
using System.Windows;
using Forwarder.Entity;
using Forwarder.states;
using System.Security.Cryptography;
using System.Text;
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
            
            if (user.Password != generatePasswordHash(passwordBox.Password))
            {
                MessageBox.Show("Введён неверный пароль");
                return;
            }
            var window = new Main();
            this.Close();
            window.Show();                       
        }

        private static string generatePasswordHash(string password)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

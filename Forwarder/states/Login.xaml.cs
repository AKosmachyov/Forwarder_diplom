using System.Linq;
using System;
using System.Windows;
using Forwarder.Entity;
using Forwarder.states;

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
            
            var window = new Main();
            this.Close();
            window.Show();                       
        }

        private void showErrorMessageBox(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

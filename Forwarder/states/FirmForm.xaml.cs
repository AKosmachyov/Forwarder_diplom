using Forwarder.DB;
using Forwarder.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for FirmForm.xaml
    /// </summary>
    public partial class FirmForm : Window
    {
        public FirmForm()
        {
            InitializeComponent();
        }
        public Car choice;
        private List<Firm> firms;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            firms=ForwarderDB._db.Firms.ToList();
            dataGridFirm.ItemsSource = firms;
        }      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var FirmAddWindow = new FirmAdd();
            this.Hide();
            FirmAddWindow.ShowDialog();
            if (FirmAddWindow.firm != null)
            {
                ForwarderDB._db.Firms.Add(FirmAddWindow.firm);
                ForwarderDB._db.SaveChanges();
                firms.Add(FirmAddWindow.firm);
                dataGridFirm.Items.Refresh();
            }
            this.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGridFirm.SelectedItems.Count > 0)
            {
                var firmDb = dataGridFirm.SelectedItems[0] as Firm;                
                ForwarderDB._db.Entry(firmDb).State = EntityState.Deleted;
                ForwarderDB._db.SaveChanges();
                firms.Remove(firmDb);
                dataGridFirm.Items.Refresh();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dataGridFirm.SelectedItems.Count > 0)
            {                
                var firmDg = dataGridFirm.SelectedItems[0] as Firm;
                var firmDb = ForwarderDB._db.Firms.Where(x => x.Id == firmDg.Id).First();
                var FirmAddWindow = new FirmAdd (firmDb);
                this.Hide();
                FirmAddWindow.ShowDialog();
                if (FirmAddWindow.firm != null)
                {
                    ForwarderDB._db.Entry(FirmAddWindow.firm).State = EntityState.Modified;
                    ForwarderDB._db.SaveChanges();
                    firmDg = FirmAddWindow.firm;
                    dataGridFirm.Items.Refresh();
                }
                this.ShowDialog();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (dataGridFirm.SelectedItems.Count > 0)
            {              
                var firmDb = dataGridFirm.SelectedItems[0] as Firm;
                var CarFormWindow = new CarForm(firmDb);
                this.Hide();
                CarFormWindow.ShowDialog();
                if (CarFormWindow.choice != null)
                    choice = CarFormWindow.choice;
                this.Close();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

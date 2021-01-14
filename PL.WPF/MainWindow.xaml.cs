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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLAPI;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IBL bl = BLFactory.GetBL();
        public MainWindow()
        {
            InitializeComponent();
            User user = new User(bl);
            user.ShowDialog();
           
        }
        private void btnGO_Click(object sender, RoutedEventArgs e)
        {
            if (rbBus.IsChecked == true)
            {
                new BusWindow(bl).Show();
               
            }
            else if(rbBusLine.IsChecked==true)
            {
                new BusLineWindow(bl).Show();
             
            }
            else if(rbStation.IsChecked==true)
            {
                new StationWindow(bl).Show();
             
            }
            else
            {
                MessageBox.Show("Please select an option");
            }
        }
    }
}

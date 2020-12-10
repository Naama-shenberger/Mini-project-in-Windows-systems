using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for BusDriver.xaml
    /// </summary>
    public partial class BusDriverWindow : Window
    {
        private ObservableCollection<BusDriver> BusDriverList = new ObservableCollection<BusDriver>();
        /// <summary>
        /// constructor Receiving a list of bus drivers
        /// </summary>
        /// <param name="Listedrivers"></param>
        public BusDriverWindow(object Listedrivers)
        {
            InitializeComponent();
            if (Listedrivers != null)
            {
                BusDriverList = (ObservableCollection<BusDriver>)Listedrivers;
                if (BusDriverList.Count==0)//If the list is empty the user will see
                    emptyList.Visibility = Visibility.Visible;
                lbdrivers.ItemsSource = BusDriverList;
                lbdrivers.Items.Refresh();
            }
        }
        /// <summary>
        /// MouseDoubleClick event for item list
        /// The event opens a window with the bus driver details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as ListView;
            BusDriver busDriverData = btn.SelectedItem as BusDriver;
            showInfoBusDriver busDriver = new showInfoBusDriver(busDriverData);
            busDriver.ShowDialog();
        }
        /// <summary>
        ///  A click event that closes the class window, the button actually displays the previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

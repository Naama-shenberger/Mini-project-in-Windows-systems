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
using BLAPI;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for AddNewStationWindow.xaml
    /// </summary>
    public partial class AddNewStationWindow : Window
    {
        IBL bl;
        BO.BusStation newStation;
        public BO.BusStation BusStation
        {
            get { return newStation; }
        }

        public AddNewStationWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;

        }
        /// <summary>
        /// A button that absorb the new stations info and returns it to previous window 
        /// and returns to previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newStation = new BO.BusStation()
                {
                    BusStationKey = int.Parse(Bus_Station_Key.Text),
                    StationName = Station_Name.Text,
                    StationAddress = Station_Address.Text,
                    Latitude = LatitudeValue.Value,
                    Longitude = LongdeValue.Value,
                    Active=true,
                };
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Unsuitable characters please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.IdException)
            {
                MessageBox.Show("", "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           

        }
        /// <summary>
        /// A button that returns you to previous window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

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
        public BO.BusStation newStation;
        public AddNewStationWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;

        }
        private void addNewStation()
        {

        }

        private void AddingButton_Click(object sender, RoutedEventArgs e)
        {
            newStation = new BO.BusStation() { BusStationKey = int.Parse(Bus_Station_Key.Text), StationName = Station_Name.Text, StationAddress = Station_Address.Text, Latitude = float.Parse(Station_Latitude.Text), Longitude = float.Parse(Station_Longitude.Text) };

            try
            {
                    //if (int.Parse(Bus_Station_Key.Text) > 99999 && int.Parse(Bus_Station_Key.Text) < 1000000)
                    //    MessageBox.Show("unvalid bus station code", "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                bl.AddBusStation(newStation);
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

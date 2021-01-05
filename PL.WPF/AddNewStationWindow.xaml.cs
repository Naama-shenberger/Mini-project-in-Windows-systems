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
        public BO.BusStation curBusStation;
        public AddNewStationWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;

        }

        private void AddingButton_Click(object sender, RoutedEventArgs e)
        {
            curBusStation = new BO.BusStation() { BusStationKey = int.Parse(Bus_Station_Key.Text), StationName = Station_Name.Text, StationAddress = Station_Address.Text, Latitude = float.Parse(Station_Latitude.Text), Longitude = float.Parse(Station_Longitude.Text) };
            this.Close();
        }
    }
}

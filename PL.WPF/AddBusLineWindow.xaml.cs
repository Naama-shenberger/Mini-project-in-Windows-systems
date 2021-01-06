using BLAPI;
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

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for AddBusLineWindow.xaml
    /// </summary>
    public partial class AddBusLineWindow : Window
    {
        IBL bl;
        BO.BusLine CurBusLine;
        ObservableCollection<BO.BusLineStation> busLineStations= new ObservableCollection<BO.BusLineStation>();
        public BO.BusLine BusLine
        {
            get { return CurBusLine; }
        }
        public AddBusLineWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            Areacb.ItemsSource = Enum.GetValues(typeof(BO.Enums.Zones));
            Areacb.SelectedItem = BO.Enums.Zones.Jerusalem_Corridor;
            LastStopcb.ItemsSource = FirstStopcb.ItemsSource = bl.GetAllBusLineStations();
            FirstStopcb.SelectedIndex = 0;
            LastStopcb.SelectedIndex = 2;
            LastStopcb.DisplayMemberPath = FirstStopcb.DisplayMemberPath = "BusStationKey";
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    CurBusLine = new BO.BusLine
                    {
                        BusLineNumber = int.Parse(BusLineNumbertb.Text),
                        Area = (BO.Enums.Zones)Areacb.SelectedItem,
                        Active = true,
                        FirstStopNumber=(FirstStopcb.SelectedItem as BO.BusLineStation).BusStationKey,
                        LastStopNumber=(LastStopcb.SelectedItem as BO.BusLineStation).BusStationKey

                    };
                    busLineStations.Add(FirstStopcb.SelectedItem as BO.BusLineStation);
                    busLineStations.Add(LastStopcb.SelectedItem as BO.BusLineStation);
                    CurBusLine.StationsInLine = busLineStations;
                    MessageBox.Show("The bus was successfully added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    this.Close();
                }
            }
            catch(BO.IdException)
            {

            }
        }
    }
}

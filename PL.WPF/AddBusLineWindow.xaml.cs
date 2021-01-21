using BLAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        ObservableCollection<BO.BusLineStation> busLineStations= new ObservableCollection<BO.BusLineStation>();//List of bus line station
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
        /// <summary>
        /// Enter
        /// Takes all the data and initializes the relevant parameters 
        /// goes to the window where it was sent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    BO.LineRides lineRides = new BO.LineRides
                    {
                        Active=true,
                        TravelStartTime = TimeSpan.Parse(Regex.Replace(StartTimePicker.Text, "[A-Za-z ]", "")),
                        TravelEndTime = TimeSpan.Parse(Regex.Replace(EndTimePicker.Text, "[A-Za-z ]", "")),
                        BusDepartureNumber = TimeSpan.Parse(Regex.Replace(ExitEvery.Text, "[A-Za-z ]", "")),
                    };
                    CurBusLine.lineRides = new List<BO.LineRides>();
                    CurBusLine.lineRides= CurBusLine.lineRides.Append(lineRides);
                    busLineStations.Add(FirstStopcb.SelectedItem as BO.BusLineStation);
                    var saveSelectedItem = (LastStopcb.SelectedItem as BO.BusLineStation);
                    saveSelectedItem.NumberStationInLine = 2;
                    busLineStations.Add(saveSelectedItem);
                    if ((FirstStopcb.SelectedItem as BO.BusLineStation).BusStationKey == saveSelectedItem.BusStationKey)
                        throw new BO.IdException("");
                    CurBusLine.StationsInLine = busLineStations;
                    MessageBox.Show("The bus was successfully added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch(FormatException)
            {
                MessageBox.Show("Unsuitable characters please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("No typing details please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.IdException)
            {
                MessageBox.Show("First stop and Last stop can not be the same to the stop", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

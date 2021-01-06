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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        IBL bL;
        BO.BusStation curBusStation;
        static int Index;
        ObservableCollection<BO.BusLineStation> curBusLineStations = new ObservableCollection<BO.BusLineStation>();
        ObservableCollection<BO.BusLineStation> curAllBusLineStations = new ObservableCollection<BO.BusLineStation>();
        public ObservableCollection<BO.BusLineStation> BusLineStations
        {
            get { return curBusLineStations; }
        }
        public AddStationWindow(IBL _bL)
        {
            InitializeComponent();
            bL = _bL;
            curAllBusLineStations = Convert<BO.BusLineStation>(bL.GetAllBusLineStations());
            Index = curAllBusLineStations.Count();
            RefreshAllStationsComboBox();
        }

        void RefreshAllStationsComboBox()
        {

            lvBusStations.DataContext = Convert<BO.BusStation>(bL.GetAllBusStation());

        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        private void AddBusLinesStations(BO.BusStation busStation)
        {
            try
            {

                if (curBusLineStations.FirstOrDefault(a => a.BusStationKey == busStation.BusStationKey) != null)
                    throw new ArgumentException("You have already selected the station");
                BO.BusLineStation busLineStation = new BO.BusLineStation
                {
                    BusStationKey = busStation.BusStationKey,
                    Active = busStation.Active,
                    NumberStationInLine = ++Index,
                };
                if (curAllBusLineStations.FirstOrDefault(a => a.BusStationKey == busLineStation.BusStationKey) != null)
                    throw new ArgumentException("The bus line Station already Exists");
                curBusLineStations.Add(busLineStation);
                lvBusLineStation.DataContext = Convert<object>(bL.StationDetails(curBusLineStations));
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                Index = --Index;
            }
        }
        private void DeleteBusLinesStation(int BusStationKey)
        {
            var save = curBusLineStations.FirstOrDefault(s => s.BusStationKey == BusStationKey).NumberStationInLine;
            if (curBusLineStations.Count == 1)
                Index = curAllBusLineStations.Count();
            else
                if (save > curAllBusLineStations.Count())
                Index = Index - 1;
            else
                Index = save - 1;

            curBusLineStations.Remove(curBusLineStations.FirstOrDefault(s => s.BusStationKey == BusStationKey));
            curBusLineStations.AsParallel().ForAll(a => { if (a.NumberStationInLine > save) { a.NumberStationInLine = a.NumberStationInLine - 1; } });
            lvBusLineStation.DataContext = Convert<object>(bL.StationDetails(curBusLineStations));
        }
        private void lvBusStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listViewItem = sender as ListView;
            curBusStation = listViewItem.DataContext as BO.BusStation;

        }
        private void listViewAllItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var senderList = sender as ListViewItem;
            curBusStation = senderList.DataContext as BO.BusStation;
            AddBusLinesStations(curBusStation);
        }
        private void listViewItemAdded_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var senderList = sender as ListViewItem;
            var Object = senderList.DataContext as object;
            DeleteBusLinesStation(int.Parse(Object.ToString().Substring(18, 6)));

        }
        private void FinishAddingBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

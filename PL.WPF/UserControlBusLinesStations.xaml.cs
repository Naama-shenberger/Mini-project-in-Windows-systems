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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for UserControlBusLines.xaml
    /// </summary>
    public partial class UserControlBusLines : UserControl
    {
        IBL bL;
        BO.BusStation curBusStation;//current bus Station
        static int Index;
        /// <summary>
        ///  ObservableCollections
        /// </summary>
        ObservableCollection<BO.BusLineStation> curBusLineStations = new ObservableCollection<BO.BusLineStation>();
        ObservableCollection<BO.BusLineStation> curAllBusLineStations = new ObservableCollection<BO.BusLineStation>();
        public UserControlBusLines(IBL _bL)
        {
            InitializeComponent();
            bL = _bL;
            curAllBusLineStations = Convert<BO.BusLineStation>(bL.GetAllBusLineStations());
            Index = curAllBusLineStations.Count();
            RefreshAllStationslv();
        }
        /// <summary>
        /// The function returns void
        /// The function Refresh Stations list view 
        /// </summary>
        void RefreshAllStationslv()
        {
            lvBusStations.DataContext = Convert<BO.BusStation>(bL.GetAllBusStation());
        }
        /// <summary>
        /// Collection Conversion Function to ObservableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        /// <summary>
        /// The function gets a bus stop to add
        /// </summary>
        /// <param name="busStation"></param>
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
        /// <summary>
        /// The function gets a station id to delete from a list of stations of a line that will be
        /// </summary>
        /// <param name="BusStationKey"></param>
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
        /// <summary>
        /// SelectionChanged of list view Bus Stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBusStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listViewItem = sender as ListView;
            curBusStation = listViewItem.DataContext as BO.BusStation;

        }
        /// <summary>
        /// MouseDoubleClick event
        /// list View Add item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAllItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var senderList = sender as ListViewItem;
            curBusStation = senderList.DataContext as BO.BusStation;
            AddBusLinesStations(curBusStation);
        }
        /// <summary>
        /// MouseDoubleClick event
        /// list View Item Added now to Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewItemAdded_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var senderList = sender as ListViewItem;
            var Object = senderList.DataContext as object;
            DeleteBusLinesStation(int.Parse(Object.ToString().Substring(18, 6)));

        }
        /// <summary>
        /// event click
        /// Finish btn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishAddingBtn_Click(object sender, RoutedEventArgs e)
        {
           
            
                try
                {
                   
                    bL.AddBusLinesStation(curBusLineStations);
                   
                    MessageBox.Show("The Station/s successfully added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.IdException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            
        }
    }
}

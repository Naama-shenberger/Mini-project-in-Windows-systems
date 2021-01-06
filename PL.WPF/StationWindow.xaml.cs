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
using BLAPI;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        IBL bl;
        BO.BusStation CurBusStation = new BO.BusStation();
        private ObservableCollection<BO.BusStation> StationList = new ObservableCollection<BO.BusStation>();//Bus List 
        public StationWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            ComboBoxBusStationKey.DisplayMemberPath = "StationName";
            ComboBoxBusStationKey.SelectedValuePath= "BusStationKey";
            ComboBoxBusStationKey.SelectedIndex = 0;
            RefreshAllStationsComboBox();
            listOfLines.DataContext = bl.GetAllBusLines();
        }
        private void RefreshAllStationsComboBox()
        {
            ComboBoxBusStationKey.DataContext = Convert<BO.BusStation>( bl.GetAllBusStation()); //ObserListOfStations;
            ComboBoxBusStationKey.SelectedIndex = 0;
            CurBusStation = Convert<BO.BusStation>(bl.GetAllBusStation()).ToList()[0];
        }
        private void ComboBoxBusStationKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurBusStation = (ComboBoxBusStationKey.SelectedItem as BO.BusStation);
            gridOneBusStation.DataContext = CurBusStation;
            if (CurBusStation != null)
            {
                RefreshBusLinesInStation();
                RefreshBusLinesInStation();
            }

        }
        private void RefreshBusLinesInStation()
        {
            if (CurBusStation.ListBusLinesInStation != null)
                list_lineInStation.DataContext = bl.LineDetails(CurBusStation.ListBusLinesInStation);
        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurBusStation != null)
                    bl.UpdateBusStation(CurBusStation);
                MessageBox.Show("The Station/s successfully updated", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshAllStationsComboBox();
                RefreshBusLinesInStation();

            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteCurStationButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected Station?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            try
            {
                if (CurBusStation != null)
                {
                    bl.DeleteBusStation(CurBusStation);
                    BO.BusStation BusStationToDel = CurBusStation;
                    MessageBox.Show("The Station/s successfully deleted", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshAllStationsComboBox();
                    RefreshBusLinesInStation();
                }
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow m = new MainWindow();
            m.Show();
        }

        private void AddNewStationButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewStationWindow addStationWindow = new AddNewStationWindow(bl);
            addStationWindow.ShowDialog();
            //addNewStationWindow.addStation
            RefreshAllStationsComboBox();
            RefreshBusLinesInStation();
        }

        //private void AddLineFromStationButton_Click(BO.BusLine busLine)
        //{

        //    try
        //    {
        //        if (CurBusStation.ListBusLinesInStation.FirstOrDefault(a => a.BusLineNumber == busLine.BusLineNumber) != null)
        //            throw new ArgumentException("You already have that line in Station");
        //        BO.BusLineInStation busLineStation = new BO.BusLineInStation
        //        {
        //            BusLineNumber = busLine.BusLineNumber,
        //            ID = busLine.ID,
        //            Area = (int)busLine.Area,
        //        };
        //        bl.AddBusLine(CurBusStation, busLineStation);
        //        RefreshBusLinesInStation();
        //        listOfLines.DataContext = Convert<object>(bl.BusLineDetails(CurBusStation.ListBusLinesInStation));
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        private void DelLineFromStation(BO.BusLineInStation busLine)
        {
            try
            {
                bl.DeleteBusLineFromStation(CurBusStation, busLine);
                RefreshBusLinesInStation();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddLineFromStation(BO.BusLine busLine)
        {

            try
            {
                bl.AddBusLineToStation(CurBusStation, busLine);
                RefreshBusLinesInStation();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddLineFromStationButton_Click(object sender, RoutedEventArgs e)
        {
            //var senderList = sender as ListViewItem;
            //bo = senderList.DataContext as BO.BusStation;
            //AddLineFromStation(curBusStation);
        }

        private void DelLineFromStationButton_Click(object sender, RoutedEventArgs e)
        {
            //var senderList = sender as ListViewItem;
            //BO.BusLineInStation Object = senderList.DataContext ;
            //DelLineFromStation(Object);

        }

    }
}


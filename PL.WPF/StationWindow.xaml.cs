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
            ListOfLines.DataContext = Convert < BO.BusLine >( bl.GetAllBusLines());
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
            List_lineInStation.DataContext = CurBusStation.ListBusLinesInStation;
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
            try
            {
                AddNewStationWindow addStationWindow = new AddNewStationWindow(bl);
                addStationWindow.ShowDialog();
                MessageBox.Show($"bus Line Station {addStationWindow.Bus_Station_Key} successfully added ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                //addNewStationWindow.addStation
                RefreshAllStationsComboBox();
                RefreshBusLinesInStation();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
            try
            {
                BO.BusLineInStation busLineInStation = new BO.BusLineInStation
                {
                    BusLineNumber = int.Parse((sender as CheckBox).DataContext.ToString().Substring(18, 6))
                };
                if (CurBusStation.ListBusLinesInStation.FirstOrDefault(id => id.BusLineNumber == busLineInStation.BusLineNumber) == null)
                {
                    //bl.AddBusLineToStation(CurBusStation, busLineInStation);
                }
                else
                    throw new ArgumentException("The line is already passing in station ");

                RefreshBusLinesInStation();
                MessageBox.Show($"bus Line Station {busLineInStation.BusLineNumber} successfully added ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                //(sender as CheckBox).Visibility = Visibility.Hidden;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                //(sender as CheckBox).IsChecked = false;
            }
            //var senderList = sender as ListViewItem;
            //bo = senderList.DataContext as BO.BusStation;
            //AddLineFromStation(curBusStation);
        }

        private void DelLineFromStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = sender as CheckBox;
                var Object = row.DataContext as object;
                var ID = int.Parse(Object.ToString().Substring(18, 6));
               // bl.DeleteBusLineStation(CurBusStation.BusStationKey, CurBusStation.ListBusLinesInStation.FirstOrDefault(i => i.BusLineNumber == ID).BusLineNumber);
                RefreshAllStationsComboBox();
                RefreshBusLinesInStation();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //private void BusLine_Checked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Index = CurBusLine.StationsInLine.Count() + 1;
        //        BO.BusLineStation busLineStation = new BO.BusLineStation
        //        {
        //            BusStationKey = int.Parse((sender as CheckBox).DataContext.ToString().Substring(18, 6)),
        //            NumberStationInLine = ++Index,
        //            Active = true
        //        };
        //        if (CurBusLine.StationsInLine.FirstOrDefault(id => id.BusStationKey == busLineStation.BusStationKey) == null)
        //        {
        //            CurBusLine.StationsInLine = CurBusLine.StationsInLine.Append(busLineStation);
        //            bl.AddBusStationToLine(CurBusLine, CurBusLine.StationsInLine);
        //        }
        //        else
        //            throw new ArgumentException("The line station is already on the line route");
        //        RefreshDataGrirdStationsline();
        //        MessageBox.Show($"bus Line Station {busLineStation.BusStationKey} successfully added ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        //        (sender as CheckBox).Visibility = Visibility.Hidden;
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
        //        (sender as CheckBox).IsChecked = false;
        //    }
        //}
    }
 }



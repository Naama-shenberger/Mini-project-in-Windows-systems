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
using System.Threading;
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
        private ObservableCollection<BO.BusStation> StationList = new ObservableCollection<BO.BusStation>();//station List 
        //private BO.BusStation currentDisplayStation;
        public StationWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            ComboBoxBusStationKey.DisplayMemberPath = "StationName";
            ComboBoxBusStationKey.SelectedValuePath = "BusStationKey";
            ComboBoxBusStationKey.SelectedIndex = 0;
            //lStationKey.ItemsSource = StationList;
            RefreshAllStationsComboBox();
            DataGridBusLinesInStation.IsReadOnly = true;
            busLinesDataGrid.IsReadOnly = true;
            DatatGridAllStations.IsReadOnly = true;
        }
        private void RefreshAllStationsComboBox()
        {
            ComboBoxBusStationKey.DataContext = Convert<BO.BusStation>(bl.GetAllBusStation()); //ObserListOfStations;
        }
        private void ComboBoxBusStationKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurBusStation = (ComboBoxBusStationKey.SelectedItem as BO.BusStation);
            gridOneBusStation.DataContext = CurBusStation;
            if (CurBusStation != null)
            {
                //list of courses of selected lines
                RefreshAllRegisteredBusLineInStaionGrid();
                //list of all lines(that selected station is not it)
                RefreshAllNotRegisteredBusLinesGrid();
                //RefreshTimeStatins();
            }

        }
        private void RefreshAllRegisteredBusLineInStaionGrid()
        {
            DataGridBusLinesInStation.DataContext = Convert<object>(bl.BusLineDetails(CurBusStation.ListBusLinesInStation));
        }
        private void RefreshAllNotRegisteredBusLinesGrid()
        {
            List<BO.BusLine> listOfUnRegisteredCourses = bl.GetAllBusLines().Where(c1 => CurBusStation.ListBusLinesInStation.All(c2 => c2.ID != c1.ID)).ToList();
            busLinesDataGrid.DataContext = Convert<object>(listOfUnRegisteredCourses);
        }
        private void RefreshTimeStatins()
        {
            DatatGridAllStations.DataContext = Convert<object>(bl.BusStationDetails(bl.GetAllBusStation()));
        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        private void btAddBusLineFromList_Click(object sender, RoutedEventArgs e)
        {
            if (CurBusStation == null)
            {
                MessageBox.Show("You must select a station first", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                BO.BusLineInStation cBO = ((sender as Button).DataContext as BO.BusLineInStation);
                bl.AddBusLineToStation(CurBusStation, cBO);
                RefreshAllRegisteredBusLineInStaionGrid();
                RefreshAllNotRegisteredBusLinesGrid();
                //RefreshTimeStatins();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btRemoverBusLineFronStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BusLineInStation scBO = ((sender as Button).DataContext as BO.BusLineInStation);
                bl.DeleteBusLineInStation(CurBusStation, scBO);
                RefreshAllRegisteredBusLineInStaionGrid();
                RefreshAllNotRegisteredBusLinesGrid();
                //RefreshTimeStatins();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateButten_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurBusStation != null)
                    bl.UpdateBusStation(CurBusStation);


            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddStationButten_Click(object sender, RoutedEventArgs e)
        {
            RefreshAllStationsComboBox();
            ComboBoxBusStationKey.Visibility = Visibility.Visible;
            LabelStationName.Visibility = Visibility.Visible;
            NameComboBox.Visibility = Visibility.Visible;
            AddressLabel.Visibility = Visibility.Hidden;
            TextBoxAddress.Visibility = Visibility.Hidden;
            BusStationLabel.Visibility = Visibility.Hidden;
            BusStationKeyTextBox.Visibility = Visibility.Hidden;
        }

        private void DeleteCurStationButten_Click(object sender, RoutedEventArgs e)
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
                    RefreshAllStationsComboBox();
                }
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btUpdateTimeBetweenStationsButten_Click(object sender, RoutedEventArgs e)
        {
            BO.BusStation scBO = ((sender as Button).DataContext as BO.BusStation);
            TimeSpan temp = bl.getTimeBetStation(CurBusStation, scBO);
            TimeWindow win = new TimeWindow(CurBusStation, scBO, temp);
            win.Closing += TimeStationWindow_Closing;
            win.ShowDialog();
        }
        private void TimeStationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            TimeSpan tBO = (sender as TimeWindow).timeBeforeUpdate;
            BO.BusStation toStation = (sender as TimeWindow).timeStation;
            MessageBoxResult res = MessageBox.Show("Update grade for selected student?", "Verification", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
            {
                (sender as TimeWindow).cbTime.Text = (sender as TimeWindow).timeBeforeUpdate.ToString();
            }
            else if (res == MessageBoxResult.Cancel)
            {
                (sender as TimeWindow).cbTime.Text = (sender as TimeWindow).timeBeforeUpdate.ToString();
                e.Cancel = true; //window stayed open. cancel closing event.
            }
            else
            {
                try
                {
                    bl.UpdateTravelTimeBetweenstations(CurBusStation, toStation, tBO);
                }
                catch (BO.IdException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }




        }

        //    private void lStationKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        var station = lStationKey.SelectedItem as BO.BusStation;
        //        if (station != null)
        //        {
        //            CurBusStation = ( BO.BusStation ) lStationKey.SelectedItem;
        //        }
        //    }
        //}
    }
}

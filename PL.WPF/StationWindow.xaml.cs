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
        }
        private void RefreshAllStationsComboBox()
        {
            ComboBoxBusStationKey.DataContext = Convert<BO.BusStation>( bl.GetAllBusStation()); //ObserListOfStations;
        }
        private void ComboBoxBusStationKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurBusStation = (ComboBoxBusStationKey.SelectedItem as BO.BusStation);
            gridOneBusStation.DataContext = CurBusStation;
            if (CurBusStation != null)
            {
                
            }

        }
         
        private void RefreshTimeStatins()
        {

            //List<BO.BusStation> listOfStations = bl.GetAllBusStation().Where(bl.GetBusStation(b1 => b1 != CurBusStation.BusStationKey));
            DatatGridAllStations.DataContext =  bl.GetAllBusStation();
        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        private void btUpdateTimeBetweenStations_Click(object sender, RoutedEventArgs e)
        {
            //BO. scBO = ((sender as Button).DataContext as BO.StudentCourse);
            //GradeWindow win = new GradeWindow(scBO);
            //win.Closing += WinUpdateGrade_Closing;
            //win.ShowDialog();
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
                bl.AddBusLineToStation(CurBusStation,cBO);
                
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
                bl.DeleteBusLineInStation(CurBusStation,scBO);
                
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurBusStation != null)
                    bl.UpdateBusStation(CurBusStation);
                RefreshAllStationsComboBox();

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
                    RefreshAllStationsComboBox();
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

        private void btUpdateTimeBetweenStationsButton_Click(object sender, RoutedEventArgs e)
        {
            BO.BusStation scBO = ((sender as Button).DataContext as BO.BusStation);
            TimeSpan temp = bl.getTimeBetStation(CurBusStation, scBO);
            TimeWindow win = new TimeWindow(CurBusStation,scBO ,temp);
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

        private void AddNewStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddNewStationWindow addStationWindow = new AddNewStationWindow(bl);
                addStationWindow.ShowDialog();
                bl.AddBusStation(addStationWindow.curBusStation);
                RefreshAllStationsComboBox();
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

        private void lineInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}


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
            ComboBoxBusStationKey.DisplayMemberPath = "BusStationKey";
            ComboBoxBusStationKey.SelectedIndex = 0;
            RefreshAllStationsComboBox();
            RefreshAllRegisteredBusLineInStaionGrid();
            RefreshAllNotRegisteredBusLinesGrid();
        }
        void RefreshAllStationsComboBox()
        {
            ComboBoxBusStationKey.DataContext = Convert<BO.BusStation>( bl.GetAllBusStation()); //ObserListOfStations;
        }
        private void ComboBoxBusStationKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurBusStation = (ComboBoxBusStationKey.SelectedItem as BO.BusStation);
            gridOneBusStation.DataContext = CurBusStation;
        }
        void RefreshAllRegisteredBusLineInStaionGrid()
        {
            DataGridBusLinesInStation.DataContext = Convert<object>(bl.BusLineDetails(bl.GetAllBusLineInStation()));
        }
        //void RefreshDataGrirdAllStationslines()
        //{
        //    DataGrirdAllStationslines.DataContext = Convert<object>(bl.StationDetails(bl.GetAllBusLineStations()));
        //}
        void RefreshAllNotRegisteredBusLinesGrid()
        {
            List<BO.BusLine> listOfUnRegisteredCourses = bl.GetAllBusLines().Where(c1 => CurBusStation.ListBusLinesInStation.All(c2 => c2.ID != c1.ID)).ToList();
            DataGridBusLines.DataContext = Convert < BO.BusLine >( listOfUnRegisteredCourses);
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
                BO.BusLineInStation scBO = ((sender as Button).DataContext as BO.BusLineInStation);
                bl.AddBusLineInStation(scBO);
                RefreshAllRegisteredBusLineInStaionGrid();
                RefreshAllNotRegisteredBusLinesGrid();
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
                bl.DeleteBusLineInStation((scBO));
                RefreshAllRegisteredBusLineInStaionGrid();
                RefreshAllNotRegisteredBusLinesGrid();
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
    }
}

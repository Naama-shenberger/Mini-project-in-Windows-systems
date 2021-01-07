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
            DataGrirdAllLines.IsReadOnly = true;
            DataGrirdAllLinesInStation.IsReadOnly = true;


        }
        private void RefreshAllStationsComboBox()
        {
            ComboBoxBusStationKey.DataContext = bl.GetAllBusStation().ToList(); //ObserListOfStudents;

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
            if (CurBusStation != null && CurBusStation.ListBusLinesInStation != null)
            {
                DataGrirdAllLinesInStation.DataContext = CurBusStation.ListBusLinesInStation;
            }
        }
        private void RefreshBusLines()
        {
            if (CurBusStation != null && CurBusStation.ListBusLinesInStation != null)
            {
                DataGrirdAllLinesInStation.DataContext = CurBusStation.ListBusLinesInStation;
            }
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
                    if (CurBusStation.ListBusLinesInStation == null)
                        MessageBox.Show($"No bus line contained at station {CurBusStation.BusStationKey} ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshAllStationsComboBox();
                    RefreshBusLinesInStation();
                    RefreshBusLines();
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
                bl.AddBusStation(addStationWindow.BusStation);
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



        private void AddLineFromStationButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurBusStation == null)
            {
                MessageBox.Show("You must select a student first", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                BO.BusLine blBO = ((sender as Button).DataContext as BO.BusLine);
                bl.AddBusLineToStation(CurBusStation, blBO);
                RefreshBusLinesInStation();
                RefreshBusLines();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DelLineFromStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BusLineInStation blisBO = ((sender as Button).DataContext as BO.BusLineInStation);
                bl.DeleteBusLineFromStation(CurBusStation, blisBO);
                MessageBox.Show($"bus Line Station {blisBO.BusLineNumber} successfully added ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshBusLinesInStation();
                RefreshBusLines();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                //(sender as CheckBox).IsChecked = false;
            }

        }

        private void TextBoxAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}



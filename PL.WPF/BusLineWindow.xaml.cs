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
    /// Interaction logic for BusLineWindow.xaml
    /// </summary>
    public partial class BusLineWindow : Window
    {
        IBL bl;
        BO.BusLine CurBusLine = new BO.BusLine();
        ObservableCollection<BO.BusLineStation> CurBusLineStation=new ObservableCollection<BO.BusLineStation>();
        private ObservableCollection<BO.BusLine> BusLineList = new ObservableCollection<BO.BusLine>();
        public BusLineWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            AreaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Zones));
            ComboBoxBusLineNumber.DisplayMemberPath = "BusLineNumber";
            ComboBoxBusLineNumber.SelectedIndex = 0;
            RefreshAllBusLinesComboBox();
            RefreshDataGrirdAllStationslines();
            RefreshDataGrirdStationsline();
            CurBusLineStation = Convert<BO.BusLineStation>(CurBusLine.StationsInLine);


        }
        void RefreshDataGrirdAllStationslines()
        {
            DataGrirdAllStationslines.DataContext = Convert<object>(bl.StationDetails(bl.GetAllBusLineStations()));
        }
        void RefreshAllBusLinesComboBox()
        {
            ComboBoxBusLineNumber.DataContext = Convert<BO.BusLine>(bl.GetAllBusLines()); //ObserListOfStudents;
            CurBusLine = Convert<BO.BusLine>(bl.GetAllBusLines()).ToList()[0];
        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        private void cbBusBusLineNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurBusLine = (ComboBoxBusLineNumber.SelectedItem as BO.BusLine);
            gridOneBusLine.DataContext = CurBusLine;
            if(CurBusLine==null)
                CurBusLine = Convert<BO.BusLine>(bl.GetAllBusLines()).ToList()[0];
            RefreshDataGrirdStationsline();
           
        }
        private void RemoveStation_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AddBusLineButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshAllBusLinesComboBox();
            BusLineLabel.Visibility = Visibility.Visible;
            BusLineNumberTextBox.Visibility = Visibility.Visible;
            AreaComboBox.Visibility = Visibility.Visible;
            LabelArea.Visibility = Visibility.Visible;
            TextBoxFristStop.Visibility = Visibility.Hidden;
            FriststopLabel.Visibility = Visibility.Hidden;
            LastStopLabel.Visibility = Visibility.Hidden;
            LastStopTextBox.Visibility = Visibility.Hidden;

        }

        private void DeleteButon_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected student?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            try
            {
                if (CurBusLine != null)
                {
                    bl.DeleteBusLine(CurBusLine);
                    BO.BusLine BusLineToDel = CurBusLine;
                    RefreshAllBusLinesComboBox();
                }
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
                if (CurBusLine != null)
                    bl.UpdateBusLine(CurBusLine);


            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BusLineStation_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.BusLineStation busLineStation = new BO.BusLineStation
                {
                    BusStationKey = int.Parse((sender as CheckBox).DataContext.ToString().Substring(18, 6)),
                    NumberStationInLine = int.Parse((sender as CheckBox).DataContext.ToString().Substring(48, 1)),
                    Active = true
                };
                if (CurBusLineStation.FirstOrDefault(id => id.BusStationKey == busLineStation.BusStationKey) == null)
                    CurBusLineStation.Add(busLineStation);
                else
                    throw new ArgumentException("The line station is already on the line route");
                //(sender as CheckBox)
                RefreshDataGrirdStationsline();

            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                (sender as CheckBox).IsChecked = false;
            }
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    BO.BusLine busLine = new BO.BusLine
                    {
                        BusLineNumber = int.Parse(BusLineNumberTextBox.Text),
                        Area = (BO.Enums.Zones)AreaComboBox.SelectedItem,
                        Active = true

                    };
                    bl.AddBusLine(busLine, CurBusLineStation);
                    BusLineNumberTextBox.Visibility = Visibility.Hidden;
                    BusLineLabel.Visibility = Visibility.Hidden;
                    RefreshAllBusLinesComboBox();
                    MessageBox.Show("The bus was successfully added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    ComboBoxBusLineNumber.SelectedIndex = 0;
                }
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                ComboBoxBusLineNumber.SelectedIndex = 0;
            }
        }

        void RefreshDataGrirdStationsline()
        {


            DataGrirdStationslines.DataContext =bl.StationDetails(CurBusLineStation);


        }
        
        private void AddBusLineStatoin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddStationWindow addStationWindow = new AddStationWindow(bl);
                addStationWindow.ShowDialog();
                bl.AddBusLinesStation(addStationWindow.BusLineStations);
                RefreshDataGrirdAllStationslines();
               
              //  RefreshDataGrirdStationsline();
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
                MessageBox.Show(ex.Message,"Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        
    }
}

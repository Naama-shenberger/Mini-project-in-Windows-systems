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
        static int Index = 0;
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
            LastStopcb.ItemsSource = FirstStopcb.ItemsSource = bl.GetAllBusLineStations();
            LastStopcb.DisplayMemberPath = FirstStopcb.DisplayMemberPath = "BusStationKey";
            RefreshAllBusLinesComboBox();
            RefreshDataGrirdAllStationslines();
            RefreshDataGrirdStationsline();
            CurBusLineStation =Convert<BO.BusLineStation>(CurBusLine.StationsInLine);
            Index = CurBusLine.StationsInLine.Count();
  
        }
        void RefreshDataGrirdAllStationslines()
        {
            DataGrirdAllStationslines.DataContext = Convert<object>(bl.StationDetails(bl.GetAllBusLineStations()));
        }
        void RefreshAllBusLinesComboBox()
        {
            ComboBoxBusLineNumber.DataContext = Convert<BO.BusLine>(bl.GetAllBusLines()); //ObserListOfStudents;
            ComboBoxBusLineNumber.SelectedIndex = 0;
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
            if (CurBusLine!=null && CurBusLine.StationsInLine != null)
                DataGrirdStationslines.DataContext = bl.StationDetails(CurBusLine.StationsInLine);
            else
                DataGrirdStationslines.DataContext = "";
            RefreshDataGrirdAllStationslines();
        }
        private void RemoveStation_Click(object sender, RoutedEventArgs e)
        {
            var btn= sender as Button;
            var save = CurBusLine.StationsInLine.FirstOrDefault(c => c.BusStationKey == int.Parse((btn).DataContext.ToString().Substring(18, 6)));
            if (CurBusLine.StationsInLine.Count() == 1)
                Index = CurBusLine.StationsInLine.Count();
            else
              if (save.NumberStationInLine > CurBusLine.StationsInLine.Count())
                Index = Index - 1;
            else
                Index = save.NumberStationInLine - 1;
            CurBusLine.StationsInLine.AsParallel().ForAll(a => { if (a.NumberStationInLine > save.NumberStationInLine) { a.NumberStationInLine = a.NumberStationInLine - 1; } });
            bl.DeleteBusLineStationFromeLine(CurBusLine,save);
            RefreshDataGrirdAllStationslines();
            RefreshDataGrirdStationsline();
        }
        private void AddBusLineButton_Click(object sender, RoutedEventArgs e)
        {
            AddBusLineWindow addBusLineWindow = new AddBusLineWindow(bl);
            addBusLineWindow.ShowDialog();
           // addBusLineWindow.BusLine;
            RefreshAllBusLinesComboBox();
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
        private void UpdateIndexInLine_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var save = CurBusLine.StationsInLine.FirstOrDefault(c => c.BusStationKey == int.Parse((btn).DataContext.ToString().Substring(18, 6)));
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
                Index = CurBusLine.StationsInLine.Count()+1;
                BO.BusLineStation busLineStation = new BO.BusLineStation
                {
                    BusStationKey = int.Parse((sender as CheckBox).DataContext.ToString().Substring(18, 6)),
                    NumberStationInLine =++Index,
                    Active = true
                };
                if (CurBusLine.StationsInLine.FirstOrDefault(id => id.BusStationKey == busLineStation.BusStationKey) == null)
                {
                    CurBusLine.StationsInLine=CurBusLine.StationsInLine.Append(busLineStation);
                    bl.AddBusStationToLine(CurBusLine, CurBusLine.StationsInLine);
                }
                else
                    throw new ArgumentException("The line station is already on the line route");
                RefreshDataGrirdStationsline();
                MessageBox.Show($"bus Line Station {busLineStation.BusStationKey} successfully added ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                (sender as CheckBox).Visibility = Visibility.Hidden;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                (sender as CheckBox).IsChecked = false;
            }
        }
       

        void RefreshDataGrirdStationsline()
        {
            DataGrirdStationslines.DataContext = Convert<object>(bl.StationDetails(CurBusLine.StationsInLine));
        }

        private void AddBusLineStatoin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddStationWindow addStationWindow = new AddStationWindow(bl);
                addStationWindow.ShowDialog();
                bl.AddBusLinesStation(addStationWindow.BusLineStations);
                RefreshDataGrirdAllStationslines();
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
                MessageBox.Show(ex.Message,"Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }
    }
}

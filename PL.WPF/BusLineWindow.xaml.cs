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
using System.Text.RegularExpressions;
namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for BusLineWindow.xaml
    /// </summary>
    public partial class BusLineWindow : Window
    {
        IBL bl;
        static int Index = 0;//Index update
        BO.BusLine CurBusLine = new BO.BusLine();//current Bus Line
        private ObservableCollection<BO.BusLine> BusLineList = new ObservableCollection<BO.BusLine>();//Bus Line List 
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
            Treeview.ItemsSource = bl.GetAllBusLinesGroupByArea();
           
        }
        /// <summary>
        /// Refresh Data Grird
        /// </summary>
        void RefreshDataGrirdAllStationslines()
        {
            DataGrirdAllStationslines.DataContext = Convert<object>(bl.StationDetails(bl.GetAllBusLineStations()));
        }
        /// <summary>
        /// Refresh Combo Box
        /// </summary>
        void RefreshAllBusLinesComboBox()
        {
            ComboBoxBusLineNumber.DataContext= BusLineList = Convert<BO.BusLine>(bl.GetAllBusLines()); //ObserListOfStudents;
            ComboBoxBusLineNumber.SelectedIndex = 0;
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
        /// Combo Box Bus Line Number SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBusBusLineNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurBusLine = (ComboBoxBusLineNumber.SelectedItem as BO.BusLine);
            gridOneBusLine.DataContext = CurBusLine;
            if (CurBusLine != null && CurBusLine.StationsInLine != null)
                DataGrirdStationslines.DataContext = bl.StationDetails(CurBusLine.StationsInLine);
            if (CurBusLine != null && CurBusLine.lineRides != null)
                lvExpander.DataContext = CurBusLine.lineRides;
        }
        /// <summary>
        /// Click event
        /// Deleting a bus line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDelBusLineStation_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var save = int.Parse((btn).DataContext.ToString().Substring(18, 6));
            bl.DeleteBusLineStationFromeLine(CurBusLine, bl.GetAllBusLineStations().FirstOrDefault(predicate => predicate.BusStationKey == save));
            bl.DeleteBusLineStation(bl.GetAllBusLineStations().FirstOrDefault(predicate => predicate.BusStationKey == save));
            RefreshDataGrirdAllStationslines();
            RefreshDataGrirdStationsline();
        }
        /// <summary>
        /// Click event
        /// Add a station to the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthAddBusLineStationToLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Index = CurBusLine.StationsInLine.Count() + 1;
                BO.BusLineStation busLineStation = new BO.BusLineStation
                {
                    BusStationKey = int.Parse((sender as Button).DataContext.ToString().Substring(18, 6)),
                    NumberStationInLine = ++Index,
                    Active = true
                };
                if (CurBusLine.StationsInLine.FirstOrDefault(id => id.BusStationKey == busLineStation.BusStationKey && CurBusLine.ID == id.ID) == null)
                {
                    try
                    {
                        CurBusLine.StationsInLine = CurBusLine.StationsInLine.Append(busLineStation);
                        if (Ddistancetb.Text == "")
                            Ddistancetb.Text = "0";
                        if (TimePickerDistance.Text == null)
                            TimePickerDistance.Text = "0";
                        bl.AddBusStationToLine(CurBusLine, CurBusLine.StationsInLine, float.Parse(Ddistancetb.Text), TimeSpan.Parse(Regex.Replace(TimePickerDistance.Text, "[A-Za-z ]", "")));
                    }
                    catch (BO.IdException ex)
                    {
                        Ddistancetb.Visibility = Visibility.Visible;
                        TimePickerDistance.Visibility = Visibility.Visible;
                        bl.AddBusLine(CurBusLine, CurBusLine.StationsInLine, float.Parse(Ddistancetb.Text), TimeSpan.Parse(Regex.Replace(TimePickerDistance.Text, "[A-Za-z ]", "")));
                    }
                }
                else
                    throw new ArgumentException("The line station is already on the line route");
                RefreshDataGrirdStationsline();
                MessageBox.Show($"bus Line Station {busLineStation.BusStationKey} successfully added ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
               
            }
            
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }
        /// <summary>
        /// Click event
        /// Deleting a bus stop from a line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            RefreshDataGrirdStationsline();
        }
        /// <summary>
        /// Click event
        /// Add Bus Line event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBusLineButton_Click(object sender, RoutedEventArgs e)
        {
            AddBusLineWindow addBusLineWindow = new AddBusLineWindow(bl);
            addBusLineWindow.ShowDialog();
            var save = addBusLineWindow.BusLine;
            try
            {
               
                if (Ddistancetb.Text == "")
                    Ddistancetb.Text = "0";
                if (TimePickerDistance.Text == null)
                {
                    TimePickerDistance = new MaterialDesignThemes.Wpf.TimePicker {Text="0" };
                   // TimePickerDistance.Text = "0";
                }
                bl.AddBusLine(save, save.StationsInLine, float.Parse(Ddistancetb.Text), TimeSpan.Parse(Regex.Replace(TimePickerDistance.Text, "[A-Za-z ]", "")));
                BusLineList.Add(save);
            }
            catch(BO.IdException ex)
            {
                Ddistancetb.Visibility = Visibility.Visible;
                TimePickerDistance.Visibility = Visibility.Visible;
                bl.AddBusLine(save, save.StationsInLine, float.Parse(Ddistancetb.Text), TimeSpan.Parse(Regex.Replace(TimePickerDistance.Text, "[A-Za-z ]", "")));
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No typing details please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Click event
        /// Deleting a bus line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Click event
        ///  updates station location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateIndexInLine_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var save = CurBusLine.StationsInLine.FirstOrDefault(c => c.BusStationKey == int.Parse((btn).DataContext.ToString().Substring(18, 6)));
            var inex = from sin in CurBusLine.StationsInLine
                       orderby sin.NumberStationInLine descending
                       select sin;
            Update update = new Update(bl, save, CurBusLine, inex.ToList()[0].NumberStationInLine);
            update.Closed += UpdateIndexInLine_Closed;
            update.ShowDialog();
        }
        /// <summary>
        /// Closing event for UpdateIndexInLine_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateIndexInLine_Closed(object sender, EventArgs e)
        {
            RefreshDataGrirdStationsline();
        }
        /// <summary>
        /// Click event
        /// The function updates a bus line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// The function Refresh DataGrird -Stationsline
        /// </summary>
        void RefreshDataGrirdStationsline()
        { 
            if (CurBusLine!=null && CurBusLine.StationsInLine != null)
            {
                DataGrirdStationslines.DataContext = Convert<object>(bl.StationDetails(CurBusLine.StationsInLine)).Distinct();
            }
        }
        /// <summary>
        /// Click event
        /// Add a line/s station from a list of stations 
        /// Using the window AddBusLineWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Back button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }
        /// <summary>
        /// Data Grird 'Stationslines'-SelectionChanged Function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrirdStationslines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurBusLine != null && CurBusLine.StationsInLine != null)
            {
                DataGrirdStationslines.DataContext = Convert<object>(bl.StationDetails(CurBusLine.StationsInLine).Distinct());
            }
        }
    }
}

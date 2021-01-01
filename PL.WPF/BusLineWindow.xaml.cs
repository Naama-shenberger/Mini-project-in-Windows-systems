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
        private ObservableCollection<BO.BusLine> BusLineList = new ObservableCollection<BO.BusLine>();
        public BusLineWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            AreaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Zones));
            ComboBoxBusLineNumber.DisplayMemberPath = "BusLineNumber";
            ComboBoxBusLineNumber.SelectedIndex = 0;
            RefreshAllBusLinesComboBox();
        }
        void RefreshAllBusLinesComboBox()
        {
            ComboBoxBusLineNumber.DataContext = Convert<BO.BusLine>(bl.GetAllBusLines()); //ObserListOfStudents;
        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        private void cbBusBusLineNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurBusLine = (ComboBoxBusLineNumber.SelectedItem as BO.BusLine);
            gridOneBusLine.DataContext = CurBusLine;
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
                    AddStationWindow addStationWindow =new AddStationWindow(bl);
                    addStationWindow.ShowDialog();
                    BO.BusLineStation lineStationOne = addStationWindow.CurBusLineStation;
                    //AddStationWindow addStationWindow1 = new AddStationWindow(bl);
                    //addStationWindow1.Show();
                    BO.BusLineStation lineStationTwo = addStationWindow.CurBusLineStation;
                    bl.AddBusLine(busLine,lineStationOne,lineStationTwo);
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
    }
}

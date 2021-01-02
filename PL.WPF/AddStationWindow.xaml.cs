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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        IBL bL;
        BO.BusStation curBusStation;
        BO.BusLineStation curBusLineStation;
        public AddStationWindow(IBL _bL)
        {
            InitializeComponent();
            bL = _bL;
            ComboBoxAllStations.DisplayMemberPath = " BusStationKey";
            ComboBoxAllStations.SelectedIndex = 0;
            RefreshAllStationsComboBox();
        }
        void RefreshAllStationsComboBox()
        {
             ComboBoxAllStations.DataContext = Convert<BO.BusStation>(bL.GetAllBusStation());
        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        public BO.BusLineStation CurBusLineStation
        {
            get { return curBusLineStation; }
            set { curBusLineStation = value; }
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    BO.BusLineStation Add = new BO.BusLineStation
                    {
                        BusStationKey = int.Parse(stationTextBlock.Text),
                        NumberStationInLine = int.Parse(NumberStationInLineTextBox.Text),
                        Active=true
                        
                    };
                    CurBusLineStation = Add;
                    this.Close();
                }
              
            }
            catch(BO.IdException)
            {

            }
            
        }
        private void AllStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curBusStation = (ComboBoxAllStations.SelectedItem as BO.BusStation);
            GridStation.DataContext = curBusStation;
        }
    }
}

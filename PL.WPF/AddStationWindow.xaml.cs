using BLAPI;
using System;
using System.Collections.Generic;
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
        BO.BusLineStation curBusLineStation;
        public AddStationWindow(IBL _bL)
        {
            InitializeComponent();
            bL = _bL;
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
                        BusStationKey = int.Parse(stationTextBox.Text),
                        StationAddress = stationAddressTextBox.Text,
                        StationName = StationNameTextBox.Text,
                        NumberStationInLine = int.Parse(NumberStationInLineTextBox.Text)
                    };
                    curBusLineStation = Add;
                    this.Close();
                }
              
            }
            catch(BO.IdException)
            {

            }
            
        }
    }
}

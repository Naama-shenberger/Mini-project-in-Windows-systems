using BLAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddLineRideWindow.xaml
    /// </summary>
    public partial class AddLineRideWindow : Window
    {
        /// <summary>
        /// Adding Line Ride
        /// </summary>
        IBL bL;
        BO.BusLine CurBusLine;
        public AddLineRideWindow(IBL _BL,BO.BusLine busLine)
        {
            InitializeComponent();
            bL = _BL;
            CurBusLine = busLine;
        }

        /// <summary>
        /// event Click
        /// Add Line Ride
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.LineRides lineRides = new BO.LineRides
                {
                    BusLineNumber = CurBusLine.BusLineNumber,
                    ID = CurBusLine.ID,
                    CodeLastStasion = CurBusLine.LastStopNumber,
                    NameLastStasion = bL.GetBusStation(CurBusLine.LastStopNumber).StationName,
                    Active = true,
                    TravelStartTime = TimeSpan.Parse(Regex.Replace(StartTimePicker.Text, "[A-Za-z ]", "")),
                    TravelEndTime = TimeSpan.Parse(Regex.Replace(EndTimePicker.Text, "[A-Za-z ]", "")),
                    BusDepartureNumber = TimeSpan.Parse(Regex.Replace(ExitEvery.Text, "[A-Za-z ]", "")),
                };
                CurBusLine.lineRides = CurBusLine.lineRides.Append(lineRides);
                bL.AddLineRides(lineRides);
                this.Close();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

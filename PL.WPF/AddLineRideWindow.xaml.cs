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
        int runNumberOfBusLine;
        public AddLineRideWindow(IBL _BL,int RunNumber)
        {
            InitializeComponent();
            bL = _BL;
            runNumberOfBusLine= RunNumber;
        }

        /// <summary>
        /// event Click
        /// Add Line Ride
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            BO.LineRides lineRides = new BO.LineRides
            {
                ID = runNumberOfBusLine,
                Active =true,
                TravelStartTime = TimeSpan.Parse(Regex.Replace(StartTimePicker.Text, "[A-Za-z ]", "")),
                TravelEndTime = TimeSpan.Parse(Regex.Replace(EndTimePicker.Text, "[A-Za-z ]", "")),
                BusDepartureNumber = TimeSpan.Parse(Regex.Replace(ExitEvery.Text, "[A-Za-z ]", "")),
            };
          //  bL.Add
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

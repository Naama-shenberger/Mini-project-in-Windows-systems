using BLAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for TravelerWindow.xaml
    /// Traveler Window, partial of the  SimulateOneStationWindow
    /// </summary>
    public partial class TravelerWindow : Window
    {
        IBL bL;
        BO.BusStation CurBusStation;//Current bus stop
        public TravelerWindow(IBL _bL)
        {
            InitializeComponent();
            bL = _bL;
            ///Before accessing the window, we need to see if there is access
            User user = new User(bL);
            user.ShowDialog();
            AllBusStaionsDataGrid.DataContext = bL.GetAllBusStation();
            AllBusStaionsDataGrid.IsReadOnly = true;
            AlllinesDataGrid.IsReadOnly = true;
        }
        /// <summary>
        /// event click
        /// Simulate Button
        /// Open a window to see the simulation of the bus lines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simulationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurBusStation == null)
                        throw new ArgumentNullException();
                SimulateOneStationWindow simulateOneStationWindow = new SimulateOneStationWindow(bL, CurBusStation);

                simulateOneStationWindow.ShowDialog();
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("No station selected");
            }
        }
        /// <summary>
        /// event  SelectionChangedEventArgs
        /// Updates the list of lines by clicking on each station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllBusStaionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurBusStation = AllBusStaionsDataGrid.SelectedItem as BO.BusStation;
            AlllinesDataGrid.DataContext = (CurBusStation).ListBusLinesInStation.ToList();
        }
        /// <summary>
        /// event click
        /// back btn
        /// Returns to the  access window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            access access = new access(bL);
            access.ShowDialog();

        }
    }
}

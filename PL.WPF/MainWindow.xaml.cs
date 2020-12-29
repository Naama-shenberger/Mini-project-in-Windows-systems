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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLAPI;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
          
        }
        /// <summary>
        /// A button event that opens a new window to represent buses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BusDisplay_Click(object sender, RoutedEventArgs e)
        {
            new BusWindow().Show();
            Close();
        }
        /// <summary>
        /// A button event that opens a new window to represent bus lines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BusLinesDisplay_Click(object sender, RoutedEventArgs e)
        {
            new BusLineWindow().Show();
            Close();
        }
        /// <summary>
        /// A button event that opens a new window to represent stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationsDisplay_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow().Show();
            Close();
        }
    }
}

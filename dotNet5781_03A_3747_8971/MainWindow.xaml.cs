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
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_02_3747_8971;
/// <summary>
/// Exercise 3A - Mini project in c#
/// Name:Naama Shenberger 
/// Id:211983747
/// Name:Ella Hanzin
/// Id:212028971
/// <summary>
namespace dotNet5781_03A_3747_8971
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Object from class List of bus lines
        /// </summary>
        private ListBusLines MyBusList = new ListBusLines();
        public MainWindow()
        {
            InitializeComponent();
            Initialize_ListOfBuses();
            cbBusLines.ItemsSource = MyBusList;
            cbBusLines.DisplayMemberPath = "BUSLINE";
            cbBusLines.SelectedIndex = 0;
            ShowBusLine(currentDisplayBusLine.BUSLINE);
        }
        /// <summary>
        /// Object from class SingleBusLine
        /// </summary>
        private SingleBusLine currentDisplayBusLine;
        /// <summary>
        /// Gets a line number, and returns the object
        /// </summary>
        /// <param name="index"></param>
        private void ShowBusLine(string index)
        {
            currentDisplayBusLine = MyBusList[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as SingleBusLine).BUSLINE);
        }
        ///Adding and deleting a bus stop can only be seen after switching to another line 
        ///and then clicking again on the line that has been added or deleted.
        //We know this is not the best way to add and delete a station,
        //but this was not required for the exercise so we still thought of leaving it :)
        /// <summary>
        /// Button for adding a bus stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                currentDisplayBusLine.AddStation(textBox1.Text);
                ShowBusLine(currentDisplayBusLine.BUSLINE);
            }
            catch (MyException) { MessageBox.Show("Station code already exists","ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error); }
        }
        /// <summary>
        /// Bus station delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                currentDisplayBusLine.DelStation(textBox2.Text);
                ShowBusLine(currentDisplayBusLine.BUSLINE);
            }
            catch (MyException) { MessageBox.Show("There is no such station code","ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error); }
        }
        /// <summary>
        ///Initialize bus lines and stations
        /// </summary>
        private void Initialize_ListOfBuses()
        {
            try
            {
                for (int i = 0; i < 10; i++)
                    MyBusList.AddBusLine(BusStation.r.Next(0, 1000).ToString());
                foreach (var Bus in MyBusList)
                    Bus.AddStation(BusStation.r.Next(0, 1000).ToString());
            }
            catch (MyException) { MessageBox.Show("ERROR","mistake",MessageBoxButton.OKCancel, MessageBoxImage.Error); }
        }
    }
}



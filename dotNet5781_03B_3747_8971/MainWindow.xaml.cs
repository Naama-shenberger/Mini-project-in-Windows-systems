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
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using dotNet5781_01_3747_8971;
using System.Globalization;
using static dotNet5781_01_3747_8971.Bus;
/// <summary>
/// Exercise 3B - Mini project in c#
/// Name:Naama Shenberger 
/// Id:211983747
/// Name:Ella Hanzin
/// Id:212028971
/// <summary>
namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Using the ObservableCollection that we can see changes in runTime
        /// the ObservableCollection implementaion the interface INotifyPropertyChanged
        /// </summary>
        private ObservableCollection<Bus> BusList = new ObservableCollection<Bus>();
        private Bus currentDisplayBus;
        public MainWindow()
        {
            InitializeComponent();
            InitilizeBusList();
            lbBuses.ItemsSource = BusList;
        }
        /// <summary>
        /// Add A Bus Click event
        /// sign up for an event AddBusWindow_Closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddABus_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow secondWindow = new SecondWindow();
            secondWindow.Closed += AddBusWindow_Closed;
            secondWindow.ShowDialog();
        }
        /// <summary>
        /// The event gets the object that we  want to add to the list
        /// If the bus is already found will throw an exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBusWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                Bus resultBus = (Bus)(sender as SecondWindow).CurrentDisplayBusLine;
                if (resultBus != null)
                {
                    foreach (Bus Item in BusList)
                        if (Item.LICENSEPLATE == resultBus.LICENSEPLATE)
                            throw new InvalidOperationException("The Already exists in the system");
                }
                else
                    throw new InvalidOperationException("Lack of typing details");

                BusList.Add(resultBus);
                System.Windows.MessageBox.Show($"Bus: {resultBus.LICENSEPLATE} is Added", "Added Bus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException  message)
            {
                System.Windows.MessageBox.Show($"{message.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Function that receives a string (license number) and returns the object
        /// </summary>
        /// <param name="BusLicenseNumber"></param>
        /// <returns></returns>
        public object GetItem(string BusLicenseNumber)
        {
            foreach (var Bus in BusList)
                if (Bus.LICENSEPLATE == BusLicenseNumber)
                    return Bus;
            throw new InvalidOperationException("The License Number does not exist");
        }
        /// <summary>
        /// Initialized list function
        /// One bus will be after the next treatment date
        /// One bus will be close to the next treatment passenger
        ///  One bus will be with little fuel
        /// And adds statuses and colors to buses
        /// </summary>
        private void InitilizeBusList()
        {
            try
            {
                DateTime date_Activity = new DateTime(Bus.r.Next(1895, 2021), Bus.r.Next(1, 13), Bus.r.Next(1, 29));
                DateTime date_Treatment = new DateTime(Bus.r.Next(2022, 2024), Bus.r.Next(1, 13), Bus.r.Next(1, 29));
                int Min = date_Activity.Year < 2018 ? 1000000 : 10000000;
                int Max = Min.ToString().Length < 8 ? 10000000 : 100000000;
                BusList.Add(new Bus(Bus.r.Next(Min, Max).ToString(),
                    date_Activity, date_Treatment
                   , (float)(Bus.r.NextDouble() * (19999 - 1) + 1),
                    (float)(Bus.r.NextDouble() * (1199 - 1) + 1),
                    (float)(Bus.r.NextDouble() * (500000 - 1) + 1)));
                date_Activity = new DateTime(Bus.r.Next(1895, 2021), Bus.r.Next(1, 13), Bus.r.Next(1, 29));
                date_Treatment = new DateTime(Bus.r.Next(date_Activity.Year, 2021), Bus.r.Next(1, 12), Bus.r.Next(1, 29));
                Min = date_Activity.Year < 2018 ? 1000000 : 10000000;
                Max = Min.ToString().Length < 8 ? 10000000 : 100000000;
                BusList.Add(new Bus(Bus.r.Next(Min, Max).ToString(),
                     date_Activity, date_Treatment
           , (float)(Bus.r.NextDouble() * (19999.9 - 19999) + 19999),
                     (float)(Bus.r.NextDouble() * (1199 - 1) + 1),
                    (float)(Bus.r.NextDouble() * (500000 - 1) + 1)));
                date_Activity = new DateTime(Bus.r.Next(1895, 2021), Bus.r.Next(1, 13), Bus.r.Next(1, 29));
                date_Treatment = new DateTime(Bus.r.Next(date_Activity.Year, 2021), Bus.r.Next(1, 12), Bus.r.Next(1, 29));
                Min = date_Activity.Year < 2018 ? 1000000 : 10000000;
                Max = Min.ToString().Length < 8 ? 10000000 : 100000000;
                BusList.Add(new Bus(Bus.r.Next(Min, Max).ToString(), date_Activity,
                        date_Treatment, (float)(Bus.r.NextDouble() * (19999 - 1) + 1),
                         (float)(Bus.r.NextDouble() * (1199.9 - 1198) + 1198),
                        (float)(Bus.r.NextDouble() * (500000 - 1) + 1)
                        ));

                for (int i = 0; i < 7; i++)
                {
                    date_Activity = new DateTime(Bus.r.Next(1895, 2021), Bus.r.Next(1, 13), Bus.r.Next(1, 29));
                    date_Treatment = new DateTime(Bus.r.Next(date_Activity.Year, 2021), Bus.r.Next(1, 12), Bus.r.Next(1, 29));
                    Min = date_Activity.Year < 2018 ? 1000000 : 10000000;
                    Max = Min.ToString().Length < 8 ? 10000000 : 100000000;
                    BusList.Add(new Bus(Bus.r.Next(Min, Max).ToString(),
                         date_Activity, date_Treatment
                        ,
                        (float)(Bus.r.NextDouble() * (19999 - 1) + 1),
                        (float)(Bus.r.NextDouble() * (1199 - 1) + 1),
                        (float)(Bus.r.NextDouble() * (500000 - 1) + 1)
                        ));
                }
                for (int i = 0; i < 10; i++)
                {
                    if (!BusList[i].FuelCondition() && !BusList[i].TreatmentIsNeeded())
                    {
                        BusList[i].STATUS = (Situation)(0);
                        BusList[i].Color = "#FFB3F6BE";
                    }
                    else
                    {
                        BusList[i].STATUS = (Situation)4;
                        BusList[i].Color = "#FFF49494";
                    }
                    }
                
            }
            catch (InvalidOperationException message)
            {
                System.Windows.MessageBox.Show($"{message.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Travel Click Event
        /// The event opens a window for adding a number (driving distance)
        /// The function is registered for events BackgroundWorker 
        /// function updates the status of the bus and its color
        /// The function throws an exception in case it is in another process
        /// (Logically it is not possible to refuel during treatment
        /// And it is not possible to travel during treatment or refueling, etc.)
        /// The function checks that the bus is fit for travel if not  there will be a throw Exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Travel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ThirdWindow thirdWindow = new ThirdWindow();
                System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
                currentDisplayBus = (Bus)GetItem(button.DataContext.ToString());
                currentDisplayBus.worker.DoWork += currentDisplayBus.Worker_DoWork;
                currentDisplayBus.worker.ProgressChanged += currentDisplayBus.Worker_ProgressChanged;
                currentDisplayBus.worker.RunWorkerCompleted += currentDisplayBus.Worker_RunWorkerCompleted;
                currentDisplayBus.worker.WorkerReportsProgress = true;
                currentDisplayBus.worker.WorkerSupportsCancellation = true;
                if (currentDisplayBus.worker.IsBusy != true)
                {
                    if (currentDisplayBus.FuelCondition())
                        throw new InvalidOperationException("The bus needs a refueling");
                    if (currentDisplayBus.TreatmentIsNeeded())
                        throw new InvalidOperationException("The bus needs a Treatment");
                    thirdWindow.TransferObjectBus = currentDisplayBus;
                    thirdWindow.ShowDialog();
                    if (currentDisplayBus.TimeTravel == 0)
                        throw new InvalidOperationException("You did not enter details");
                    currentDisplayBus.Color = "#FFE8BDE7";
                    currentDisplayBus.STATUS = Situation.In_the_middle_of_A_ride;
                  
                    System.Windows.MessageBox.Show("bus starting to Travel", "Bus information Situation", MessageBoxButton.OK);
                    currentDisplayBus.worker.RunWorkerAsync((int)currentDisplayBus.TimeTravel);
                }
                else
                    throw new InvalidOperationException($"The bus cannot travel because he is { currentDisplayBus.STATUS}");
            }
            catch (InvalidOperationException message)
            {
                System.Windows.MessageBox.Show($"{message.Message}", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// MouseDoubleClick event for item list
        /// The event opens a window with the bus details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as ListBoxItem;
            Bus lineData = btn.DataContext as Bus;
            ShowInfoBus a = new ShowInfoBus(lineData);
            a.ShowDialog();
        }
        /// <summary>
        /// Refuel Click Event
        /// the event  Sends the selected object to the refuel function in the window ShowInfoBus 
        /// (save code, and its more accurately that the MainWindow will use a Another window (programmable),Than the opposite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            currentDisplayBus = (Bus)GetItem(button.DataContext.ToString());
            ShowInfoBus a = new ShowInfoBus(currentDisplayBus);
            a.refuel_Click(sender, e);
        }
        /// <summary>
        /// SelectionChanged buslist event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            var bus = lbBuses.SelectedItem as Bus;
            if (bus != null)
            {
                currentDisplayBus = (Bus)lbBuses.SelectedItem;
            }
        }
        /// <summary>
        /// StopProcess Click event
        /// The event checks that indeed the selected object is in some process and it stops it
        /// if The bus is not in process A message will be sent to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopProcess_Click(object sender, RoutedEventArgs e)
        {

            try {
                System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
                currentDisplayBus = (Bus)GetItem(button.DataContext.ToString());
                if (currentDisplayBus.worker.WorkerSupportsCancellation == true && currentDisplayBus.worker.IsBusy == true)
                    // Cancel the asynchronous operation.
                    currentDisplayBus.worker.CancelAsync();
                else
                    throw new InvalidOperationException();

            }
            catch (InvalidOperationException )
            {
                System.Windows.MessageBox.Show("The bus is not in process", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

       
    }
}
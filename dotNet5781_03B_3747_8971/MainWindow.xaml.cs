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


namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Bus> BusList = new ObservableCollection<Bus>();
        private Bus currentDisplayBus;
        public MainWindow()
        {
            InitializeComponent();
            InitilizeBusList();
            lbBuses.ItemsSource = BusList;
           

        }
        
        private void AddABus_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow secondWindow = new SecondWindow();
            secondWindow.Closed += AddBusWindow_Closed;
            secondWindow.ShowDialog();
        }
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
        public object GetItem(string BusLicenseNumber)
        {
            foreach (var Bus in BusList)
                if (Bus.LICENSEPLATE == BusLicenseNumber)
                    return Bus;
            throw new InvalidOperationException("The License Number does not exist");
        }
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
                        BusList[i].Color = "#FFA3F4B0";
                    }
                    else
                    {
                        BusList[i].STATUS = (Situation)4;
                        BusList[i].Color = "#FFBD5850";
                    }
                    }
                
            }
            catch (InvalidOperationException message)
            {
                System.Windows.MessageBox.Show($"{message.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Travel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ThirdWindow thirdWindow = new ThirdWindow();
                System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
                currentDisplayBus = (Bus)GetItem(button.DataContext.ToString());
                if (currentDisplayBus.FuelCondition())
                    throw new InvalidOperationException("The bus needs a refueling");
                if (currentDisplayBus.TreatmentIsNeeded())
                    throw new InvalidOperationException("The bus needs a Treatment");
                currentDisplayBus.worker.DoWork += currentDisplayBus.Worker_DoWork;
                currentDisplayBus.worker.ProgressChanged += currentDisplayBus.Worker_ProgressChanged;
                currentDisplayBus.worker.RunWorkerCompleted += currentDisplayBus.Worker_RunWorkerCompleted;
                currentDisplayBus.worker.WorkerReportsProgress = true;
                currentDisplayBus.worker.WorkerSupportsCancellation = true;
                if (currentDisplayBus.worker.IsBusy != true)
                {
                    thirdWindow.TransferObjectBus = currentDisplayBus;
                    thirdWindow.ShowDialog();
                    currentDisplayBus.Color = "#FF0F4EB9";
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
        private void ListBoxItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as ListBoxItem;
            Bus lineData = btn.DataContext as Bus;
            ShowInfoBus a = new ShowInfoBus(lineData);
            a.ShowDialog();
        }
        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            currentDisplayBus = (Bus)GetItem(button.DataContext.ToString());
            ShowInfoBus a = new ShowInfoBus(currentDisplayBus);
            a.refuel_Click(sender, e);
        }          
        private void lbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            var bus = lbBuses.SelectedItem as Bus;
            if (bus != null)
            {
                currentDisplayBus = (Bus)lbBuses.SelectedItem;
            }
        }
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
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
using static dotNet5781_01_3747_8971.Bus;


namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool EnabledT = true;
        public bool EnabledR = true;
        public bool EnabledTreatment = true;
        BackgroundWorker worker;
        public MainWindow()
        {
            InitializeComponent();
            InitilizeBusList();
            lbBuses.ItemsSource = BusList;
           // TreatmentButton = "EnabledTreatment";
           // lbBuses.DrawItem = DrawMode.OwnerDrawFixed;
           // lbBuses.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
           // Controls.Add(ListBox1);
           worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;


        }
        private void ListBoxItem_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {

        }
        private void DrawItemEventHandler(System.Windows.Controls.ListBox ListBoxItem_DrawItem)
        {

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
                foreach (Bus Item in BusList)
                    if (Item.LICENSEPLATE == resultBus.LICENSEPLATE)
                        throw new InvalidOperationException("The Already exists in the system");

                BusList.Add(resultBus);
            }
            catch (InvalidOperationException message)
            {
                System.Windows.MessageBox.Show($"{message.Message}", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }
        private ObservableCollection<Bus> BusList = new ObservableCollection<Bus>();
        private Bus currentDisplayBusLine;
        //public void AddBus(object Newbus)
        //{
        //    var adding = Newbus as Bus;
        //    if (adding != null)
        //    {
        //        foreach (Bus item in BusList)
        //        {
        //            if (item.LICENSEPLATE == adding.LICENSEPLATE)
        //                throw new ArithmeticException("The plate number is already in the system");
        //        }
        //        BusList.Add(adding);
        //        return;
        //    }
        //    throw new NullReferenceException("The object Can not be added to the list because he is not Bus type");
        //}
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
                    //date activ
                    date_Activity = new DateTime(Bus.r.Next(1895, 2021), Bus.r.Next(1, 13), Bus.r.Next(1, 29));
                    date_Treatment = new DateTime(Bus.r.Next(date_Activity.Year, 2021), Bus.r.Next(1, 12), Bus.r.Next(1, 29));
                   Min = date_Activity.Year < 2018 ? 1000000 : 10000000;
                    Max = Min.ToString().Length < 8 ? 10000000 : 100000000;
                   // double k = Bus.r.NextDouble() * (33.3 - 31) + 31;
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
                        BusList[i].STATUS = (Situation)(0);
                    else
                        BusList[i].STATUS = (Situation)4;

                }
            }
            catch (InvalidOperationException message)
            {
                System.Windows.MessageBox.Show($"{message.Message}", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            


        }
        //private  void AddABus_Click(object sender, RoutedEventArgs e)
        //{
        //    SecondWindow secondWindow = new SecondWindow();
        //    secondWindow.ShowDialog();
        //}
        /// <summary>
        /// 51
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Treatment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var bus = lbBuses.SelectedItem as Bus;
        //    if (bus != null)
        //    {
        //        currentDisplayBusLine = (Bus)lbBuses.SelectedItem;
        //        currentDisplayBusLine.EnabledTreatment = false;

        //    }

        //}
        private void Travel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ThirdWindow thirdWindow = new ThirdWindow();
                System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
                currentDisplayBusLine = (Bus)GetItem(button.DataContext.ToString());
                if (currentDisplayBusLine.STATUS == Situation.Dangerous|| currentDisplayBusLine.STATUS == Situation.In_treatment || currentDisplayBusLine.STATUS == Situation.refueling || currentDisplayBusLine.STATUS == Situation.In_the_middle_of_A_ride)
                    throw new InvalidOperationException($"The bus cannot travel because of he is {currentDisplayBusLine.STATUS}");
                if (currentDisplayBusLine.FuelCondition())
                    throw new InvalidOperationException("The bus needs a refueling");
                if (currentDisplayBusLine.TreatmentIsNeeded())
                    throw new InvalidOperationException("The bus needs a Treatment");
                
                thirdWindow.listboxTextBlockNameValue = currentDisplayBusLine;
                thirdWindow.ShowDialog();
                if (worker.IsBusy != true)
                {
                 
                    TreatmentButton.IsEnabled = false;
                   // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(EnabledTreatment.ToString()));
                   // EnabledR = false;
                   // EnabledTreatment = false;
                   currentDisplayBusLine.STATUS = Situation.In_the_middle_of_A_ride;
                    System.Windows.MessageBox.Show("ff", "bus start Travel ", MessageBoxButton.OKCancel);
                    worker.RunWorkerAsync(currentDisplayBusLine.TimeTravel);
                }

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
            // MessageBox.Show(lineData.ToString());
        }
        private void Treatment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentDisplayBusLine.STATUS == Situation.Dangerous||currentDisplayBusLine.STATUS == Situation.In_treatment || currentDisplayBusLine.STATUS == Situation.refueling || currentDisplayBusLine.STATUS == Situation.In_the_middle_of_A_ride)
                    throw new InvalidOperationException($"The bus cannot travel because of he is {currentDisplayBusLine.STATUS}");
                System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
                currentDisplayBusLine = (Bus)GetItem(button.DataContext.ToString());
             
                if (worker.IsBusy != true)
                {
                    currentDisplayBusLine.STATUS = Situation.In_treatment;
                    System.Windows.MessageBox.Show("ff", "bus start treatment", MessageBoxButton.OKCancel);
                    worker.RunWorkerAsync(144);
                    currentDisplayBusLine.kILOMETERS_TREATMENT = 0;
                    currentDisplayBusLine.KILOMETERSGAS = 0;
                    currentDisplayBusLine.DATETREATMET = DateTime.Now;
                   // currentDisplayBusLine.STATUS = (Situation)3;

                }
            }
            catch (InvalidOperationException message)
            {
                System.Windows.MessageBox.Show($"{ message.Message}", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
        }
        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            ///תהליך
            try
            {
                if (currentDisplayBusLine.STATUS == Situation.Dangerous||currentDisplayBusLine.STATUS == Situation.In_treatment || currentDisplayBusLine.STATUS == Situation.refueling || currentDisplayBusLine.STATUS == Situation.In_the_middle_of_A_ride)
                    throw new InvalidOperationException($"The bus cannot travel because of he is {currentDisplayBusLine.STATUS}");
                System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
                currentDisplayBusLine = (Bus)GetItem(button.DataContext.ToString());
                if (currentDisplayBusLine.KILOMETERSGAS == 0)
                {
                    throw new IndexOutOfRangeException("The bus is already refueled");
                   
                }
                if (worker.IsBusy != true)
                {
                    currentDisplayBusLine.STATUS = Situation.refueling;
                    System.Windows.MessageBox.Show("ff", "bus start refuel");
                    worker.RunWorkerAsync(12);
                    currentDisplayBusLine.KILOMETERSGAS = 0;

                }
            }
            catch (InvalidOperationException message)
            {
                System.Windows.MessageBox.Show($"{ message.Message}", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
            catch(System.Windows.Markup.XamlParseException)
            {
                System.Windows.MessageBox.Show("Error");
            }
        }
        //public bool TravelTest(object obj)
        //{
        //    var BusItem = obj as Bus;
        //    if (BusItem != null)
        //    {
        //        if (GetItem(BusItem.LICENSEPLATE) is Bus)
        //        {
        //            currentDisplayBusLine = (Bus)GetItem(BusItem.LICENSEPLATE);
        //            if (currentDisplayBusLine.TreatmentIsNeeded())
        //                throw new InvalidOperationException("The bus needs a Treatment");
        //            if (currentDisplayBusLine.FuelCondition())
        //                throw new InvalidOperationException("The bus needs a refueling");
        //            //סטטוס
        //            return true;
        //        }
        //        throw new NullReferenceException("The object Can not be added to the list because he is not Bus type");
        //    }
        //   throw new NullReferenceException("The object Can not be added to the list because he is not Bus type");
        //}

        private void lbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bus = lbBuses.SelectedItem as Bus;
            if (bus != null)
            {
                currentDisplayBusLine = (Bus)lbBuses.SelectedItem;
            }
          
          
        }
        //private void cbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    currentDisplayBusLine= (Bus)lbBuses.SelectedItem;

        //}
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // BackgroundWorker worker = sender as BackgroundWorker;
            int length = (int)e.Argument;

            for (int i = 1; i <= length; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    e.Result = stopwatch.ElapsedMilliseconds; // Unnecessary
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(1000);
                    worker.ReportProgress(i * 100 / length);
                }
            }

            e.Result = stopwatch.ElapsedMilliseconds;

        }
        /// <summary>
        /// 50,77
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //int progress = e.ProgressPercentage;
            ////resultLabel.Content = (progress + "%");
            ////resultProgressBar.Value = progress;
            //System.Windows.MessageBox.Show((progress + "%"));
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                 //e.Result throw new System.InvalidOperationException


            }
            else if (e.Error != null)
            {
                // e.Result throw System.Reflection.TargetInvocationException
               // resultLabel.Content = "Error: " + e.Error.Message; //Exception Message
            }
            else
            {
                long result = (long)e.Result;
                if (currentDisplayBusLine.STATUS == Situation.In_treatment)
                   System.Windows.MessageBox.Show("ff", "bus finished treatment", MessageBoxButton.OKCancel);
                if (currentDisplayBusLine.STATUS == Situation.refueling)
                    System.Windows.MessageBox.Show("ff", "bus finished refueling", MessageBoxButton.OKCancel);
                if (currentDisplayBusLine.STATUS == Situation.In_the_middle_of_A_ride)
                    System.Windows.MessageBox.Show("ff", "bus finished ", MessageBoxButton.OKCancel);
                currentDisplayBusLine.STATUS = Situation.Ready_to_go;
                //if (result ==20000)
                // .Content = "Done after " + result + " ms.";

                // resultLabel.Content = "Done after " + result / 1000 + " sec.";
            }

        }


    }
}

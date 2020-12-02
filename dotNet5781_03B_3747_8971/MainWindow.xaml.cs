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
using dotNet5781_01_3747_8971;
using static dotNet5781_01_3747_8971.Bus;


namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitilizeBusList();
            lbBuses.ItemsSource = BusList;

           // lbBuses.DrawItem = DrawMode.OwnerDrawFixed;
           // lbBuses.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
           // Controls.Add(ListBox1);
        }
        public event System.Windows.Forms.DrawItemEventHandler DrawItem;
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
                DateTime date_Activity = new DateTime(Bus.r.Next(1895, 2021), Bus.r.Next(1, 13), Bus.r.Next(1, 32));
                int Min = date_Activity.Year < 2018 ? 1000000 : 10000000;
                int Max = Min.ToString().Length < 8 ? 10000000 : 100000000;
                BusList.Add(new Bus(Bus.r.Next(Min, Max).ToString(),
                    date_Activity,
                    new DateTime(Bus.r.Next(2022, 2024), Bus.r.Next(1, 13), Bus.r.Next(1, 32)), (float)(Bus.r.NextDouble() * (19999 - 1) + 1),
                    (float)(Bus.r.NextDouble() * (1199 - 1) + 1),
                    (float)(Bus.r.NextDouble() * (500000 - 1) + 1)));
                date_Activity = new DateTime(Bus.r.Next(1895, 2021), Bus.r.Next(1, 13), Bus.r.Next(1, 32));
                Min = date_Activity.Year < 2018 ? 1000000 : 10000000;
                Max = Min.ToString().Length < 8 ? 10000000 : 100000000;
                BusList.Add(new Bus(Bus.r.Next(Min, Max).ToString(),
                     date_Activity,
                     new DateTime(Bus.r.Next(date_Activity.Year,2021), Bus.r.Next(1, 12), Bus.r.Next(1, 32)), (float)(Bus.r.NextDouble() * (19999.9 - 19999) + 19999),
                     (float)(Bus.r.NextDouble() * (1199 - 1) + 1),
                    (float)(Bus.r.NextDouble() * (500000 - 1) + 1)));
                date_Activity = new DateTime(Bus.r.Next(1895, 2021), Bus.r.Next(1, 13), Bus.r.Next(1, 32));
                Min = date_Activity.Year < 2018 ? 1000000 : 10000000;
                Max = Min.ToString().Length < 8 ? 10000000 : 100000000;
                BusList.Add(new Bus(Bus.r.Next(Min, Max).ToString(), date_Activity,
                          new DateTime(Bus.r.Next(date_Activity.Year, 2021), Bus.r.Next(1, 12), Bus.r.Next(1, 32)), (float)(Bus.r.NextDouble() * (19999 - 1) + 1),
                         (float)(Bus.r.NextDouble() * (1199.9 - 1198) + 1198),
                        (float)(Bus.r.NextDouble() * (500000 - 1) + 1)
                        ));
                for (int i = 0; i < 7; i++)
                {
                    //date activ
                    date_Activity = new DateTime(Bus.r.Next(1895, 2021), Bus.r.Next(1, 13), Bus.r.Next(1, 32));
                    Min = date_Activity.Year.CompareTo(2018) < 0 ? 1000000 : 10000000;
                    Max = Min.ToString().Length < 8 ? 10000000 : 100000000;
                   // double k = Bus.r.NextDouble() * (33.3 - 31) + 31;
                    BusList.Add(new Bus(Bus.r.Next(Min, Max).ToString(),
                        date_Activity,
                          new DateTime(Bus.r.Next(date_Activity.Year, 2021), Bus.r.Next(1, 12), Bus.r.Next(1, 32)),
                        (float)(Bus.r.NextDouble() * (19999 - 1) + 1),
                        (float)(Bus.r.NextDouble() * (1199 - 1) + 1),
                        (float)(Bus.r.NextDouble() * (500000 - 1) + 1)
                        ));
                }
                for (int i = 0; i < 10; i++)
                {
                    if (!BusList[i].FuelCondition() &&!BusList[i].TreatmentIsNeeded())
                        BusList[i].STATUS = (Situation)(0); 
                    //else
                    //{
                    //    if (BusList[i].TreatmentIsNeeded())
                    //        BusList[i].STATUS = (Situation)3;
                    //    else
                    //        BusList[i].STATUS = (Situation)(0);
                    //}
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
        private void Travel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ThirdWindow thirdWindow = new ThirdWindow();
                System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
                currentDisplayBusLine = (Bus)GetItem(button.DataContext.ToString());
                if (currentDisplayBusLine.FuelCondition())
                    throw new InvalidOperationException("The bus needs a refueling");
                if (currentDisplayBusLine.TreatmentIsNeeded())
                    throw new InvalidOperationException("The bus needs a Treatment");
                if (currentDisplayBusLine.STATUS == Situation.In_treatment || currentDisplayBusLine.STATUS == Situation.refueling)
                     throw new InvalidOperationException($"A bus cannot travel because of he is {currentDisplayBusLine.STATUS}");
                thirdWindow.listboxTextBlockNameValue = currentDisplayBusLine;
                thirdWindow.ShowDialog();
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
            //תהליך
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            currentDisplayBusLine = (Bus)GetItem(button.DataContext.ToString());
            currentDisplayBusLine.kILOMETERS_TREATMENT = 0;
            currentDisplayBusLine.KILOMETERSGAS = 0;
            currentDisplayBusLine.DATETREATMET = DateTime.Now;
            currentDisplayBusLine.STATUS = (Situation)3;
        }
        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            ///תהליך
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            currentDisplayBusLine = (Bus)GetItem(button.DataContext.ToString());
            if (currentDisplayBusLine.KILOMETERSGAS == 0)
            {
                System.Windows.MessageBox.Show("The bus is already refueled", "refuel_info", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                return;
            }
            currentDisplayBusLine.KILOMETERSGAS = 0;
            currentDisplayBusLine.STATUS = (Situation)2;
            System.Windows.MessageBox.Show("The bus successfully refueled", "refuel_info", MessageBoxButton.OKCancel, MessageBoxImage.Information);

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
    }
}

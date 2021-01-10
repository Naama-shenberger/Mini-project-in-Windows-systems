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
using System.Windows.Shapes;
using BLAPI;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for BusWindow.xaml
    /// </summary>
    public partial class BusWindow : Window
    {
        IBL bl;
        BO.Bus CurBus = new BO.Bus();
        private ObservableCollection<BO.Bus> BusList = new ObservableCollection<BO.Bus>();//Bus List 
        public BusWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            cbBusLicensePlate.DisplayMemberPath = "LicensePlate";
            cbBusLicensePlate.SelectedIndex = 0;
            RefreshAllBusesComboBox();
            Treeview.ItemsSource = bl.GetAllBussGroupByTreatmentIsNeeded();
        }
        /// <summary>
        ///  Collection Conversion Function to ObservableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        /// <summary>
        /// Refresh All BusesComboBox
        /// </summary>
        void RefreshAllBusesComboBox()
        {
            cbBusLicensePlate.DataContext = Convert<BO.Bus>(bl.GetAllBus()); //ObserListOfStudents;
        }
        private void cbBusLicensePlate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurBus = (cbBusLicensePlate.SelectedItem as BO.Bus);
            gridOneBus.DataContext = CurBus;
            StatusLabel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Click event 
        /// Sending a bus for treatment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Treatment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurBus != null)
                    bl.BusInTreatment(CurBus);
                KmTreatmentTextBox.Text = "0";
                KmGasTextBox.Text = "0";
                DateTreatmentDatePicker.Text= DateTime.Now.ToString();
               
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        ///  Click event 
        /// Sending a bus for refuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refuel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurBus != null)
                    bl.RefillingBus(CurBus);
               
                KmGasTextBox.Text = "0";

            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }
        /// <summary>
        /// event click 
        /// Update Bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurBus != null)
                    bl.UpdateBus(CurBus);

               
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// event click 
        /// Deleting a bus 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBus_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected student?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            try
            {
                if (CurBus != null)
                {
                    bl.DeleteBus(CurBus);
                    BO.Bus BusToDel = CurBus;
                    RefreshAllBusesComboBox();
                }
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
        /// <summary>
        /// event click
        /// Add bus click 
        /// info in OnKeyDownHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            RefreshAllBusesComboBox();
            LabelLicensePlate.Visibility = Visibility.Visible;
            LicensePlateTextBox.Visibility = Visibility.Visible;
            StatusLabel.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// A function that adding a bus 
        ///the functuon takes the details from box texts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    CurBus = new BO.Bus
                    {
                        LicensePlate = LicensePlateTextBox.Text,
                        DateActivity = (DateTime)DateActivityDatePicker.SelectedDate,
                        DateTreatment = (DateTime)DateTreatmentDatePicker.SelectedDate,
                        KilometersTreatment = float.Parse(KmTreatmentTextBox.Text),
                        KilometersGas = float.Parse(KmGasTextBox.Text),
                        Totalkilometers = float.Parse(TotalkmTextBox.Text),
                        AirTire = float.Parse(AirTireTextBox.Text),
                        OilCondition = bool.Parse(OilConditionTextBox.Text),Active=true
                        
                    };

                    if (CurBus != null)
                        bl.AddABus(CurBus);
                    LabelLicensePlate.Visibility = Visibility.Hidden;
                    LicensePlateTextBox.Visibility = Visibility.Hidden;
                    RefreshAllBusesComboBox();
                    MessageBox.Show("The bus was successfully added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    cbBusLicensePlate.SelectedIndex = 0;
                }
            }
            catch(BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                cbBusLicensePlate.SelectedIndex = 0;
            }
        }
    }
}

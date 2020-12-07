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
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using dotNet5781_01_3747_8971;
using static dotNet5781_01_3747_8971.Bus;

namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for ShowInfoBus.xaml
    /// </summary>
    public partial class ShowInfoBus : Window
    {
        private Bus currentDisplayBus;
        public ShowInfoBus(object currentBus)
        {
            InitializeComponent();
            var BusObject = currentBus as Bus;
            if (BusObject != null)
            {
                currentDisplayBus = (Bus)currentBus;
                tbDate_Activiy.Text = currentDisplayBus.DATEACTIVITY.ToString("dd/MM/yyyy");
                tbDate_Treatmet.Text = currentDisplayBus.DATETREATMET.ToString("dd/MM/yyyy");
                tbKilometers_refueling.Text = currentDisplayBus.KILOMETERSGAS.ToString();
                tbKilometers_Treatmet.Text = currentDisplayBus.kILOMETERS_TREATMENT.ToString();
                tbTotal_Kilometers.Text = currentDisplayBus.TOTALKILOMETERS.ToString();
            }
            else
                throw new NullReferenceException("The object is not type Bus");
          
        }
        public void refuel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                currentDisplayBus.worker.DoWork += currentDisplayBus.Worker_DoWork;
                currentDisplayBus.worker.ProgressChanged += currentDisplayBus.Worker_ProgressChanged;
                currentDisplayBus.worker.RunWorkerCompleted += currentDisplayBus.Worker_RunWorkerCompleted;
                currentDisplayBus.worker.WorkerReportsProgress = true;
                currentDisplayBus.worker.WorkerSupportsCancellation = true;
                if (currentDisplayBus.worker.IsBusy != true)
                {
                    if (currentDisplayBus.KILOMETERSGAS == 0)
                        throw new InvalidOperationException("The bus is already refueled");
                    System.Windows.MessageBox.Show("bus starting to refuel", "Bus information Situation", MessageBoxButton.OK);
                    currentDisplayBus.STATUS = Situation.refueling;
                    currentDisplayBus.Color = "#FFE7A956";
                    currentDisplayBus.worker.RunWorkerAsync(12);
                    this.Close();
                }
                else
                    throw new InvalidOperationException($"The bus cannot travel because he is { currentDisplayBus.STATUS}");

            }
            catch (InvalidOperationException message)
            {
                System.Windows.MessageBox.Show($"{ message.Message}", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            }
        }
        private void Treatment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                currentDisplayBus.worker.DoWork += currentDisplayBus.Worker_DoWork;
                currentDisplayBus.worker.ProgressChanged += currentDisplayBus.Worker_ProgressChanged;
                currentDisplayBus.worker.RunWorkerCompleted += currentDisplayBus.Worker_RunWorkerCompleted;
                currentDisplayBus.worker.WorkerReportsProgress = true;
                currentDisplayBus.worker.WorkerSupportsCancellation = true;
                if (currentDisplayBus.worker.IsBusy != true)
                {
                    System.Windows.MessageBox.Show("bus starting treatment", "Bus information Situation", MessageBoxButton.OK);
                    currentDisplayBus.STATUS = Situation.In_treatment;
                    currentDisplayBus.Color = "#FF1853A8";
                    currentDisplayBus.worker.RunWorkerAsync(144);
                }
                else
                    throw new InvalidOperationException($"The bus cannot travel because he is {currentDisplayBus.STATUS}");
                this.Close();

            }
            catch (InvalidOperationException message)
            {
                System.Windows.MessageBox.Show($"{ message.Message}", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

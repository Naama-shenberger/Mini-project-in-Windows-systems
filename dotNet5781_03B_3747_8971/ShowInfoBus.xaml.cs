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
        /// <summary>
        /// Create an object from a class Bus,That we can access the methods and public fields of the class 
        /// </summary>
        private Bus currentDisplayBus;
        /// <summary>
        /// The constructor of the class (of this window) receives an object-type parameter 
        /// so that we can transfer an object between the windows
        /// the constructor checks that the objecct is from Bus type
        /// </summary>
        /// <param name="currentBus"></param>
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
                throw new NullReferenceException("The object is not Bus type");
          
        }
        /// <summary>
        /// Click refuel event 
        /// The function is registered for events BackgroundWorker 
        /// function updates the status of the bus and its color
        /// The function throws an exception in case it is in another process
        /// (Logically it is not possible to refuel during treatment
        /// And it is not possible to travel during treatment or refueling, etc.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    currentDisplayBus.Color = "#FFF0E3C2";
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
        /// <summary>
        /// Click Treatment event 
        /// The function is registered for events BackgroundWorker 
        /// function updates the status of the bus and its color
        /// The function throws an exception in case it is in another process
        // (Logically it is not possible to refuel during treatment
        // And it is not possible to travel during treatment or refueling, etc.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    currentDisplayBus.Color = "#FF4E80C8";
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
        /// <summary>
        /// A click event that closes the class window, the button actually displays the previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;

namespace dotNet5781_01_3747_8971
{
    /// <summary>
    /// Bus class, which allows access to the fields by methods 'set' and 'get'
    /// Functions that check the integrity and update of buses
    /// The class implements the interface INotifyPropertyChanged
    /// </summary>
    class Bus : INotifyPropertyChanged
    {
        
        public static Random r = new Random();
        private string l_licensePlate;
        private DateTime d_dateActivity;
        private DateTime d_dateTreatment;
        private float k_kilometersTreatment;
        private float k_kilometersGas;
        private float t_totalkilometers;
        private Situation status;
        /// We saw the need to add fields to the bus class:
        private string color;//Bus color
        private float timeTravel;//Bus ride time
        private string _timeLeft;//Time left for travel
        private int _progress;//progress bar
        int Workerlength;//Process length
        /// <summary>
        /// Using of BackgroundWorker
        /// The class provides the option for a background process that is not the main process 
        /// to make changes to the graphical interface safely.
        /// </summary>
        public BackgroundWorker worker = new BackgroundWorker();
        private Stopwatch stopwatch = new Stopwatch();
        /// <summary>
        /// Variable type enum
        /// The bus can be in one of the situations
        ///  In case the bus is not in treatment/ refueled or if he is not Ready to go,it will receive the status 'Dangerous'
        /// </summary>
        public enum Situation { Ready_to_go, In_the_middle_of_A_ride, refueling, In_treatment, Dangerous }
        public Bus(string _licensePlate, DateTime _dateActivity, DateTime _dateTreatment,
            float _kilometersTreatment, float _kilometersGas, float _totalkilometers)
        {
            DATEACTIVITY = _dateActivity;
            LICENSEPLATE = _licensePlate;
            DATETREATMET = _dateTreatment;
            kILOMETERS_TREATMENT = _kilometersTreatment;
            KILOMETERSGAS = _kilometersGas;
            t_totalkilometers = _totalkilometers;
        }
        public Bus() { }
        /// <summary>
        /// Realization of the interface NotifyPropertyChanged
        /// that we can see the product on to the graphical interface In a dynamic way
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// set and get functions for private fields
        /// </summary>
        public string TimeLeft
        {
            get { return _timeLeft; }
            set { _timeLeft = value; NotifyPropertyChanged("TimeLeft"); }
        }
        public int Progress
        {
            get { return _progress; }
            set { _progress = value; NotifyPropertyChanged("Progress"); }
        }
        public int WorkerLength
        {
            get { return Workerlength; }
        }
        public string Color
        {
            get { return color; }
            set { color = value; NotifyPropertyChanged("Color"); }

        }
        public Situation STATUS
        {
            get { return status;  }
            set { status = value; NotifyPropertyChanged("STATUS"); }
        }
        public float TimeTravel
        {
            get { return timeTravel; }
            set { timeTravel = value; }
        }
        public float KILOMETERSGAS
        {
            get { return k_kilometersGas; }
            set { k_kilometersGas = value; }
        }
        public DateTime DATETREATMET
        {
            get { return d_dateTreatment; }
            set
            {
                if ((value.Day > 0 && value.Day < 32) && (value.Month > 0 && value.Month < 13) && (value.Year > 1894))
                    d_dateTreatment = value;
                else
                    throw new InvalidOperationException("Wrong date Treatment");
            }
        }
        public DateTime DATEACTIVITY
        {
            get { return d_dateActivity; }
            set
            {
                if ((value.Day > 0 && value.Day < 32) && (value.Month > 0 && value.Month < 13) && (value.Year >= 1895 && value.Year < 2021))
                    d_dateActivity = value;
                else
                    throw new InvalidOperationException("Wrong date Activity");
            }
        }
        public float TOTALKILOMETERS
        {
            get { return t_totalkilometers; ; }
            ///private set,doesn't allowed to change the total kilometers
            private set { t_totalkilometers = value; }

        }
        public float kILOMETERS_TREATMENT
        {
            get { return k_kilometersTreatment; }
            set { k_kilometersTreatment = value; }
        }

        public string LICENSEPLATE
        {
            get { return l_licensePlate; }
            set
            {
                if (this.NumberOflicensePlate() == value.Length - 2 || this.NumberOflicensePlate() == value.Length)
                    l_licensePlate = value;
                else
                    throw new InvalidOperationException("Invalid license number input");
            }
        }
        /// <summary>
        /// A function that receives a parameter and adds to the total kilometers
        /// </summary>
        /// <param name="_kilometers"></param>
        public void Mileage(float _kilometers)
        {
            this.TOTALKILOMETERS += _kilometers;
        }
        /// <summary>
        /// A function that checks if the vehicle needs to be refueled
        /// </summary>
        /// <returns></returns>
        public bool FuelCondition()
        {
            if (this.k_kilometersGas > 1200)
                return true;
            return false;
        }
        /// <summary>
        /// A function that checks how many numbers the user needs to type for the number plate 
        /// </summary>
        /// <returns></returns>
        public int NumberOflicensePlate()
        {
            int year;
            int.TryParse(this.d_dateActivity.Year.ToString(), out year);
            return year < 2018 ? 7 : 8;
        }
        /// <summary>
        /// Function that checks if the bus needs treatment - 
        /// treatment bus defined if the bus has traveled more than 20,000 kilometers or a year has passed since the last treatment
        /// </summary>
        /// <returns></returns>
        public bool TreatmentIsNeeded()
        {
            if (this.k_kilometersTreatment >= 20000 || dateCheck())
                return true;
            return false;
        }/// <summary>
         /// A function that checks if a year has passed since the last treatment
         /// </summary>
         /// <returns></returns>
        bool dateCheck()
        {
            int day;
            int month;
            int year;
            int.TryParse(this.d_dateTreatment.Day.ToString(), out day);
            int.TryParse(this.d_dateTreatment.Month.ToString(), out month);
            int.TryParse(this.d_dateTreatment.Year.ToString(), out year);
            DateTime currentDate = DateTime.Now;
            if (int.Parse(currentDate.Day.ToString()) == day && int.Parse(currentDate.Month.ToString()) == month && int.Parse(currentDate.Year.ToString()) < year || int.Parse(currentDate.Year.ToString()) < year)
                return true;
            return false;
        }
        /// <summary>
        /// Override of function ToString
        /// Returns bus details
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("License number: {0} , Date Activity: {1} ,Date Areatmet: {2} ,Kilometers since last treatment: {3} ,Kilometers since last refueling: {4} ,Total Kilometers: {5}",
               LICENSEPLATE, DATEACTIVITY.ToString("dd/MM/yyyy"), DATETREATMET.ToString("dd/MM/yyyy"), kILOMETERS_TREATMENT, KILOMETERSGAS, TOTALKILOMETERS);
        }
        /// <summary>
        /// A function for performing travel that updates the kilometers
        /// </summary>
        /// <param name="kilometers"></param>
        public void BusTravel(float kilometers)
        {
            KILOMETERSGAS += kilometers;
            kILOMETERS_TREATMENT += kilometers;
            Mileage(kilometers);

        }
        /// <summary>
        /// Refueling function
        /// </summary>
        public void refueling()
        {
            KILOMETERSGAS = 0;
        }
        /// <summary>
        /// Treatment function
        /// The function calls a Refueling function because every bus that goes to treatment also comes out refueled
        /// </summary>
        public void Treatment()
        { 
            kILOMETERS_TREATMENT = 0;
            refueling();
            DATETREATMET = DateTime.Now;
        }
        /// <summary>
        /// function DoWork
        /// Performs the procession
        //  The function is sent to Worker_ProgressChanged function to update the process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            if (Progress== 0)
            {
                stopwatch.Restart();
                Workerlength = (int)e.Argument;
                TimeLeft = WorkerLength.ToString() + " Seconds left";
                for (int i = 1; i <= Workerlength; i++)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        e.Result = stopwatch.ElapsedMilliseconds; 
                        break;
                    }
                    else
                    {
                        // Perform a time consuming operation and report progress.
                        System.Threading.Thread.Sleep(1000);
                        worker.ReportProgress(i * 100 / Workerlength);
                    }
                }
            }
        }
        /// <summary>
        /// ProgressChanged function
        /// Updates the remaining time and fills the   Progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
            int timerText=(int)stopwatch.Elapsed.TotalSeconds;
            TimeLeft = ((WorkerLength - timerText).ToString() + " Seconds left");
        }
        /// <summary>
        /// RunWorkerCompleted function
        /// Updating the status of the bus and color
        /// Issues an appropriate notice to the user regarding the bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                // e.Result throw System.InvalidOperationException
                TimeLeft = "Canceled!";
                System.Windows.MessageBox.Show($"Canceled!", "bus process was Stop", MessageBoxButton.OK);
            }
            else if (e.Error != null)
            {
                TimeLeft = "Error: " + e.Error.Message; 
                System.Windows.MessageBox.Show($"Error", "bus process was Stop", MessageBoxButton.OK);

            }
            else
            {
                if (STATUS == Situation.refueling)
                {
                    System.Windows.MessageBox.Show($"Bus {LICENSEPLATE}", "finished refueling", MessageBoxButton.OK);
                    this.refueling();
                }
                if (STATUS == Situation.In_the_middle_of_A_ride)
                {
                    System.Windows.MessageBox.Show($" Bus {LICENSEPLATE}", "finished travel ", MessageBoxButton.OK);
                }
                if (STATUS == Situation.In_treatment)
                {
                    System.Windows.MessageBox.Show($" Bus {LICENSEPLATE}", "finished the treatment ", MessageBoxButton.OK);
                    this.Treatment();
                }
            }
            if (!FuelCondition() && !TreatmentIsNeeded())
            {
                STATUS = (Situation)(0);
                Color = "#FFB3F6BE";
            }
            else
            {
                STATUS = (Situation)4;
                Color= "#FFF49494";
            }
            Progress = 0;
            TimeLeft = "";
        }

        
    }
}
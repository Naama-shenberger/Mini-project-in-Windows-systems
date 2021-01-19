using BLAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for SimulateOneStationWindow.xaml
    /// </summary>
    public partial class SimulateOneStationWindow :Window
    {
        IBL BL;
        bool isTimerRun;
        string timerText;
        BO.BusStation CurBusStation;
        Stopwatch Stopwatch;
        BackgroundWorker timeWorker;
        TimeSpan tsStartTime= new TimeSpan(8, 0, 0);
        /// <summary>
        /// constructor -
        /// Receiving instance of ibl interface and the station pressed window travels
        /// </summary>
        /// <param name="_BL"></param>
        /// <param name="busStation"></param>
        public SimulateOneStationWindow(IBL _BL, BO.BusStation busStation)
        {
            InitializeComponent();
            BL = _BL;
            CurBusStation = busStation;
            StaionNametb.Text = busStation.StationName;
            CodeStaiontb.Text = busStation.BusStationKey.ToString();
            Stopwatch = new Stopwatch();
            timeWorker = new BackgroundWorker();
            //Event registration
            timeWorker.DoWork += Worker_DoWork;
            timeWorker.ProgressChanged += Worker_ProgressChanged;
            timeWorker.WorkerReportsProgress = true;
            Stopwatch.Restart();
            isTimerRun = true;
            timeWorker.RunWorkerAsync();
        }
        /// <summary>
        /// event ProgressChangedEventArgs 
        /// Updating the time, and pulling out all buses that arrived to the station from bl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan tsCurentTime = tsStartTime + Stopwatch.Elapsed;
            timerText = tsCurentTime.ToString().Substring(0, 8);
            timerTextBlock.Text = timerText;
            if (CurBusStation != null && CurBusStation.ListBusLinesInStation != null)
                lineTimingDataGrid.DataContext = BL.GetLineTimingPerStation(CurBusStation, tsCurentTime);
        }
        /// <summary>
        /// event DoWorkEventArgs 
        /// Thread sleep
        /// ReportProgress(1000)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timeWorker.ReportProgress(231);
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// event click
        /// back btn 
        /// is timer run false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backbtn_Click(object sender, RoutedEventArgs e)
        {

            isTimerRun = false;
            this.Close();
        }
    }
}

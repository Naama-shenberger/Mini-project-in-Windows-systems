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
        Stopwatch stopwatch;
        bool isTimerRun = false;
        Thread timerThread;
        string timerText;
        string simulaterRate;
        BO.BusStation CurBusStation;
        Stopwatch Stopwatch;

        BackgroundWorker timeWorker;
        TimeSpan tsStartTime;
       
        public string TimerText
        {
            get { return timerTextBlock.Text; }
        }
        public bool IsTimerRun
        {
            get { return isTimerRun; }
        }
        public SimulateOneStationWindow(IBL _BL, BO.BusStation busStation)
        {
            InitializeComponent();
           
                BL = _BL;
                CurBusStation = busStation;
                StaionNametb.Text = busStation.StationName;
                CodeStaiontb.Text = busStation.BusStationKey.ToString();
                stopwatch = new Stopwatch();
                Stopwatch = new Stopwatch();
                timeWorker = new BackgroundWorker();
                timeWorker.DoWork += Worker_DoWork;
                timeWorker.ProgressChanged += Worker_ProgressChanged;
                timeWorker.WorkerReportsProgress = true;
                Stopwatch.Restart();
                isTimerRun = true;
                timeWorker.RunWorkerAsync();
            
         

        }
        TimeSpan s = new TimeSpan(8, 0, 0);

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan tsCurentTime = tsStartTime + Stopwatch.Elapsed+s;
            timerText = tsCurentTime.ToString().Substring(0, 8);
            timerTextBlock.Text = timerText;
            if (CurBusStation != null && CurBusStation.ListBusLinesInStation != null)
                lineTimingDataGrid.DataContext = BL.GetLineTimingPerStation(CurBusStation, tsCurentTime);
        }
        void setTextInvok(string text)
        {
            this.timerTextBlock.Text = text;
            //  Thread.Sleep(1000);
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timeWorker.ReportProgress(1000);
                Thread.Sleep(1000);
            }
        }

       

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {

            isTimerRun = false;

            this.Close();
        }
    }
}

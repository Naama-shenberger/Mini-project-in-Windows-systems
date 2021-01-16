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

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for SimulateOneStationWindow.xaml
    /// </summary>
    public partial class SimulateOneStationWindow : Window
    {
        IBL BL;
        BO.BusStation CurBusStation;
        Stopwatch Stopwatch;
        BackgroundWorker timeWorker;
        TimeSpan tsStartTime;
        bool isTimerRun;
        public SimulateOneStationWindow(IBL _BL, BO.BusStation BusStation)
        {
            InitializeComponent();
            BL = _BL;
            CurBusStation = BusStation;
            gridOneStation.DataContext = CurBusStation;
            Stopwatch = new Stopwatch();
            timeWorker = new BackgroundWorker();
            timeWorker.DoWork += Worker_DoWork;
            timeWorker.ProgressChanged += Worker_ProgressChanged;

            timeWorker.WorkerReportsProgress = true;
            tsStartTime = DateTime.Now.TimeOfDay;
            Stopwatch.Restart();
            isTimerRun= true;
            timeWorker.RunWorkerAsync();

        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan tsCurentTime = tsStartTime + Stopwatch.Elapsed;
            string timerText = tsCurentTime.ToString().Substring(0, 8);
            this.timerTextBlock.Text = timerText;
            //  lineTimingDataGrid.DataContext=BL.GetLineTimingPerStation(CurBusStation,tsCurentTime);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(isTimerRun)
            {
                timeWorker.ReportProgress(231);
                Thread.Sleep(1000);
            }
        }

      
    }
}

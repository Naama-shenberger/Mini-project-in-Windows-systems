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
    /// Interaction logic for TravelerWindow.xaml
    /// </summary>
    public partial class TravelerWindow : Window
    {
        IBL bL;
        BO.BusStation CurBusStation;
        Stopwatch Stopwatch;
        BackgroundWorker timeWorker;
        TimeSpan tsStartTime;
        bool isTimerRun;
        string timerText;
        public TravelerWindow(IBL _bL)
        {
            InitializeComponent();
            bL = _bL;
            AllBusStaionsDataGrid.DataContext = bL.GetAllBusStation();
            AllBusStaionsDataGrid.IsReadOnly = true;
            AlllinesDataGrid.IsReadOnly = true;
            Stopwatch = new Stopwatch();
            timeWorker = new BackgroundWorker();
            timeWorker.DoWork += Worker_DoWork;
            timeWorker.ProgressChanged += Worker_ProgressChanged;

            //timeWorker.WorkerReportsProgress = true;
            //tsStartTime = DateTime.Now.TimeOfDay;
            //Stopwatch.Restart();
            //isTimerRun = true;
            //timeWorker.RunWorkerAsync();
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan tsCurentTime = tsStartTime + Stopwatch.Elapsed;
            timerText = tsCurentTime.ToString().Substring(0, 8);
            if (CurBusStation != null && CurBusStation.ListBusLinesInStation != null)
                lineTimingDataGrid.DataContext = bL.GetLineTimingPerStation(CurBusStation, tsCurentTime);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timeWorker.ReportProgress(231);
                Thread.Sleep(1000);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SimulateOneStationWindow simulateOneStationWindow = new SimulateOneStationWindow(bL);
            simulateOneStationWindow.ShowDialog();
            timerText =simulateOneStationWindow.TimerText;
            if(simulateOneStationWindow.IsTimerRun==true)
            {
                timeWorker.WorkerReportsProgress = true;
                tsStartTime = DateTime.Now.TimeOfDay;
                Stopwatch.Restart();
                isTimerRun = true;
                timeWorker.RunWorkerAsync();
            }

        }
        private void AllBusStaionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurBusStation = AllBusStaionsDataGrid.SelectedItem as BO.BusStation;

            AlllinesDataGrid.DataContext = (CurBusStation).ListBusLinesInStation.ToList();
        }
    }
}

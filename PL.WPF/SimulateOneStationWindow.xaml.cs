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
       

        public string TimerText
        {
            get { return timerTextBlock.Text; }
        }
        public bool IsTimerRun
        {
            get { return isTimerRun; }
        }
        public SimulateOneStationWindow(IBL _BL)
        {
            InitializeComponent();
            BL = _BL;
            simulater.Text = "60";
            stopwatch = new Stopwatch();

        }
        void runTimer()
        {
           
            while (isTimerRun)
            {
                
                timerText = stopwatch.Elapsed.ToString();
                // 00:00:00.000000 ==> 00:00:00
                timerText = timerText.Substring(0, 8);
                // this.timerTextBlock.Text = timerText;

                Action<string> action = setTextInvok;
                timerTextBlock.Dispatcher.BeginInvoke(action, timerText);
                Thread.Sleep(1000);
            }
        }

        void setTextInvok(string text)
        {
            this.timerTextBlock.Text = text;
         
        }

        private void startTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if(startTimerButton.Content.ToString()=="Start")
            {
                startTimerButton.Content = "Stop";
                if (!isTimerRun)
                {
                    stopwatch.Start();
                    isTimerRun = true;
                    timerThread = new Thread(runTimer);
                    timerThread.Start();
                }

            }
            else
            {
                startTimerButton.Content = "Start";
                if (isTimerRun)
                {
                    stopwatch.Stop();
                    isTimerRun = false;
                }
            }
           
        }


    }
}

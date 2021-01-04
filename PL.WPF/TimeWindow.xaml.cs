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
using BLAPI;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for TimeWindow.xaml
    /// </summary>
    public partial class TimeWindow : Window
    {
        BO.BusStation curstation;
        public BO.BusStation timeStation;
        public TimeSpan timeBeforeUpdate;
        public TimeWindow( BO.BusStation s1, BO.BusStation s2,TimeSpan t)
        {
            curstation = s1;
            timeStation = s2;
            timeBeforeUpdate = t;
            InitializeComponent();

            for (int i = 0; i <= 60; i++)
            {
                cbTime.Items.Add(i);
            }

            DataContext = timeBeforeUpdate;
        }

        private void btUpdateTimeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
}

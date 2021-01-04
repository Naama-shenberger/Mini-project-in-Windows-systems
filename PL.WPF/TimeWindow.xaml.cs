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
        BO.BusStation curBusStation1;
        BO.BusStation curBusStation2;
        public TimeSpan timeBeforeUpdate;
        public TimeWindow(IBL bl,BO.BusStation s1,BO.BusStation s2, TimeSpan t)
        {
            IBL bL = bl;
            InitializeComponent();
            curBusStation1 = s1;
            curBusStation2 = s2;

            timeBeforeUpdate; = t;

            for (int i = 0; i <= 100; i++)
            {
                cbGrade.Items.Add(i);
            }

            DataContext = curScBO;
        }

        private void btUpdateTimeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TimeAndDistanceWindow.xaml
    /// </summary>
    public partial class TimeAndDistanceWindow : Window
    {
        IBL bl;
        TimeSpan t;
        float d;
        public TimeSpan Time
        {
            get { return t; }
        }
        public float Distance
        {
            get { return d; }
        }

        public TimeAndDistanceWindow(IBL _bl)
        {
            InitializeComponent();
            
            bl = _bl;
            hourTextBox.Text=  "00";
            MinuteTextBox.Text = "00";
            SeconTextBox.Text = "00";
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            var hours = int.Parse(hourTextBox.Text.ToString());
            var minutes = int.Parse(MinuteTextBox.Text.ToString());
            var seconds = int.Parse(SeconTextBox.Text.ToString());
            t = new TimeSpan(hours, minutes, seconds);
            d = float.Parse(distance.Text);

            this.Close();


        }
    }
}

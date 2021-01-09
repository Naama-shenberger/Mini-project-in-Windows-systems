using BLAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for TimeAndDistanceWindow.xaml
    /// </summary>
    public partial class TimeAndDistanceWindow : Window
    {
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
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            t = TimeSpan.Parse( Regex.Replace(Time_pick.Text, "[A-Za-z ]", ""));
            d = float.Parse(distance.Text);
            this.Close();
        }
    }
}

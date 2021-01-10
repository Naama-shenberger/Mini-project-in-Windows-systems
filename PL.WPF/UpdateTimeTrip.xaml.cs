using System;
using System.Collections.Generic;
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
    /// Interaction logic for UpdateTimeTrip.xaml
    /// </summary>
    public partial class UpdateTimeTrip : Window
    {
        TimeSpan timedrive;
        public UpdateTimeTrip()
        {
            InitializeComponent();
        }
        public TimeSpan Timedrive
        {
            get { return timedrive; }
        }
        ///// <summary>
        /////event Done Click 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Done_Click(object sender, RoutedEventArgs e)
        //{
        //    timedrive = TimeSpan.Parse(Regex.Replace(Timepicker.Text, "[A-Za-z ]", ""));
        //    this.Close();
        //}
    }
}

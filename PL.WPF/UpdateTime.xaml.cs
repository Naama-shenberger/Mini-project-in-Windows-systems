using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for UpdateTime.xaml
    /// </summary>
    public partial class UpdateTime : Window
    {
        TimeSpan timedrive;
        public UpdateTime()
        {
            InitializeComponent();
        }
        public TimeSpan Timedrive
        {
            get { return timedrive; }
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            timedrive = TimeSpan.Parse(Regex.Replace(Timepicker.Text, "[A-Za-z ]", ""));
            this.Close();
        }
    }
   
   
}

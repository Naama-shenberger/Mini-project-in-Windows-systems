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
using dotNet5781_01_3747_8971;
using static dotNet5781_01_3747_8971.Bus;

namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for ThirdWindow.xaml
    /// </summary>
    public partial class ThirdWindow : Window
    {
        public ThirdWindow()
        {
            InitializeComponent();
        }
        public object listboxTextBlockNameValue
        {
            get
            {
                return currentDisplayBusLine;
            }
            set
            {
                if (value is Bus)
                   currentDisplayBusLine =(Bus)value;
            }
        }
        private Bus currentDisplayBusLine;
        private void TakeATrip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                currentDisplayBusLine = (Bus)listboxTextBlockNameValue;
                currentDisplayBusLine.BusTravel((float)Convert.ToDouble(tbDistanceDrive.Text));
                float Average_speed = Bus.r.Next(20, 51);
                currentDisplayBusLine.TimeTravel = (float)Convert.ToDouble(tbDistanceDrive.Text) / Average_speed;
                this.Close();
            }
            catch (NullReferenceException Null)
            {
                MessageBox.Show($"{Null.Message}", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            catch(InvalidOperationException exception)
            {
                MessageBox.Show($"{exception.Message}", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            
        }
    }

}

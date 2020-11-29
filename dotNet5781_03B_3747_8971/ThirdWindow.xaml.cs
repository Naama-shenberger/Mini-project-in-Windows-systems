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
        private Bus currentDisplayBusLine = new Bus();
        private void TakeATrip_Click(object sender, RoutedEventArgs e)
        {
           MainWindow Main = Application.Current.MainWindow as MainWindow;
            try
            {
                //currentDisplayBusLine =
                if (Main != null)
                {
                    if (Main.TravelTest(currentDisplayBusLine))
                        currentDisplayBusLine.BusTravel((float)Convert.ToDouble(tbDistanceDrive.Text));
                }
                else new NullReferenceException("The object is Null");
            }
            catch(NullReferenceException Null)
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

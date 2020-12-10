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

namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for showInfoBusDriver.xaml
    /// </summary>
    public partial class showInfoBusDriver : Window
    {
        BusDriver current;
        /// <summary>
        /// A constructor who receives an object BusDriver
        /// Placement operation of info Bus Driver
        /// </summary>
        /// <param name="driver"></param>
        public showInfoBusDriver(object driver)
        {
            InitializeComponent();
            if(driver is BusDriver)
            {
                current = (BusDriver)driver;
                tlTime.Text = current.TotalTravelTime.ToString();
                lbGender.Text = current.Gender;
                lbLastName.Text = current.LastName;
                lbName.Text = current.Name;
            }
        }
        /// <summary>
        ///  A click event that closes the class window, the button actually displays the previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

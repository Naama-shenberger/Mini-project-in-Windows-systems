using BLAPI;
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

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for access.xaml
    /// access window,There are 2 options for user or passenger access
    /// </summary>
    public partial class access : Window
    {
        IBL BL;
        TravelerWindow travelerWindow;
        public access(IBL _BL)
        {
            InitializeComponent();
            BL = _BL;

        }
        /// <summary>
        /// event click
        /// management btn, Open a management window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void management_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(BL);
            user.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// event click
        ///traveler btn, Open a TravelerWindow  window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void traveler_Click(object sender, RoutedEventArgs e)
        {
            travelerWindow = new TravelerWindow(BL);
            travelerWindow.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// event click 
        /// Shut down the entire system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                if (App.Current.Windows[intCounter].Activate() == true)
                    App.Current.Windows[intCounter].Close();

        }
    }
}

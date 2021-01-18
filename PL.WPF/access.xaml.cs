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
    /// </summary>
    public partial class access : Window
    {
        IBL BL;
        public access(IBL _BL)
        {
            InitializeComponent();
            BL = _BL;

        }

        private void management_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(BL);
            user.ShowDialog();
            this.Close();
        }

        private void traveler_Click(object sender, RoutedEventArgs e)
        {
            TravelerWindow travelerWindow = new TravelerWindow(BL);
            travelerWindow.ShowDialog();
            this.Close();
        }
    }
}

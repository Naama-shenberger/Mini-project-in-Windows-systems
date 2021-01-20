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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for UserControlMoreDetails.xaml
    /// </summary>
    public partial class UserControlMoreDetails : UserControl
    {
        IBL bl;
        public UserControlMoreDetails(IBL _bl)
        {
            try
            {
                InitializeComponent();
                bl = _bl;
                Treeview.ItemsSource = bl.GetAllBusLinesGroupByArea();
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
    }
}

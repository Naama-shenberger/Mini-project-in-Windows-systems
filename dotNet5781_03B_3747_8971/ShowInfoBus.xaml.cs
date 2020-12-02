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
    /// Interaction logic for ShowInfoBus.xaml
    /// </summary>
    public partial class ShowInfoBus : Window
    {
        private Bus currcurrent;
        public ShowInfoBus(object v)
        {
            var val = v as Bus;
            InitializeComponent();
            currcurrent = (Bus)v;
            tbDate_Activiy.Text = currcurrent.DATEACTIVITY.ToString("dd/MM/yyyy");
            tbDate_Treatmet.Text = currcurrent.DATETREATMET.ToString("dd/MM/yyyy");
            tbKilometers_refueling.Text = currcurrent.KILOMETERSGAS.ToString();
            tbKilometers_Treatmet.Text = currcurrent.kILOMETERS_TREATMENT.ToString();
            tbTotal_Kilometers.Text = currcurrent.TOTALKILOMETERS.ToString();
            //tbstatus.Text= currcurrent.STATUS.ToString();
            // tbShowBus.Text =(Bus). ;
            //this.Close();
        }
    }
}

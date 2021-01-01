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
    /// Interaction logic for BusInformationWindow.xaml
    /// </summary>
    public partial class BusInformationWindow : Window
    {
        BO.Bus Bus = new BO.Bus();
        public BusInformationWindow(object transference)
        {
            InitializeComponent();
            if (transference is BO.Bus)
                Bus = (BO.Bus)transference;
            tbLicensePlate.Text = Bus.LicensePlate;
        }
    }
}

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
using System.Collections.ObjectModel;
using dotNet5781_01_3747_8971;

namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();
        }
        public void AddBus_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                MainWindow Main = Application.Current.MainWindow as MainWindow;
                DateTime DateActivityText = DateTime.Parse(tb_DateActivityBus.Text);
                DateTime DateTreatmetText = DateTime.Parse(tb_DateTreatment.Text);
                Bus NewBus = new Bus(tb_license_number.Text.ToString(), DateActivityText, DateTreatmetText,(float)Convert.ToDouble(tb_kilometersTreatment.Text), (float)Convert.ToDouble(tb_kilometersGas.Text), (float)Convert.ToDouble(tb_Totalkilometers.Text));
                Main.AddBus(NewBus);
                this.Close();
            }
            catch (InvalidOperationException message)
            {
                MessageBox.Show($"{message.Message}","ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            catch(FormatException F)
            {
                MessageBox.Show($"{F.Message}","ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            
        }
    }
}

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
        private Bus currentDisplayBusLine;
        public object CurrentDisplayBusLine
        {
            get
            {
                return (Bus)currentDisplayBusLine;
            }
            set
            {
                if (value is Bus)
                    currentDisplayBusLine = (Bus)value;
            }
        }
        private void calendarActivity_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Calendar calendarActivity = sender as Calendar;
            if (calendarActivity.SelectedDate != null)
                tb_DateActivityBus.Text = ((DateTime)calendarActivity.SelectedDate).ToString("dd/MM/yyyy");
            else
                MessageBox.Show("Not Selected, please Selecte again", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
        }
        private void calendarTreatment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Calendar calendarTreatment = sender as Calendar;
            if(calendarTreatment.SelectedDate!=null)
              tb_DateTreatment.Text = ((DateTime)calendarTreatment.SelectedDate).ToString("dd/MM/yyyy");
            else
                MessageBox.Show("Not Selected, please Selecte again", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);

        }
        public void AddBus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              
                 DateTime DateActivityText = DateTime.Parse(tb_DateActivityBus.Text);
                DateTime DateTreatmetText = DateTime.Parse(tb_DateTreatment.Text);
                currentDisplayBusLine = new Bus(tb_license_number.Text.ToString(), DateActivityText, DateTreatmetText,(float)Convert.ToDouble(tb_kilometersTreatment.Text), (float)Convert.ToDouble(tb_kilometersGas.Text), (float)Convert.ToDouble(tb_Totalkilometers.Text));

                if (!currentDisplayBusLine.FuelCondition() && !currentDisplayBusLine.TreatmentIsNeeded())
                {
                    currentDisplayBusLine.STATUS = (Bus.Situation)(0);
                    currentDisplayBusLine.Color = "#FFA3F4B0";
                }
                else
                {
                    currentDisplayBusLine.STATUS = (Bus.Situation)(4);
                    currentDisplayBusLine.Color = "#FFBD5850";
                }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

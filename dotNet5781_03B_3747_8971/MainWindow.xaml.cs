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
using System.Collections.ObjectModel;
using dotNet5781_01_3747_8971;

namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
            InitilizeBusList();
            cbBuses.ItemsSource =BusList;
        }
        private ObservableCollection<Bus> BusList = new ObservableCollection<Bus>();
        private Bus currentDisplayBusLine;
        public void AddBus(object Newbus)
        {
            var adding = Newbus as Bus;
            if (adding != null)
            {
                BusList.Add(adding);
                return;
            }
            throw new NullReferenceException("The object Can not be added to the list because he is not Bus type");
        }
        public object GetItem(string BusLicenseNumber)
        {
            foreach (var Bus in BusList)
                    if (Bus.LICENSEPLATE== BusLicenseNumber)
                            return Bus;
            throw new InvalidOperationException("The License Number does not exist");
        }
        private void InitilizeBusList()
        {
            DateTime v = new DateTime(2019, 4, 3);
            for (int i = 0; i < 10; i++)
            {
                BusList.Add(new Bus(Bus.r.Next(10000000,100000000).ToString(),v,v,8,6,4));
            }
        }
        private void AddABus_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow secondWindow = new SecondWindow();
            secondWindow.Show();
            //secondWindow.ShowDialog();
        }
        private void Travel_Click(object sender, RoutedEventArgs e)
        {
            ThirdWindow thirdWindow = new ThirdWindow();
            thirdWindow.Show();
            
        }
        public bool TravelTest(object obj)
        {
            var BusItem = obj as Bus;
            if (BusItem != null)
            {
                if (GetItem(BusItem.LICENSEPLATE) is Bus)
                {
                    currentDisplayBusLine = (Bus)GetItem(BusItem.LICENSEPLATE);
                    if (currentDisplayBusLine.TreatmentIsNeeded())
                        throw new InvalidOperationException("The bus needs a Treatment");
                    if (currentDisplayBusLine.FuelCondition())
                        throw new InvalidOperationException("The bus needs a refueling");
                    //סטטוס
                    return true;
                }
                throw new NullReferenceException("The object Can not be added to the list because he is not Bus type");
            }
           throw new NullReferenceException("The object Can not be added to the list because he is not Bus type");
        }


    }
}

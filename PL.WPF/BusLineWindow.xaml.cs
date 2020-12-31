using BLAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BusLineWindow.xaml
    /// </summary>
    public partial class BusLineWindow : Window
    {
        IBL bl;
        private ObservableCollection<BO.BusLine> BusLineList = new ObservableCollection<BO.BusLine>();
        public BusLineWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            ComboBoxBusLineNumber.DisplayMemberPath = "BusLineNumber";
            ComboBoxBusLineNumber.SelectedIndex = 0;
            RefreshAllBusLinesComboBox();
        }
        void RefreshAllBusLinesComboBox()
        {

            ComboBoxBusLineNumber.DataContext = Convert<BO.BusLine>(bl.GetAllBusLines()); //ObserListOfStudents;
        }
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        /// <summary>
        /// 
        /// The event opens a window with the bus details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewDetailsClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            BO.BusLine Data = btn.DataContext as BO.BusLine;

        }
    }
}

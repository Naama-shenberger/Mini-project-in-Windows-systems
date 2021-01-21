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
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        int Index;
        IBL BL;
        BO.BusLineStation CurBusLineStation;
        BO.BusLine BusLine;//Bus Line Get form a BusLineWindow
        public Update(IBL _BL,BO.BusLineStation busLineStation,BO.BusLine _busLine,int _Index)
        {
            InitializeComponent();
            Index = _Index;
            BL = _BL;
            BusLine = _busLine;
            CurBusLineStation = busLineStation;
            for (int i = 1; i <= Index; i++)
            {
                cbNumberIndex.Items.Add(i);
            }
            DataContext = cbNumberIndex;
        }
        /// <summary>
        /// End of update
        /// send to bl UpdateBusLineStation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(cbNumberIndex.SelectedValue.ToString()) == 1)
                    BusLine.FirstStopNumber = CurBusLineStation.BusStationKey;
                if (int.Parse(cbNumberIndex.SelectedValue.ToString()) == Index)
                    BusLine.LastStopNumber = CurBusLineStation.BusStationKey;
                BL.UpdateBusLineStation(CurBusLineStation.BusStationKey, BusLine.ID, BusLine, int.Parse(cbNumberIndex.SelectedValue.ToString()));
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Unsuitable characters please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
          
        }
    }
}


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
        BO.BusLine BusLine;
        public Update(IBL _BL,BO.BusLineStation busLineStation,BO.BusLine _busLine,int _Index)
        {
            InitializeComponent();
            Index = _Index;
            BL = _BL;
            BusLine = _busLine;
            CurBusLineStation = busLineStation;
            for (int i = 1; i <= Index -1; i++)
            {
                cbNumberIndex.Items.Add(i);
            }
            DataContext = cbNumberIndex;
        }
        
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            //CurBusLineStation.NumberStationInLine = int.Parse(cbNumberIndex.SelectedValue.ToString());
            BL.UpdateBusLineStation(CurBusLineStation.BusStationKey,BusLine.ID, BusLine, int.Parse(cbNumberIndex.SelectedValue.ToString()));
           // BusLine.StationsInLine.AsParallel().ForAll(a => { if (a.BusStationKey == CurBusLineStation.BusStationKey) { a.NumberStationInLine= int.Parse(cbNumberIndex.SelectedValue.ToString()); } });
            this.Close();
        }
    }
}


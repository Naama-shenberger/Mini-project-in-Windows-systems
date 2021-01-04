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
    /// Interaction logic for TimeStations.xaml
    /// </summary>
    public partial class TimeStations : Window
    {
        IBL bL;
        BO.BusStation curBusStation1;
        BO.BusStation curBusStation2;
        TimeSpan timeBeforeUpdate;
        ObservableCollection<BO.BusLineStation> curBusLineStations = new ObservableCollection<BO.BusLineStation>();
        public TimeStations(IBL _bL, BO.BusStation busStation, BO.BusStation busStation2)
        {
            //InitializeComponent();
            //InitializeComponent();
            bL = _bL;
            //curBusStation1 = busStation1;
            //curBusStation2 = busStation2;
            //BusLineTextBlock.Text = CurBusLine.BusLineNumber.ToString();
            //Index = CurBusLine.StationsInLine.Count();
            //RefreshAllStationsComboBox();
        }
       
    }
}

   

﻿using System;
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
using BLAPI;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for BusWindow.xaml
    /// </summary>
    public partial class BusWindow : Window
    {
        IBL bl;
        private ObservableCollection<BO.Bus> BusList = new ObservableCollection<BO.Bus>();//Bus List 
        public BusWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            BusList=Convert<BO.Bus>(bl.GetAllBus());
            lvBuses.ItemsSource = BusList;
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
            BO.Bus Data = btn.DataContext as BO.Bus;
            BusInformationWindow busInformationWindow = new BusInformationWindow(Data);
            busInformationWindow.ShowDialog();
        }
        private void UpdateBus_Click(object sender, RoutedEventArgs e)
        {
            BO.Bus Data= lvBuses.SelectedItem as BO.Bus;
           
        }
        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow addBusWindow = new AddBusWindow(bl);
            addBusWindow.ShowDialog();


        }
        private void DeleteBus_Click(object sender, RoutedEventArgs e)
        {
            BO.Bus Data = lvBuses.SelectedItem as BO.Bus;
            bl.DeleteBus(Data);
            BusList.Remove(Data);
            lvBuses.Items.Refresh();
        }
    }
}
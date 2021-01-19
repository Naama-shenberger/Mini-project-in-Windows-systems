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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for BusLineWindow.xaml
    /// </summary>
    public partial class BusLineWindow : Window
    {
        IBL bl;
       
        public BusLineWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
        }
        /// <summary>
        /// event MouseButtonEventArgs 
        /// grid  DragMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender,MouseButtonEventArgs e)
        {
            DragMove();
        }
        /// <summary>
        /// Opens another window with each click on the list view menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewMenu_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            switch(index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlBusLinexaml(bl));
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlBusLines(bl));
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlMoreDetails(bl));
                    break;
                default:
                      break;

            }
        }
        /// <summary>
        /// move menu
        /// </summary>
        /// <param name="index"></param>
        private void MoveCursorMenu(int index)
        {
            TransitionEffectContenSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (100+(60 * index)), 0, 0);
        }
        /// <summary>
        /// event click
        /// back btn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}



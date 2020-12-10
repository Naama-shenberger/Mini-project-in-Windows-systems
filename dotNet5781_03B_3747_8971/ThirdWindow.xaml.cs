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
using dotNet5781_01_3747_8971;
using static dotNet5781_01_3747_8971.Bus;

namespace dotNet5781_03B_3747_8971
{
    /// <summary>
    /// Interaction logic for ThirdWindow.xaml
    /// </summary>
    public partial class ThirdWindow : Window
    {
        public ThirdWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// A parameter that Saves the distance that user typed
        /// Static so we can use it in bus class
        /// </summary>
        static string saveText;
        public static string SaveText
        {
            get { return saveText; }
        }
        /// <summary>
        /// Create an object from a class Bus,That we can access the methods and public fields of the class 
        /// </summary>
        private Bus currentDisplayBus;
        /// <summary>
        /// A property of the bus object, used to move objects between windows
        /// </summary>
        public object TransferObjectBus
        {
            get
            {
                return currentDisplayBus;
            }
            set
            {
                if (value is Bus)
                   currentDisplayBus =(Bus)value;
            }
        }
        /// <summary>
        /// Registration of an event control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender,KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //allow get out the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;
            //allow list of system keys
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.CapsLock || e.Key == Key.LeftShift
                || e.Key == Key.Home || e.Key == Key.End ||e.Key==Key.OemPeriod || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right||e.Key==Key.Decimal)
                return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //allow control system keys
            if (char.IsControl(c)) return;
            //allow digits (without shift or Alt)
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift)) || !(Keyboard.IsKeyDown(Key.RightShift)))
                    return;//let this key be written inside the textbox
            //forbid letters and signs(#,$,%,...)
            e.Handled = true;//ignore this key . make event as Handled will be routed to other controls
            return;
        }
        /// <summary>
        ///  KeyDown event
        /// The function gets the current object from a previous window by the property TransferObjectBus
        /// Receives text(driving distance) that the user typed and calculates the average time it will take to travel 
        /// and puts it in a field(TimeTravel) of bus class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender,KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    currentDisplayBus = (Bus)TransferObjectBus;
                     saveText = tbDistanceDrive.Text.ToString();
                    float Average_speed = Bus.r.Next(20, 51);
                    currentDisplayBus.TimeTravel = (float)Convert.ToDouble(tbDistanceDrive.Text) / Average_speed;
                    this.Close();
                }
                catch (NullReferenceException Null)
                {
                    MessageBox.Show($"{Null.Message}", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    this.Close();
                }
                catch (InvalidOperationException exception)
                {
                    MessageBox.Show($"{exception.Message}", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    this.Close();
                }
               
            }

    }
    /// <summary>
    /// A click event that closes the class window, the button actually displays the previous page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}

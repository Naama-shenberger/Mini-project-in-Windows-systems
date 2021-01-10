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
    /// Interaction logic for UpdateDistancexaml.xaml
    /// </summary>
    public partial class UpdateDistancexaml : Window
    {
        public UpdateDistancexaml()
        {
            InitializeComponent();
        }
        float distance;
        public float Distance
        {
            get { return distance; }
        }
        /// <summary>
        /// Registration of an event control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //allow get out the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;
            //allow list of system keys
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.CapsLock || e.Key == Key.LeftShift
                || e.Key == Key.Home || e.Key == Key.End || e.Key == Key.OemPeriod || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right || e.Key == Key.Decimal)
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
        /// OnKeyDownHandler for typing distance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    distance = float.Parse(UpdateTimetb.Text);
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
    }
}

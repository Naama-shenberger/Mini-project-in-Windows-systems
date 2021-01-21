using BLAPI;
using BO;
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
    /// Interaction logic for User.xaml
    /// User class, confirm access to passenger or driver
    /// </summary>
    public partial class User : Window
    {
        IBL iBL;//IBL 
        Random random = new Random();
        /// <summary>
        /// A constructor that receives an ibl interface object
        /// </summary>
        /// <param name="_iBL"></param>
        public User(IBL _iBL)
        {
            InitializeComponent();
            iBL = _iBL;
        }
        /// <summary>
        /// event click 
        /// Long in btn,Checking whether the user exists in another system will be notified to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LongIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (iBL.GetUser(UserName.Text).HashedPassword == Tools.hashPassword(iBL.GetUser(UserName.Text).Salt + PasswordUser.Password))
                {
                    if (iBL.GetUser(UserName.Text).AllowingAccess != true)
                    {
                        throw new ArgumentException();
                    }
                    MessageBox.Show("congratulation, hello user ");
                  
                    this.Close();
               
                }
                else
                {
                    MessageBox.Show("wrong password", "ERROR", MessageBoxButton.OKCancel);
                }

            }
            catch (ArgumentException)
            {
                MessageBox.Show("User doesn't have access", "ERROR", MessageBoxButton.OKCancel);
            }
            catch (BO.IdException)
            {
                MessageBox.Show("wrong User Name", "ERROR", MessageBoxButton.OKCancel);

            }
        }
        /// <summary>
        /// event click
        /// sing up btn,Open a registration window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingUp_Click(object sender, RoutedEventArgs e)
        {
            SingUp s = new SingUp(iBL);
            s.Show();
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
            access access = new access(iBL);
            access.ShowDialog();
        }
    }
}
    


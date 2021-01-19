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
    /// </summary>
    public partial class User : Window
    {
        IBL iBL;
        Random random = new Random();
        public User(IBL _iBL)
        {
            InitializeComponent();
            iBL = _iBL;
           


        }

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
        private void SingUp_Click(object sender, RoutedEventArgs e)
        {
            SingUp s = new SingUp(iBL);
            s.Show();
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //MainWindow a = new MainWindow();
            //a.Visibility = Visibility.Hidden;
        }
    }
}
    


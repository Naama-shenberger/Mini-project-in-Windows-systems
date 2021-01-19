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
    /// Interaction logic for SingUp.xaml
    /// </summary>
    public partial class SingUp : Window
    {
        IBL bL;
        Random random = new Random();
        public SingUp(IBL _BL)
        {
            InitializeComponent();
            bL = _BL;
        }
        private void Confrim_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int salt = random.Next();


                if (PasswordUser.Password.Any(char.IsDigit) && PasswordUser.Password.Any(char.IsLower) && PasswordUser.Password.Any(char.IsUpper))
                {
                    if (PasswordUser.Password.Length >= 6)
                    {
                        if (PasswordUser.Password == PasswordBoxConfirm.Password)
                        {
                            BO.User Newuser = new BO.User
                            {
                                UserName = UserName.Text,
                                AllowingAccess = true,
                                DelUser = false,
                                password = PasswordUser.Password,
                                Salt = salt,
                                HashedPassword = Tools.hashPassword(salt + PasswordUser.Password)
                            };
                            bL.AddUser(Newuser);
                            MessageBox.Show("Registration passed successfully", "Registration", MessageBoxButton.OK);
                            this.Close();
                        }
                        else
                            MessageBox.Show("wrong Confirm password", "ERROR", MessageBoxButton.OKCancel);
                    }
                    else
                        MessageBox.Show("Your password must Contains 6 At least characters,For your safety :)", "ERROR", MessageBoxButton.OKCancel);

                }
                else
                    MessageBox.Show("Your password must Contains lowercase and uppercase letters and numbers,For your safety :)", "ERROR", MessageBoxButton.OKCancel);

            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OKCancel);
            }
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
    


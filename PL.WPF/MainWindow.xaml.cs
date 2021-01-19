using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLAPI;
using MaterialDesignThemes.Wpf;

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IBL bl = BLFactory.GetBL();
        public MainWindow()
        {
            InitializeComponent();
            access access = new access(bl);
            access.ShowDialog();
           
            //if (access.IsActive == false)
            //    this.Visibility = Visibility.Hidden;


        }
        private void BusLine_Click(object sender, RoutedEventArgs e)
        {
            new BusLineWindow(bl).Show();

        }
        private void Bus_Click(object sender, RoutedEventArgs e)
        {
            new BusWindow(bl).Show();
        }

        private void Stations_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow(bl).Show();
        }

        private void Git_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Naama-shenberger/dotNet5781_3747_8971");
        }

        private void Facebookbtn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/campaign/landing.php?campaign_id=1641099308&extra_1=s%7Cc%7C314920565653%7Ce%7Cfacebook%7C&placement=&creative=314920565653&keyword=facebook&partner_id=googlesem&extra_2=campaignid%3D1641099308%26adgroupid%3D62120105989%26matchtype%3De%26network%3Dg%26source%3Dnotmobile%26search_or_content%3Ds%26device%3Dc%26devicemodel%3D%26adposition%3D%26target%3D%26targetid%3Dkwd-541132862%26loc_physical_ms%3D1007977%26loc_interest_ms%3D%26feeditemid%3D%26param1%3D%26param2%3D&gclid=CjwKCAiAgJWABhArEiwAmNVTByB1_TSePwJkJAJR165WDZ2k1x0-RmoHWe1x7BtY5G7SP83MA17k1hoCev8QAvD_BwE");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://twitter.com/twitter");
        }

        private void Linkedin_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.linkedin.com/feed/");
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
          
            //access access = new access(bl);
            //access.ShowDialog();
            this.Close();

        }
    }
}

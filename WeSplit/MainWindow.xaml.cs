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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeSplit.User_Control;
using System.IO;
using WeSplit.SQLData;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void reloadBackground()
        {
            ImageBrush newBackGround = new ImageBrush();
            var baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            baseFolder += "Image\\Main\\New_Background.png";
            newBackGround.ImageSource = new BitmapImage(new Uri(baseFolder, UriKind.Absolute));
            mainwindow.Background = newBackGround;

        }
        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            reloadBackground();
            DataContext = new Main();
        }
        private void Button_TripHasGone(object sender, MouseButtonEventArgs e)
        {
            reloadBackground();
            DataContext = new HistoryTripUserControl();

        }

        private void Button_Member(object sender, MouseButtonEventArgs e)
        {
            reloadBackground();
            DataContext = new MemberUserControl();

        }

        private void Button_Sites(object sender, MouseButtonEventArgs e)
        {
            reloadBackground();
            DataContext = new siteUserControl();
        }

        private void Button_Search(object sender, MouseButtonEventArgs e)
        {
            reloadBackground();
            DataContext = new searchUserControl();
        }

        private void Button_Infomation(object sender, MouseButtonEventArgs e)
        {
            ImageBrush newBackGround = new ImageBrush();
            var baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            baseFolder += "Image\\Main\\Backround_Trip_Console.png";
            newBackGround.ImageSource = new BitmapImage(new Uri(baseFolder, UriKind.Absolute));
            mainwindow.Background = newBackGround;
            DataContext = new Infomation_UserControl();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            reloadBackground();
            DataContext = new Main();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}

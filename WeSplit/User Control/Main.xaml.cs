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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeSplit.SQLData;

namespace WeSplit.User_Control
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            table_CHUYENDI.ItemsSource = (from cd in DataProvider.Ins.DB.CHUYENDI 
                                          where cd.TRANGTHAI == false
                                          select cd).ToList();
        }

        private void button_add_trip(object sender, MouseButtonEventArgs e)
        {
            DataContext = new AddTripUserControl();
            this.Content = new AddTripUserControl();
        }



        private void table_CHUYENDI_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var data = table_CHUYENDI.SelectedItem as CHUYENDI;
            if (data != null)
            {
                DataContext = new DetailsTripUserControl(data.MA_CHUYENDI);
                this.Content = new DetailsTripUserControl(data.MA_CHUYENDI);
            }
            
        }

    }
}
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
using WeSplit.SQLData;

namespace WeSplit.User_Control
{
    /// <summary>
    /// Interaction logic for siteUserControl.xaml
    /// </summary>
    public partial class siteUserControl : UserControl
    {
        List<DD_DULICH> diadiem = DataProvider.Ins.DB.DD_DULICH.ToList();
        public siteUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listPlace.ItemsSource = diadiem.ToList();
        }

        private void buttonAdd(object sender, RoutedEventArgs e)
        {
            DataContext = new AddPlaceUserControl();
            this.Content = new AddPlaceUserControl();
        }
    }
}

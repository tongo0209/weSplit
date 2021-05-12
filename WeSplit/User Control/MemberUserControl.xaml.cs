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
    /// Interaction logic for MemberUserControl.xaml
    /// </summary>
    public partial class MemberUserControl : UserControl
    {
        public MemberUserControl()
        {
            InitializeComponent();
        }

        private void listMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            listMember.ItemsSource = DataProvider.Ins.DB.THANHVIEN.ToList();
        }

        private void addMemberButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AddMemberUserControl();
            this.Content = new AddMemberUserControl();
        }
    }
}

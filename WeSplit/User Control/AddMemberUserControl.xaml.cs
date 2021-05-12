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
using System.Data.SqlClient;

namespace WeSplit.User_Control
{
    /// <summary>
    /// Interaction logic for AddMemberUserControl.xaml
    /// </summary>
    public partial class AddMemberUserControl : UserControl
    {
        private string maCD;

        public AddMemberUserControl()
        {
            InitializeComponent();
        }

        public AddMemberUserControl(string maCD)
        {
            InitializeComponent();
            this.maCD = maCD;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var cd = DataProvider.Ins.DB.CHUYENDI.ToList();
            foreach (CHUYENDI item in cd)
            {
                if (item.TRANGTHAI == false)
                {
                    trip.Items.Add(item);
                }
            }
            if (maCD != null)
            {

                trip.SelectedItem = (from item in DataProvider.Ins.DB.CHUYENDI
                                  where item.MA_CHUYENDI == maCD
                                     select item).ToList()[0];
                trip.IsEnabled = false;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }
        }
        //Add thành viên
        private void addMember_Click(object sender, RoutedEventArgs e)
        {
            var name = nameMember.Text.Trim();
            var phone = phoneNumber.Text.Trim();
            //Thông báo khi để trống tên thành viên
            if (name.Length == 0)
            {
                MessageBox.Show("Không đễ trống tên thành viên", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Thông báo khi để trống số điện thoại
            else if (phone.Length == 0)
            {
                MessageBox.Show("Không để trống số điện thoại", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            //Thông báo khi trùng số điện thoại
            else if (DataProvider.Ins.DB.THANHVIEN.Any(mem => mem.SDT == phone))
            {
                MessageBox.Show("Số điện thoại đã trùng", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(phone.Length > 12)
            {
                MessageBox.Show("Số điện thoại không dài hơn 12");
            }    
            //Add khi nhập đầy đủ
            else
            {
                string MTV = $"TV{DataProvider.Ins.DB.THANHVIEN.Count() + 1}";

                var mem = new THANHVIEN()
                {
                    MATV = MTV,
                    TENTV = name,
                    SDT = phone
                };
                nameMember.Text = default;
                phoneNumber.Text = default;
                DataProvider.Ins.DB.THANHVIEN.Add(mem);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show($"Thêm thành công {name}", "Hoàn thành", MessageBoxButton.OK, MessageBoxImage.Information);
                member.ItemsSource = DataProvider.Ins.DB.THANHVIEN.ToList();
                reload();
            }
        }
        private void reload()
        {
            var idCD = trip.SelectedItem as CHUYENDI;
            tableMemberofTrip.ItemsSource = (from tg in DataProvider.Ins.DB.THAMGIA join tv in DataProvider.Ins.DB.THANHVIEN on tg.MATV equals tv.MATV select new { tg.TIENTHU, tv.TENTV, tg.MACD })
                            .Where(tg => tg.MACD == idCD.MA_CHUYENDI).ToList();
            var listinCD = (from tv in DataProvider.Ins.DB.THANHVIEN join tg in DataProvider.Ins.DB.THAMGIA on tv.MATV equals tg.MATV select new { tv.MATV, tv.TENTV, tg.MACD, tv.SDT })
                            .Where(tg => tg.MACD == idCD.MA_CHUYENDI);
            var listFilter = (from data in listinCD select new { data.MATV, data.TENTV, data.SDT });
            member.ItemsSource = (from tv in DataProvider.Ins.DB.THANHVIEN select new { tv.MATV, tv.TENTV, tv.SDT }).Except(listFilter).ToList();
        }
        private void trip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reload();

        }
        private void addMemberToTrip_Click(object sender, RoutedEventArgs e)
        {
            
            if(trip.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn chuyến đi", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(member.SelectedItem == null){
                MessageBox.Show("Chưa chọn thành viên", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);

            }else if (money.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập tiền thu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                var item_CD = trip.SelectedItem as CHUYENDI;
                var CD = item_CD.MA_CHUYENDI;
                var item_TV = member.SelectedItem.ToString();
                string[] arr = item_TV.Split(',');
                var TV = arr[0].Substring(9);

                var tg = new THAMGIA()
                {
                    MACD = CD,
                    MATV = TV,
                    TIENTHU = Convert.ToDouble(money.Text.Trim()),
                    THANHVIEN = (from tv in DataProvider.Ins.DB.THANHVIEN
                                where tv.MATV == TV
                                select tv).ToList()[0],
                    
                };
                DataProvider.Ins.DB.THAMGIA.Add(tg);
                DataProvider.Ins.DB.SaveChanges();
                money.Text = default;
                reload();
            }
          
        }
        private void back_button(object sender, MouseButtonEventArgs e)
        {
            DataContext = new DetailsTripUserControl(maCD);
            this.Content = new DetailsTripUserControl(maCD);
        }
    }
}

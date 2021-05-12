using MaterialDesignThemes.Wpf;
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
    public partial class AddTripUserControl : UserControl
    {
        string MaCD;
        int check = 0; 
        int _STT = 0;
        int idKC = DataProvider.Ins.DB.KHOANCHI.Count();
        public AddTripUserControl()
        {
            InitializeComponent();
            var CHUYENDI = DataProvider.Ins.DB.CHUYENDI.ToList();
            int num = CHUYENDI.Count + 1;
            MaCD = "CD" + num;
        }

        public AddTripUserControl(string maCD)
        {
            MaCD = maCD;
            InitializeComponent();
        }

        List<DD_DUNGCHAN> DUNGCHAN = DataProvider.Ins.DB.DD_DUNGCHAN.ToList();
        List<LOTRINH> listRoadMap = new List<LOTRINH>();
        //Tạo biến tạm chứa các thành phần Add
        private class Temp
        {
            private string noiDungChan;

            public string NoiDungChan { get => noiDungChan; set => noiDungChan = value; }

            private string moTa;
            public string MoTa { get => moTa; set => moTa = value; }

            private string chiPhi;
            public string ChiPhi { get => chiPhi; set => chiPhi = value; }

            private string diaDiem;
            public string DiaDiem { get => diaDiem; set => diaDiem = value; }

        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (MaCD != null)
            {
                check = 1;
                var item = (from cd in DataProvider.Ins.DB.CHUYENDI
                            where cd.MA_CHUYENDI == MaCD
                            select cd).ToList()[0];
                nameTrip.Text = item.TEN_CHUYENDI;
                nameTrip.IsEnabled = false;
                destination.SelectedItem = (from dd in DataProvider.Ins.DB.DD_DULICH
                                            where dd.MA_DIEMDEN == item.DIEMDEN
                                            select dd).ToList()[0];
                checkin.SelectedDate = item.NGAYDI;
                checkin.IsEnabled = false;
                checkout.SelectedDate = item.NGAYVE;
                checkout.IsEnabled = false;

                if (item.THUE_KS > 0)
                {
                    room.IsChecked = true;
                    room.IsEnabled = false;
                    rentOfRoom.Text = Convert.ToString(item.THUE_KS);
                }
                if (item.THUE_XE > 0)
                {
                    car.IsChecked = true;
                    car.IsEnabled = false;
                    rentOfCar.Text = Convert.ToString(item.THUE_XE);
                }
                if (item.MAYBAY > 0)
                {
                    plane.IsChecked = true;
                    plane.IsEnabled = false;
                    rentOfPlane.Text = Convert.ToString(item.MAYBAY);
                }
                province.ItemsSource = DataProvider.Ins.DB.DD_DUNGCHAN.ToList();
                btnConfirm.Visibility = Visibility.Hidden;
            }
            else
            {
                destination.ItemsSource = DataProvider.Ins.DB.DD_DULICH.ToList();
                province.ItemsSource = DataProvider.Ins.DB.DD_DUNGCHAN.ToList();
                checkout.BlackoutDates.AddDatesInPast();
                checkin.BlackoutDates.AddDatesInPast();
                backBtn.Visibility = Visibility.Hidden;
            }
            
        }
        //Click Add
        private void addRoadMap_Click(object sender, RoutedEventArgs e)
        {
            var wp = wayPoint.Text.Trim();
            var des = description.Text.Trim();
            if (price.IsEnabled == false)
            {
                var prc = "0";
            }
            //Cảnh báo khi chưa nhập chi phí
            if (price.IsEnabled == true)
            {

                if (price.Text.Trim().Length == 0)
                {
                    MessageBox.Show($"Chưa nhập chi phí cho {des}","Cảnh báo",MessageBoxButton.OK,MessageBoxImage.Warning);
                }

            }
            //Cảnh báo khi chưa nhập địa điểm  dừng chân
            if (wp.Length == 0)
            {
                MessageBox.Show("Chưa nhập nơi dừng chân", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Cảnh báo khi chưa nhập mô tả chuyến đi
            else if (des.Length == 0)
            {
                MessageBox.Show("Chưa nhập mô tả", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Cảnh báo khi chưa chọn tỉnh thành
            else if (province.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn tỉnh thành", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Add dữ liệu
            else
            {
                _STT += 1;
                var dddc = DataProvider.Ins.DB.DD_DUNGCHAN.ToList();
                var lt = new LOTRINH
                {
                    STT = _STT,
                    MA_CHUYENDI = MaCD,
                    MA_DD_DUNGCHAN = dddc[province.SelectedIndex].MA_DD_DUNGCHAN,
                    DD_DUNGCHAN = wp,
                    MOTA = des
                };
                DataProvider.Ins.DB.LOTRINH.Add(lt);
                if (price.Text.Trim().Length != 0)
                {
                    var prc = Convert.ToDouble(price.Text.Trim());
                    idKC++;
                    var kc = new KHOANCHI
                    {
                        MA_KCHI = "KC" + idKC,
                        MA_CHUYENDI = MaCD,
                        STT_DC = _STT,
                        MA_DD_DUNGCHAN = dddc[province.SelectedIndex].MA_DD_DUNGCHAN,
                        SOTIENCHI = prc,
                        HANGMUC = des,
                        GHI_CHU = null

                    };
                    DataProvider.Ins.DB.KHOANCHI.Add(kc);
                    if(check == 1)
                    {
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }
                var item = new Temp()
                {
                    NoiDungChan = wayPoint.Text,
                    MoTa = description.Text,
                    ChiPhi = price.Text,
                    DiaDiem = dddc[province.SelectedIndex].TEN_DD_DUNGCHAN
                };
                roadMapsList.Items.Add(item);
            }
            wayPoint.Text = "";
            description.Text = "";
            province.SelectedItem = default;
            MaterialDesignFilledTextFieldTextBoxEnabledComboBox.IsChecked = false;
            price.Text = "";
        }
        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            var dd = DataProvider.Ins.DB.DD_DULICH.ToList();

            if (rentOfRoom.Text == "")
            {
                rentOfRoom.Text = "0";
            }
            if (rentOfPlane.Text == "")
            {
                rentOfPlane.Text = "0";
            }
            if (rentOfCar.Text == "")
            {
                rentOfCar.Text = "0";
            }
            //Cảnh báo khi chưa chọn điểm đến
            if (destination.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn điểm đến", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Cảnh báo khi để trống tên chuyến đi
            else if (nameTrip.Text.Trim().Length == 0)
            {
                MessageBox.Show("Tên chuyến đi không được bỏ trống", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Cảnh báo khi chưa chọn ngày
            else if (checkin.SelectedDate.ToString().Length == 0 || checkout.SelectedDate.ToString().Length == 0)
            {
                MessageBox.Show("Ngày đi ngày về không dược bỏ trống", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Cảnh báo chưa chọn lộ trình
            else if (roadMapsList.Items.Count < 1)
            {
                MessageBox.Show("Chuyến đi này chưa có lộ trình", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Kết thúc add
            else
            {
                
                CHUYENDI chuyendi = new CHUYENDI()
                {
                    MA_CHUYENDI = MaCD,
                    TEN_CHUYENDI = nameTrip.Text.Trim(),
                    DIEMDEN = dd[destination.SelectedIndex].MA_DIEMDEN,
                    NGAYDI = checkin.SelectedDate.Value,
                    NGAYVE = checkout.SelectedDate.Value,
                    THUE_KS = Int32.Parse(rentOfRoom.Text.Trim()),
                    THUE_XE = Int32.Parse(rentOfCar.Text.Trim()),
                    MAYBAY = Int32.Parse(rentOfPlane.Text.Trim()),
                    TRANGTHAI = false,
                };
                DataProvider.Ins.DB.CHUYENDI.Add(chuyendi);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show($"Đã thêm thành công chuyến đi {nameTrip.Text}","Hoàn thành",MessageBoxButton.OK,MessageBoxImage.Information);
                nameTrip.Text = "";
                destination.SelectedItem = default;
                checkin.SelectedDate = default;
                checkout.SelectedDate = default;
                car.IsChecked = false;
                room.IsChecked = false;
                plane.IsChecked = false;
                roadMapsList.Items.Clear();
                
            }

        }
        private void car_Checked(object sender, RoutedEventArgs e)
        {
            if (car.IsChecked == true)
            {
                rentOfCar.IsEnabled = true;
            }
            else
            {
                rentOfCar.IsEnabled = false;
            }
        }
        private void room_Checked(object sender, RoutedEventArgs e)
        {
            if (room.IsChecked == true)
            {
                rentOfRoom.IsEnabled = true;
            }
            else
            {
                rentOfRoom.IsEnabled = false;
            }
        }
        private void plane_Checked(object sender, RoutedEventArgs e)
        {
            if (plane.IsChecked == true)
            {
                rentOfPlane.IsEnabled = true;
            }
            else
            {
                rentOfPlane.IsEnabled = false;
            }

        }
        private void checkin_Changed(object sender, RoutedEventArgs e)
        {
            checkout.SelectedDate = default;
            checkout.BlackoutDates.Clear();
            if (checkin.SelectedDate != default)
            {
                var date = checkin.SelectedDate.Value;
                date.AddDays(-1);
                checkout.BlackoutDates.AddDatesInPast();
                checkout.BlackoutDates.Add(new CalendarDateRange(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(date.Year, date.Month, date.Day)));
            }
            
        }

        private void back_button(object sender, MouseButtonEventArgs e)
        {
            DataContext = new DetailsTripUserControl(MaCD);
            this.Content = new DetailsTripUserControl(MaCD);
        }
    }
}
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WeSplit.SQLData;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for DialogAddImage.xaml
    /// </summary>
    public partial class DialogAddImage : Window
    {


        public DialogAddImage(string maCD)
        {
            InitializeComponent();
            this.maCD = maCD;
        }

        private List<string> listPathImage = new List<string>();
        private string maCD;

        private void confirmButton(object sender, RoutedEventArgs e)
        {
            foreach(string path in listPathImage)
            {
                var maHA = $"HACD{DataProvider.Ins.DB.HINHANH_CHUYENDI.Count() + 1}";
                var info = new FileInfo(path);
                string folder = AppDomain.CurrentDomain.BaseDirectory; // "C:\Users\dev\"
                folder += "Image\\trip\\";
                string name = Guid.NewGuid().ToString();
                File.Copy(path, folder + name + info.Extension);
                HINHANH_CHUYENDI ha = new HINHANH_CHUYENDI()
                {
                    CHUYENDI = maCD,
                    MA_ANH_CD = maHA,
                    TENANH_CD = name+info.Extension,
                };
                DataProvider.Ins.DB.HINHANH_CHUYENDI.Add(ha);
                DataProvider.Ins.DB.SaveChanges();
                var test = DataProvider.Ins.DB.HINHANH_CHUYENDI.ToList();
            }
            DialogResult = true;
        }
        private void browserButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (ofd.ShowDialog() == true)
            {
                foreach (string item in ofd.FileNames)
                {
                    listImages.Items.Add(item);
                    listPathImage.Add(item);

                }
            }
        }

        private void imageDelete(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var selectItem = (from cd in DataProvider.Ins.DB.CHUYENDI
                              where cd.MA_CHUYENDI == maCD
                              select cd).ToList<CHUYENDI>()[0];
            DataContext = selectItem;
        }
    }
}

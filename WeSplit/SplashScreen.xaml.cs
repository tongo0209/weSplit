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
using System.Windows.Threading;
using WeSplit.SQLData;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        private string dataFile;
        public SplashScreen()
        {
            InitializeComponent();
            string folder = AppDomain.CurrentDomain.BaseDirectory; // "C:\Users\dev\"
            folder = folder.Remove(folder.IndexOf("bin"));
            dataFile = $"{folder}SQLData\\IsChecked.txt";
            var isChecked = File.ReadAllText(dataFile);
            if (isChecked == "true")
            {
                MainWindow hr = new MainWindow();
                hr.Show();
                this.Close();
            }
            else
            {
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = new TimeSpan(15000);
                timer.Start();
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            rectangle2.Width += 3;
            if (rectangle2.Width >= 700)
            {
                timer.Stop();
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();

            }

        }

        private void Check(object sender, RoutedEventArgs e)
        {
            if (check.IsChecked == true)
            {
                string newData = "true";
                File.WriteAllText(dataFile, newData);
            }
            else
            {
                string newData = "fasle";
                File.WriteAllText(dataFile, newData);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dd = DataProvider.Ins.DB.DD_DULICH.ToList();
            Random rng = new Random();
            DataContext = dd[rng.Next(0,dd.Count())];
        }
    }
}

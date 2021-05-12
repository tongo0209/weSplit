using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WeSplit.SQLData
{
    class ConverterColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string temp = value.ToString();
            if(temp.StartsWith("-") == true)
            {
                return new SolidColorBrush(Color.FromRgb(255, 102, 101));
            }
            return new SolidColorBrush(Color.FromRgb(1, 51, 205));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeSplit.SQLData
{
    class RelativeToAbsolutePathForPlace : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var relative = (string)value;
            var baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            var absolute = $"{baseFolder}Image\\dd\\{relative}";
            return absolute;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

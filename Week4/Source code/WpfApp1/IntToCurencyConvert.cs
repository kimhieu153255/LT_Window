using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp1
{
    internal class IntToCurencyConvert : IValueConverter
    {
        public string Culture { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int amount = (int)value;
            var info = CultureInfo.GetCultureInfo(Culture);
            string currencyFormat = String.Format(info, "{0:c}", amount);
            return currencyFormat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

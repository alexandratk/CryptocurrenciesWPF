using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CryptocurrenciesWPF.Common
{
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value != null)
            {

                double valueDouble = double.Parse(value.ToString());
                string result = valueDouble.ToString(
                    parameter.ToString(), CultureInfo.CreateSpecificCulture("en"));

                if (parameter.ToString().Equals("P") && valueDouble > 0)
                {
                    result = "+" + result;
                }
                return result;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

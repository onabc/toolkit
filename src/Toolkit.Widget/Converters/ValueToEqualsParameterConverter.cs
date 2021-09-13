using System;
using System.Globalization;
using System.Windows.Data;

namespace Toolkit.Widget.Converters
{
    /// <summary>
    /// 判断传入的值，是否与指定的参数一致
    /// </summary>
    public class ValueToEqualsParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
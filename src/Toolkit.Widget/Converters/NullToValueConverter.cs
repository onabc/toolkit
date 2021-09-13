using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Toolkit.Widget.Converters
{
    /// <summary>
    /// NulllToValueConverter
    /// </summary>
    public class NullToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return parameter;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }
    }
}
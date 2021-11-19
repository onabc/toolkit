using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Toolkit.Client.Models;

namespace Toolkit.Client
{
    public class TileStateToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TileState state)
            {
                object o = Application.Current.TryFindResource(state.ToString());
                return o as Geometry;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

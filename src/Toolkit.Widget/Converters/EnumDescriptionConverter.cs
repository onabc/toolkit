using Toolkit.Library.Extensions;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Toolkit.Widget.Converters
{
    /// <summary>
    /// 获取枚举的描述文字
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Enum)value).GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EnumDescriptionTypeConverter : EnumConverter
    {
        public EnumDescriptionTypeConverter(Type type) : base(type)
        {
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (null != value)
                {
                    FieldInfo fi = value.GetType().GetField(value.ToString());

                    if (null != fi)
                    {
                        var attributes =
                            (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                        return ((attributes.Length > 0) && (!string.IsNullOrEmpty(attributes[0].Description)))
                            ? attributes[0].Description
                            : value.ToString();
                    }
                }

                return string.Empty;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
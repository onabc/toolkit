using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Toolkit.Widget.Controls
{
    public class MetroCheckBox : CheckBox
    {
        static MetroCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroCheckBox), new FrameworkPropertyMetadata(typeof(MetroCheckBox)));
        }

        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Geometry), typeof(MetroCheckBox));

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(MetroCheckBox), new PropertyMetadata(20.0));

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(MetroCheckBox), new PropertyMetadata(20.0));
    }
}
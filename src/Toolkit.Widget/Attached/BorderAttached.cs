using System.Windows;

namespace Toolkit.Widget.Attached
{
    public static class BorderAttached
    {
        private static CornerRadius Default_CornerRadius = new CornerRadius(0);

        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius"
                , typeof(CornerRadius)
                , typeof(BorderAttached)
                , new PropertyMetadata(Default_CornerRadius));
    }
}
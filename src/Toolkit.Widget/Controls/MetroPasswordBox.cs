using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Toolkit.Widget.Controls
{
    public class MetroPasswordBox : Control
    {
        static MetroPasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroPasswordBox), new FrameworkPropertyMetadata(typeof(MetroPasswordBox)));
        }

        /// <summary>
        /// 获取或者设置水印
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(MetroPasswordBox));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MetroPasswordBox));

        public Brush MouseMoveBackground
        {
            get { return (Brush)GetValue(MouseMoveBackgroundProperty); }
            set { SetValue(MouseMoveBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseMoveBackgroundProperty =
            DependencyProperty.Register("MouseMoveBackground", typeof(Brush), typeof(MetroPasswordBox));

        /// <summary>
        /// 获取或者设置当前密码值
        /// </summary>
        [Bindable(true), Description("获取或者设置当前密码值")]
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(MetroPasswordBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 获取或者设置PasswordBox的屏蔽字符
        /// </summary>
        [Bindable(true), Description("获取或者设置PasswordBox的屏蔽字符")]
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar"
                , typeof(char)
                , typeof(MetroPasswordBox)
                , new PropertyMetadata('●'));
    }
}
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Toolkit.Widget.Controls
{
    public class MetroTextBox : TextBox
    {
        static MetroTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTextBox), new FrameworkPropertyMetadata(typeof(MetroTextBox)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MetroTextBox));

        public Visibility ShowClear
        {
            get { return (Visibility)GetValue(ShowClearProperty); }
            set { SetValue(ShowClearProperty, value); }
        }

        public static readonly DependencyProperty ShowClearProperty =
            DependencyProperty.Register("ShowClear", typeof(Visibility), typeof(MetroTextBox), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// 获取或者设置水印
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(MetroTextBox));

        public Brush MouseMoveBackground
        {
            get { return (Brush)GetValue(MouseMoveBackgroundProperty); }
            set { SetValue(MouseMoveBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseMoveBackgroundProperty =
            DependencyProperty.Register("MouseMoveBackground", typeof(Brush), typeof(MetroTextBox));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ButtonBase clearBtn = this.Template.FindName("PART_ContentHostClearButton", this) as ButtonBase;

            clearBtn.Click += (s, e) =>
            {
                this.Text = "";
            };
        }
    }
}
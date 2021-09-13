using System.Windows.Controls;

namespace Toolkit.Widget.Controls
{
    public class FormSeparator : Separator
    {
        public FormSeparator()
        {
            DefaultStyleKey = typeof(FormSeparator);
            Form.SetIsItemItsOwnContainer(this, true);
        }
    }
}
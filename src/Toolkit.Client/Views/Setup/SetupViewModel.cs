using Caliburn.Micro;

namespace Toolkit.Client.Views
{
    public class SetupViewModel : Screen, IDisplayModule
    {
        public byte SortNumber { get; init; } = 1;

        public SetupViewModel()
        {
            DisplayName = "系统设置";
        }
    }
}
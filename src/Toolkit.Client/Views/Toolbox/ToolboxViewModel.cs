using Caliburn.Micro;
using Serilog;
using System.Threading.Tasks;

namespace Toolkit.Client.Views
{
    public class ToolboxViewModel : Screen, IDisplayModule
    {
        private readonly ILogger logger;

        public byte SortNumber { get; init; } = 0;

        public ToolboxViewModel(ILogger logger)
        {
            DisplayName = "工具箱";
            this.logger = logger;
        }

        [TransactionCallHandler]
        public virtual async Task Click()
        {
            logger.Information("*******");
            await Task.Yield();
        }
    }
}
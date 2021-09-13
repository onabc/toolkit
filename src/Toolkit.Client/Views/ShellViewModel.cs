using Caliburn.Micro;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Toolkit.Library.Extensions;

namespace Toolkit.Client.Views
{
    public class ShellViewModel : Conductor<IDisplayModule>.Collection.OneActive
    {
        /// <summary>
        /// 日志管理
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items"></param>
        /// <param name="logger"></param>
        public ShellViewModel(IEnumerable<IDisplayModule> items
            , ILogger logger)
        {
            DisplayName = "Toolkit";

            Items.AddRange(items.OrderBy(item => item.SortNumber));
            this.logger = logger;
        }

        /// <summary>
        /// 激活时触发
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            await ActivateFirstItemAsync();
        }

        /// <summary>
        /// 激活第一个子模块
        /// </summary>
        /// <returns></returns>
        private async Task ActivateFirstItemAsync()
        {
            if (!Items.IsNullOrEmpty())
            {
                var firstItem = Items.FirstOrDefault();
                await ActivateItemAsync(firstItem);

                logger.Information("start app");
            }
        }
    }
}
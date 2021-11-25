using Caliburn.Micro;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Toolkit.Client.Views
{
    public class ShellViewModel : Conductor<IDisplayModule>.Collection.OneActive
    {
        /// <summary>
        /// 事件消息中介
        /// </summary>
        private readonly IMediator mediator;

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
            , IMediator mediator
            , ILogger logger)
        {
            DisplayName = "Toolkit";

            Items.AddRange(items.OrderBy(item => item.SortNumber));
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// 激活时触发
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await this.ActivateFirstItemAsync();
            await base.OnActivateAsync(cancellationToken);
        }
    }
}
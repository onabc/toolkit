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

        private string PreContent = "";

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

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            if (view is ShellView shell)
            {
                shell.SourceInitialized += (s, e) =>
                {
                    HwndSource hs = PresentationSource.FromVisual(shell) as HwndSource;

                    hs.AddHook(WndProc);
                };
                shell.Closing += (s, e) =>
                {
                    HwndSource hs = PresentationSource.FromVisual(shell) as HwndSource;

                    hs.RemoveHook(WndProc);
                };
            }
        }

        private string GetStringData()
        {
            string ret = "";
            System.Windows.IDataObject iData = System.Windows.Clipboard.GetDataObject();
            if (iData.GetDataPresent(System.Windows.DataFormats.Text))
            {
                ret = (string)iData.GetData(System.Windows.DataFormats.Text);
            }
            return ret;
        }

        private const int WM_DRAWCLIPBOARD = 0x308;

        /// <summary>
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_DRAWCLIPBOARD)
            {
            }
            try
            {
                string temp = GetStringData();
                if (temp != PreContent)
                {
                    PreContent = temp;
                    ClipboardNotification notification = new ClipboardNotification(temp);
                    mediator.Publish(notification);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "WndProc");
            }
            return IntPtr.Zero;
        }
    }
}
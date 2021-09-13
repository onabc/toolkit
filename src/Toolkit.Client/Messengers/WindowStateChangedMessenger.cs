using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Toolkit.Client.Messengers
{
    /// <summary>
    /// 窗体状态变化消息
    /// </summary>
    public class WindowStateChangedMessenger
    {
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowState State { get; init; }

        public WindowStateChangedMessenger(WindowState state)
        {
            State = state;
        }
    }
}
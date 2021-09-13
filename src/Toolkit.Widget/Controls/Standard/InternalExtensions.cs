using System;
using System.Collections.Generic;
using System.Windows;

namespace Toolkit.Widget.Standard
{
    internal static class InternalExtensions
    {
        /// <summary>
        ///执行窗口操作。
        /// </summary>
        ///<param name＝“action”>执行的窗口操作。</param>
        ///<param name＝“source”>要执行操作的UI要素。对主机此元素的窗口执行操作。</param>
        public static void Invoke(this WindowAction action, FrameworkElement source)
        {
            var window = Window.GetWindow(source);
            if (window == null) return;

            switch (action)
            {
                case WindowAction.Active:
                    window.Activate();
                    break;

                case WindowAction.Close:
                    SystemCommands.CloseWindow(window);
                    break;

                case WindowAction.Maximize:
                    SystemCommands.MaximizeWindow(window);
                    break;

                case WindowAction.Minimize:
                    SystemCommands.MinimizeWindow(window);
                    break;

                case WindowAction.Normalize:
                    SystemCommands.RestoreWindow(window);
                    break;

                case WindowAction.OpenSystemMenu:
                    var point = source.PointToScreen(new Point(0, source.ActualHeight));
                    SystemCommands.ShowSystemMenu(window, point);
                    break;
            }
        }

        /// <summary>
        /// 比较当前字符串和指定字符串。不区分大小写。
        /// </summary>
        public static bool Compare(this string strA, string strB)
        {
            return String.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence) action(item);
        }
    }
}
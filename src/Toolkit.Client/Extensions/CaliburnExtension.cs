using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Toolkit.Library.Extensions;

namespace Caliburn.Micro
{
    public static class CaliburnExtension
    {
        /// <summary>
        /// 激活第一个子模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conductor"></param>
        /// <returns></returns>
        public static async Task ActivateFirstItemAsync<T>(this Conductor<T>.Collection.OneActive conductor) where T : class
        {
            var items = conductor.Items;
            if (!items.IsNullOrEmpty())
            {
                var firstItem = items.FirstOrDefault();
                await conductor.ActivateItemAsync(firstItem);
            }
        }


        /// <summary>
        /// 失活所有子项
        /// </summary>
        /// <typeparam name="T">子项的类型</typeparam>
        /// <param name="conductor">父项</param>
        /// <param name="needClose">移除</param>
        public static async Task DeactivateAllAsync<T>(this Conductor<T>.Collection.AllActive conductor, bool needClose) where T : class
        {
            T[] oldItems = conductor.Items.ToArray();
            foreach (T ji in oldItems)
            {
                await conductor.DeactivateItemAsync(ji, needClose);
            }
        }

        /// <summary>
        /// 激活多个给定的子项
        /// </summary>
        /// <typeparam name="T">子项类型</typeparam>
        /// <param name="source">激活者</param>
        /// <param name="items">被激活子项</param>
        /// <returns></returns>
        public static async Task ActivateManyAsync<T>(this Conductor<T>.Collection.AllActive source, IEnumerable<T> items) where T : class
        {
            if (source == null) return;
            if (items == null) return;
            foreach (var ji in items)
            {
                await source.ActivateItemAsync(ji);
            }
        }

        /// <summary>
        /// 重置列表项（清空），之后再填入指定的项。
        /// </summary>
        public static BindableCollection<T> Reset<T>(this BindableCollection<T> source, params T[] items)
        {
            if (source == null) return null;
            source.Clear();
            if (items != null)
            {
                source.AddRange(items);
            }
            return source;
        }

        /// <summary>
        /// 从指定项开始移除所有后续项
        /// </summary>
        /// <typeparam name="T">列表项类型</typeparam>
        /// <param name="source">列表</param>
        /// <param name="startItem">定位开始项</param>
        /// <param name="removeStartItem">【false：保留开始项，移除所有后续项】【true：移除包括开始项和后续项】</param>
        public static BindableCollection<T> RemoveTail<T>(this BindableCollection<T> source, T startItem, bool removeStartItem = false)
        {
            if (startItem is null) throw new ArgumentNullException(nameof(startItem));
            if (source == null) return null;

            var tail = source.SkipWhile(dd => !Equals(dd, startItem)).ToList();
            if (!removeStartItem)
            {
                tail.Remove(startItem);
            }
            source.RemoveRange(tail);

            return source;
        }

        /// <summary>
        /// 获取矢量图资源
        /// </summary>
        public static Geometry TryFindGeometry(this IViewAware viewModel, string key)
        {
            return viewModel.GetView() is FrameworkElement view
                ? view.TryFindResource(key) as Geometry
                : Application.Current.TryFindResource(key) as Geometry;
        }

        /// <summary>
        /// 获取 viewmodel 对应的 view，并返回它所在的窗体。
        /// </summary>
        /// <returns>【Window：所在窗体】【null：无法获取控件】</returns>
        public static Window GetWindow(this IViewAware viewModel)
        {
            Window win_result = null;
            Execute.OnUIThread(() =>
            {
                if (viewModel.GetView() is DependencyObject fe)
                {
                    if (fe is Window win)
                    {
                        win_result = win;
                    }
                    else
                    {
                        win_result = Window.GetWindow(fe);
                    }
                }
            });

            return win_result;
        }
    }
}
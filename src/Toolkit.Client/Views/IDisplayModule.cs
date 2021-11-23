using Caliburn.Micro;
using System.Windows.Media;

namespace Toolkit.Client
{
    /// <summary>
    /// 主程序的子模块接口
    /// </summary>
    public interface IDisplayModule : IScreen
    {
        /// <summary>
        /// 图标
        /// </summary>
        Geometry Icon { get; }

        /// <summary>
        /// 序号
        /// </summary>
        byte SortNumber { get; }
    }
}
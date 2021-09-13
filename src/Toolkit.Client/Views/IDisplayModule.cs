using Caliburn.Micro;

namespace Toolkit.Client
{
    /// <summary>
    /// 主程序的子模块接口
    /// </summary>
    public interface IDisplayModule : IScreen
    {
        /// <summary>
        /// 序号
        /// </summary>
        byte SortNumber { get; init; }
    }
}
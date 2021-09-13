namespace Toolkit.Widget.Standard
{
    /// <summary>
    /// 定义表示窗口操作的标识符。
    /// </summary>
    public enum WindowAction
    {
        /// <summary>
        /// 窗口操作不执行
        /// </summary>
        None,

        /// <summary>
        /// 激活窗口
        /// </summary>
        Active,

        /// <summary>
        /// 关闭窗口。
        /// </summary>
        Close,

        /// <summary>
        /// 窗口变为正常状态
        /// </summary>
        Normalize,

        /// <summary>
        /// 最大化窗口。
        /// </summary>
        Maximize,

        /// <summary>
        /// 最小化窗口
        /// </summary>
        Minimize,

        /// <summary>
        /// 窗口的系统菜单
        /// </summary>
        OpenSystemMenu,
    }
}
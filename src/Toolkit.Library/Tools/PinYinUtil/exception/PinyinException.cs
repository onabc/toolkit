using System;

namespace Toolkit.Library.Tools
{
    /// <summary>
    /// 拼音异常类
    /// </summary>
    public class PinyinException : Exception
    {
        public PinyinException(string message) : base(message)
        {
        }
    }
}
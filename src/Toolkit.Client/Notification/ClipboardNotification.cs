using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Client
{
    /// <summary>
    /// 剪贴板消息
    /// </summary>
    public class ClipboardNotification : INotification
    {
        public ClipboardNotification(string content)
        {
            Content = content;
        }

        /// <summary>
        /// 剪贴板内容
        /// </summary>
        public string Content { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Library.Tools
{
    /// <summary>
    /// 对象描述
    /// </summary>
    public class ObjectDesc
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime PutTime { get; set; }
    }
}
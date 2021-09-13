using Qiniu.RS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Library.Extensions;

namespace Toolkit.Library.Tools
{
    internal static class QiniuExtensions
    {
        internal static ObjectDesc ConvertToObjectDesc(this FileDesc fileDesc)
        {
            var obj = new ObjectDesc();

            obj.Key = fileDesc.Key;
            obj.FileSize = fileDesc.Fsize.ToFileSizeString();
            obj.MimeType = fileDesc.MimeType;

            var time = (long)(fileDesc.PutTime / Math.Pow(10, 4));
            obj.PutTime = DateTimeUtil.LongTimeStampToDateTime(time);
            return obj;
        }
    }
}
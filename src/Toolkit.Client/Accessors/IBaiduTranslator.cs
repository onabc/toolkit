using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Client
{
    /// <summary>
    /// 百度翻译接口
    /// </summary>
    public interface IBaiduTranslator
    {
        [Get("")]
        IObservable<BaiduTranslatorResponse> Translate([Query] BaiduTranslatorQueryParam queryParam);
    }
}
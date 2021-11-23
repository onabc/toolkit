using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Client
{
    public interface IBaiduTranslator
    {
        [Get("")]
        IObservable<BaiduTranslatorResponse> Translate([Query] object queryParam);
    }

    /// <summary>
    /// 百度翻译传参
    /// </summary>
    public class BaiduTranslatorQueryParam
    {
        /// <summary>
        /// 请求翻译query
        /// </summary>
        public string Q { get; set; }

        /// <summary>
        /// 翻译源语言
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 翻译目标语言
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// APP ID
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
    }

    public class BaiduTranslatorResponse
    {
        /// <summary>
        /// 翻译源语言
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 翻译目标语言
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 翻译结果
        /// </summary>
        [JsonProperty("trans_result")]
        public IEnumerable<BaiduTranslatorResult> Results { get; set; }
    }

    public class BaiduTranslatorResult
    {
        /// <summary>
        /// 原文
        /// </summary>
        public string Src { get; set; }

        /// <summary>
        /// 译文
        /// </summary>
        public string Dst { get; set; }
    }
}
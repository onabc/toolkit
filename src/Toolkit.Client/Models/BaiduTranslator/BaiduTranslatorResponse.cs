using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Client
{
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
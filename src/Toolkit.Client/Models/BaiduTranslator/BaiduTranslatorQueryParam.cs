using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Client
{
    /// <summary>
    /// 百度翻译传参
    /// </summary>
    public class BaiduTranslatorQueryParam
    {
        public BaiduTranslatorQueryParam(string queryString, string appid, string secretKey)
        {
            this.q = queryString;
            this.from = "en";
            this.to = "zh";
            this.appid = appid;
            this.salt = rd.Next(100000).ToString();
            this.sign = EncryptString(appid + queryString + salt + secretKey);
        }

        private Random rd = new Random();

        /// <summary>
        /// 请求翻译query
        /// </summary>
        public string q { get; set; }

        /// <summary>
        /// 翻译源语言
        /// </summary>
        public string from { get; set; }

        /// <summary>
        /// 翻译目标语言
        /// </summary>
        public string to { get; set; }

        /// <summary>
        /// APP ID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string salt { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        // 计算MD5值
        private static string EncryptString(string str)
        {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }
    }
}
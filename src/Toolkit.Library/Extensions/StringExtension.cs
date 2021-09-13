using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Toolkit.Library.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 移除字符串中所有的空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveAllSpace(this string str)
            => str.Replace(" ", "");

        /// <summary>
        /// 返回一个值，该值指示指定的 System.String 对象是否出现在此字符串中
        /// <para>忽略字符串的大小写</para>
        /// </summary>
        /// <param name="str">字符串，可以是任意值</param>
        /// <param name="value">要检索的内容</param>
        public static bool ContainsIgnoreCase(this string str, string value)
            => !string.IsNullOrWhiteSpace(value) && str.ToLower().Contains(value.ToLower());

        /// <summary>
        /// 移除前缀字符串
        /// </summary>
        /// <param name="self"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemovePrefixString(this string self, string str)
        {
            string strRegex = @"^(" + str + ")";
            return Regex.Replace(self, strRegex, "");
        }

        /// <summary>
        /// 移除后缀字符串
        /// </summary>
        /// <param name="self"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSuffixString(this string self, string str)
        {
            string strRegex = @"(" + str + ")" + "$";
            return Regex.Replace(self, strRegex, "");
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
            => string.IsNullOrEmpty(s);

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, params object[] args)
            => string.Format(format, args);

        /// <summary>
        /// 正则
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string s, string pattern)
            => s is not null && Regex.IsMatch(s, pattern);

        /// <summary>
        /// 正则
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string Match(this string s, string pattern)
            => s is null ? string.Empty : Regex.Match(s, pattern).Value;

        // 是否为Email
        public static bool IsEmail(this string self)
        {
            return self.IsMatch(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        // 是否为域名
        public static bool IsDomain(this string self)
        {
            return self.IsMatch(@"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(/.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+/.?");
        }

        // 是否为IP地址
        public static bool IsIP(this string self)
        {
            return self.IsMatch(@"((?:(?:25[0-5]|2[0-4]\\d|[01]?\\d?\\d)\\.){3}(?:25[0-5]|2[0-4]\\d|[01]?\\d?\\d))");
        }

        // 是否为手机号码
        public static bool IsMobilePhone(this string self)
        {
            return self.IsMatch(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$");
        }

        // 转换为32位MD5
        public static string ConvertToMD5_32(this string self)
        {
            return ConvertToMD5(self, "x2");
        }

        // 转换为48位MD5
        public static string ConvertToMD5_48(this string self)
        {
            return ConvertToMD5(self, "x3");
        }

        // 转换为64位MD5
        public static string ConvertToMD5_64(this string self)
        {
            return ConvertToMD5(self, "x4");
        }

        // 转换为MD5, 加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
        private static string ConvertToMD5(this string self, string flag = "x2")
        {
            byte[] sor = Encoding.UTF8.GetBytes(self);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString(flag));
            }
            return strbul.ToString();
        }
    }
}
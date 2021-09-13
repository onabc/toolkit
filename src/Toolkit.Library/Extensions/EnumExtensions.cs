using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Toolkit.Library.Extensions
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取description特性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            return GetCustomAttribute<DescriptionAttribute>(value)?.Description ?? value.ToString();
        }

        /// <summary>
        /// 获取description特性，以小写形式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetLowerDescription(this Enum value)
        {
            return GetCustomAttribute<DescriptionAttribute>(value)?.Description ?? value.ToString().ToLower();
        }

        /// <summary>
        /// 获取指定类型的特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetCustomAttribute<T>(this Enum value) where T : Attribute
        {
            return GetField(value)?.GetCustomAttribute<T>(false);
        }

        /// <summary>
        /// 获取枚举值的FieldInfo
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldInfo GetField(this Enum value)
        {
            var u64 = ToUInt64(value);
            return value.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .FirstOrDefault(f => ToUInt64(f.GetRawConstantValue()) == u64);
        }

        /// <summary>
        /// 检查是否为此枚举值定义了枚举常量
        /// </summary>
        public static bool IsDefined(this Enum value)
        {
            return GetField(value) != null;
        }

        /// <summary>
        /// 枚举值转换为UInt64类型
        /// </summary>
        public static ulong ToUInt64(this Enum value) => ToUInt64((object)value);

        private static ulong ToUInt64(object value)
        {
            switch (Convert.GetTypeCode(value))
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return unchecked((ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture));

                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Char:
                case TypeCode.Boolean:
                    return Convert.ToUInt64(value, CultureInfo.InvariantCulture);

                default: throw new InvalidOperationException("UnknownEnumType");
            }
        }
    }
}
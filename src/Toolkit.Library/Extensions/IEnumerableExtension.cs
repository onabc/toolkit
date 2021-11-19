using System;
using System.Collections.Generic;
using System.Linq;

namespace Toolkit.Library.Extensions
{
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 检测是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
            => source is null || !source.Any();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetList<T>(this IEnumerable<T> source, IEnumerable<int> indexs)
        {
            List<T> list = new List<T>();
            foreach (int index in indexs)
            {
                if (index > -1 && index < source.Count())
                {
                    list.Add(source.ElementAt(index));
                }
            }
            return list;
        }


        /// <summary>
        /// 拼接内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source
         , params T[] ts) => source.Concat(ts);

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action 不能为空");
            }

            foreach (T item in source)
            {
                action(item);
            }

            return source;
        }

        public static T ForFirst<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action 不能为空");
            }

            T val = source.FirstOrDefault();
            if (val != null)
            {
                action(val);
            }

            return val;
        }

        public static T ForLast<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action 不能为空");
            }

            T val = source.LastOrDefault();
            if (val != null)
            {
                action(val);
            }

            return val;
        }
    }
}
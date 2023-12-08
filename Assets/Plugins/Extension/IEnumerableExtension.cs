using System;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class IEnumerableExtension
    {

        /// <summary>
        /// 集合是否空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <returns>结果</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            var empty = true;
            foreach(var value in source)
            {
                if (value != null)
                    empty = false;
            }
            return empty;
        }

        public static List<T> ToList<T>(this IEnumerable<T> source)
        {
            var list = new List<T>();
            foreach (var item in source)
            {
                list.Add(item);
            }

            return list;
        }

        public static Dictionary<TKey, T> ToDictionary<TKey, T>(this IList<T> list, Func<T, TKey> getKeyFunc)
        {
            var result = new Dictionary<TKey, T>();
            if (list == null) return result;
            var count = list.Count;
            for (var i = 0; i < count; i++)
            {
                var item = list[i];
                result.Add(getKeyFunc(item), item);
            }

            return result;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class IListExtension
    {
        internal static Random Rand = new Random();

        /// <summary>
        /// 获取随机元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>结果</returns>
        public static T Random<T>(this IList<T> list)
        {
            var result = list.Count > 0 ? list[Rand.Next(0, list.Count)] : default(T);
            return result;
        }

        public static void Foreach<T>(this IList<T> list,Action<T> action)
        {
            foreach(var value in list)
            {
                action?.Invoke(value);
            }
        }
    }
}

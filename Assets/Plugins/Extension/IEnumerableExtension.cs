using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            return !source.Any();
        }
    }
}

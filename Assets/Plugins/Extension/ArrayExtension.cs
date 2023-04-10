using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class ArrayExtension
    {
        /// <summary>
        /// 是否包含某个元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="item">元素</param>
        /// <returns>结果</returns>
        public static bool Contains<T>(this T[] array, T item)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var temp = array[i];
                if (temp.Equals(item)) return true;
            }

            return false;
        }
    }
}

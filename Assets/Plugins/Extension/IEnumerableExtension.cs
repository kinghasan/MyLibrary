using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Aya.Extension
{
    public static class IEnumerableExtension
    {

        /// <summary>
        /// �����Ƿ��
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="source">����</param>
        /// <returns>���</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }
    }
}

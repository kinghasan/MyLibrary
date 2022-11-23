using System;
using System.Collections;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class IListExtension
    {
        internal static Random Rand = new Random();

        /// <summary>
        /// ��ȡ���Ԫ��
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="list">�б�</param>
        /// <returns>���</returns>
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

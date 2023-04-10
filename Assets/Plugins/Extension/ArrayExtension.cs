using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class ArrayExtension
    {
        /// <summary>
        /// �Ƿ����ĳ��Ԫ��
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="array">����</param>
        /// <param name="item">Ԫ��</param>
        /// <returns>���</returns>
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

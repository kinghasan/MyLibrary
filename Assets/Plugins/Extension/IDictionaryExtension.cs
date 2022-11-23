using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class IDictionaryExtension
    {
        /// <summary>
        /// ��ȡֵ���粻��������Ӳ�����Ĭ��ֵ�������ã�
        /// </summary>
        /// <typeparam name="TKey">������</typeparam>
        /// <typeparam name="TValue">ֵ����</typeparam>
        /// <param name="dictionary">�ֵ�</param>
        /// <param name="key">��</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns>���</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            if (dictionary.TryGetValue(key, out var ret))
            {
                return ret;
            }

            dictionary.Add(key, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// ��ӣ��������滻
        /// </summary>
        /// <typeparam name="TKey">������</typeparam>
        /// <typeparam name="TValue">ֵ����</typeparam>
        /// <param name="dictionary">�ֵ�</param>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        /// <returns>dic</returns>
        public static IDictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            dictionary[key] = value;
            return dictionary;
        }
    }
}

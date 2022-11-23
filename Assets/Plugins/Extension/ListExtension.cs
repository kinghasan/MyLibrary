using System;
using System.Collections;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class ListExtension
    {
        /// <summary>
        /// ��ȡ���һ��Ԫ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Last<T>(this List<T> list)
        {
            var result = list != null && list.Count > 0 ? list[list.Count - 1] : default(T);
            return result;
        }

        /// <summary>
        /// �����б�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static List<T> Copy<T>(this List<T> From)
        {
            var To = new List<T>();

            foreach(var value in From)
            {
                To.Add(value);
            }
            return To;
        }

        /// <summary>
        /// �� Key ���ȼ���������
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="list">�б�</param>
        /// <param name="keyGetters">�Ƚ�ֵ������</param>
        /// <returns>���</returns>
        public static List<T> SortDesc<T>(this List<T> list, params Func<T, IComparable>[] keyGetters)
        {
            list.Sort(ComparisonUtil.GetDescComparison(keyGetters));
            return list;
        }

        /// <summary>
        /// �� Key ���ȼ���������
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="list">�б�</param>
        /// <param name="keyGetters">�Ƚ�ֵ������</param>
        /// <returns>���</returns>
        public static List<T> SortAsc<T>(this List<T> list, params Func<T, IComparable>[] keyGetters)
        {
            list.Sort(ComparisonUtil.GetAscComparison(keyGetters));
            return list;
        }

        /// <summary>
        /// ����ָ���Ƚ�ֵ��ȡ���ļ���Ԫ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <param name="keyGetter"></param>
        /// <returns></returns>
        public static List<T> Max<T>(this List<T> list, int count, Func<T, IComparable> keyGetter)
        {
            if (list == null || list.Count == 0) return default;
            if (count > list.Count) throw new ArgumentOutOfRangeException();
            var indexList = new List<int>();
            for (var i = 0; i < list.Count; i++)
            {
                indexList.Add(i);
            }

            indexList.SortDesc(i => keyGetter(list[i]));

            var result = new List<T>();
            for (var i = 0; i < count; i++)
            {
                result.Add(list[indexList[i]]);
            }

            return result;
        }

        /// <summary>
        /// ����ָ���Ƚ�ֵ��ȡ��С�ļ���Ԫ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <param name="keyGetter"></param>
        /// <returns></returns>
        public static List<T> Min<T>(this List<T> list, int count, Func<T, IComparable> keyGetter)
        {
            if (list == null || list.Count == 0) return default;
            if (count > list.Count) throw new ArgumentOutOfRangeException();
            var indexList = new List<int>();
            for (var i = 0; i < list.Count; i++)
            {
                indexList.Add(i);
            }

            indexList.SortAsc(i => keyGetter(list[i]));

            var result = new List<T>();
            for (var i = 0; i < count; i++)
            {
                result.Add(list[indexList[i]]);
            }

            return result;
        }
    }
}

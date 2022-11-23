using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public class SortData<T>
    {
        public Vector2 Key;
        public T Value;
    }

    public class SortManager<T>
    {
        public SortData<T>[] Datas;

        public SortManager(SortData<T>[] datas)
        {
            Datas = datas;
        }

        public SortData<T> GetData(Vector2 key)
        {
            foreach(var data in Datas)
            {
                if (data.Key == key)
                    return data;
            }

            return null;
        }

        public int GetHigh(int Index)
        {
            var max = Datas.Length;
            return Index / max;
        }

        public SortData<T> GetData(int Index)
        {
            var max = Datas.Length;
            var dataKey = Index % max;
            return Datas[dataKey];
        }
    }

    public static class SortExtension
    {
        public static SortManager<Vector2> SortVector2(int x, int y, Vector2 size)
        {
            var result = new SortData<Vector2>[x * y];
            for (var i = 0; i < y; i++)
            {
                var rY = i * size.y;
                for (var j = 0; j < x; j++)
                {
                    var rX = j * size.x;
                    var data = new SortData<Vector2>();
                    data.Key = new Vector2(j, i);
                    data.Value = new Vector2(rX, rY);
                    result[i * x + j] = data;
                }
            }

            return new SortManager<Vector2>(result);
        }

        /// <summary>
        /// 中心点排序
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static SortManager<Vector2> SortVector2Center(int x, int y, Vector2 size)
        {
            var result = new SortData<Vector2>[x * y];
            for (var i = 0; i < y; i++)
            {
                var rY = i * size.y;
                rY -= 1f * y / 2f * size.y - 0.5f * size.y;
                for (var j = 0; j < x; j++)
                {
                    var rX = j * size.x;
                    rX -= 1f * x / 2f * size.x - 0.5f * size.x;
                    var data = new SortData<Vector2>();
                    data.Key = new Vector2(j, i);
                    data.Value = new Vector2(rX, rY);
                    result[i * x + j] = data;
                }
            }

            return new SortManager<Vector2>(result);
        }

        /// <summary>
        /// 连线排序，用来给顶点排序
        /// </summary>
        /// <param name="sortList"></param>
        /// <returns></returns>
        public static List<Vector3> ConnectionSort(List<Vector3> sortList)
        {
            var MoveList = new List<Vector3>();
            for (var i = sortList.Count - 1; i >= 0; i--)
            {
                var near = sortList.Last();
                var mar = (near - MoveList.Last()).magnitude;
                for (var j = 0; j < sortList.Count; j++)
                {
                    var ver = sortList[j];
                    var verMar = (ver - MoveList.Last()).magnitude;
                    if (verMar < mar)
                    {
                        near = ver;
                        mar = verMar;
                    }
                }
                sortList.Remove(near);
                MoveList.Add(near);
            }
            return MoveList;
        }
    }
}

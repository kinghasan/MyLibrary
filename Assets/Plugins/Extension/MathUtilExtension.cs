using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class MathUtilExtension
    {
        #region Polygon

        /// <summary>
        /// �жϵ��Ƿ��ڶ���������ڣ����߷�/֧�ְ�����Σ�
        /// </summary>
        /// <param name="point">���жϵĵ�</param>
        /// <param name="polygonPoints">����ζ�������</param>
        /// <param name="containEdge">������</param>
        /// <returns>true:�ڶ�����ڣ�����   false���ڶ�����⣬͹��</returns>
        public static bool IsPointInPolygon(Vector2 point, Vector2[] polygonPoints, bool containEdge = true)
        {
            var result = false;
            var j = polygonPoints.Length - 1;
            for (var i = 0; i < polygonPoints.Length; j = i++)
            {
                var p1 = polygonPoints[i];
                var p2 = polygonPoints[j];
                // �����ж��Ƿ�պñ�����ڶ���εı���
                if (IsPointOnLineSegment(point, p1, p2)) return containEdge;
                if ((p1.y > point.y != p2.y > point.y) && (point.x < (point.y - p1.y) * (p1.x - p2.x) / (p1.y - p2.y) + p1.x))
                {
                    result = !result;
                }
            }
            return result;
        }

        /// <summary>
        /// �жϵ��Ƿ����߶���
        /// </summary>
        /// <param name="point">��</param>
        /// <param name="p1">�߶ζ˵�1</param>
        /// <param name="p2">�߶ζ˵�2</param>
        /// <returns>���</returns>
        public static bool IsPointOnLineSegment(Vector2 point, Vector2 p1, Vector2 p2)
        {
            var disLine = Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2);
            var dis1 = Math.Pow(point.x - p1.x, 2) + Math.Pow(point.y - p1.y, 2);
            var dis2 = Math.Pow(point.x - p2.x, 2) + Math.Pow(point.y - p2.y, 2);
            var result = Math.Abs(dis1 + dis2 - disLine) < MathUtil.FloatPrecision;
            return result;
        }

        /// <summary>
        /// ���Ȼ�ȡ���б�
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="pointSize"></param>
        /// <param name="staggeredX">X��ƫ��</param>
        /// <param name="staggeredY">Y��ƫ��</param>
        /// <returns></returns>
        public static List<Vector2> GetPointsInPolygon(Vector2[] positions, Vector2 pointSize, float staggeredX = 0f, float staggeredY = 0f)
        {
            var minX = 0f;
            var minY = 0f;
            var MaxX = 0f;
            var MaxY = 0f;
            foreach (var position in positions)
            {
                if (position.x < minX)
                    minX = position.x;
                if (position.x > MaxX)
                    MaxX = position.x;
                if (position.y < minY)
                    minY = position.y;
                if (position.y > MaxY)
                    MaxY = position.y;
            }

            var x = (int)(Math.Abs(minX - MaxX) / pointSize.x);
            var y = (int)(Math.Abs(minY - MaxY) / pointSize.y);
            var resultList = new List<Vector2>();

            var stdX = false;
            var stdY = false;
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    var point = new Vector2(minX + i * pointSize.x, minY + j * pointSize.y);
                    if (stdX)
                        point.x += staggeredX;
                    if (stdY)
                        point.y += staggeredY;

                    if (IsPointInPolygon(point, positions))
                    {
                        resultList.Add(point);
                    }
                }
                stdX = !stdX;
                stdY = !stdY;
            }

            return resultList;
        }

        /// <summary>
        /// �Ӷ�����л�ȡ���λ��
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Vector3 GetPositionInPolygon(Vector3[] positions)
        {
            var rand1 = positions.Random();
            var rand2 = positions.Random();
            var rand3 = positions.Random();

            var randValue = UnityEngine.Random.Range(0f, 1f);
            var result = Vector3.Lerp(rand1, rand2, randValue);
            randValue = UnityEngine.Random.Range(0f, 1f);
            result = Vector3.Lerp(result, rand3, randValue);

            return result;
        }

        /// <summary>
        /// �Ӷ�����л�ȡ������λ��
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Vector3[] GetPositionsInPolygon(Vector3[] positions,int num)
        {
            var rand1 = positions.Random();
            var rand2 = positions.Random();
            var rand3 = positions.Random();

            var result = new Vector3[num];
            for(var i = 0; i < num; i++)
            {
                var randValue = UnityEngine.Random.Range(0f, 1f);
                var pos = Vector3.Lerp(rand1, rand2, randValue);
                randValue = UnityEngine.Random.Range(0f, 1f);
                pos = Vector3.Lerp(pos, rand3, randValue);
                result[i] = pos;
            }

            return result;
        }

        #endregion
    }

    public static class MathUtil
    {
        #region Const

        /// <summary>
        /// ���㾫��
        /// </summary>
        public static float FloatPrecision = 1e-6f;

        /// <summary>
        /// 2��ƽ����
        /// </summary>
	    public const float Sqrt2 = 1.41421356f;

        /// <summary>
        /// 3��ƽ����
        /// </summary>
        public const float Sqrt3 = 1.73205081f;

        /// <summary>
        /// 2�� PI
        /// </summary>
        public const float TwoPi = 6.28318531f;

        /// <summary>
        /// 0.5�� PI
        /// </summary>
        public const float HalfPi = 1.57079633f;

        /// <summary>
        /// һ�����֮һ
        /// </summary>
        public const float OneMillionth = 1e-6f;

        /// <summary>
        /// һ����
        /// </summary>
        public const float Million = 1e6f;

        #endregion
    }
}

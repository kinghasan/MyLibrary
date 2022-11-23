using System;
using UnityEngine;
using Random = System.Random;

namespace Aya.Extension
{
    public static class RandExtension
    {
        /// <summary>
        /// 随机数发生器静态实例
        /// </summary>
        internal static Random Rand = new Random();

		#region Rand Seed
		/// <summary>
		/// 设置随机数种子
		/// </summary>
		/// <param name="seed">种子</param>
		public static void SetRandSeed(int seed)
		{
			Rand = new Random(seed);
		}

		/// <summary>
		/// 重设随机数种子
		/// </summary>
		public static void ResetRandSeed()
		{
			Rand = new Random();
		}
		#endregion

		#region CSharp 获取随机数函数(int, float, bool, byte[], enum, dateTime)
		/// <summary>
		/// 生成一个指定范围的随机整数，该随机数范围包括最小值(不包含最大值)
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		public static int RandInt(int min, int max)
		{
			return Rand.Next(min, max);
		}

		/// <summary>
		/// 生成一个指定范围的随机浮点数，该随机数范围包括最小值(不包含最大值)
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		public static float RandFloat(float min, float max)
		{
			return min + UnityEngine.Random.value * (max - min);
		}

		/// <summary>
		/// 生成一个0.0到1.0的随机小数
		/// </summary>
		public static float RandZeroToOne()
		{
			return UnityEngine.Random.value;
		}

		/// <summary>
		/// 生成一个true或false的值
		/// </summary>
		/// <param name="value">false的概率，默认0.5</param>
		/// <returns>结果</returns>
		public static bool RandBool(float value = 0.5f)
		{
			return Rand.NextDouble() > value;
		}

		/// <summary>
		/// 随机生成枚举值
		/// </summary>
		/// <typeparam name="T">枚举类型</typeparam>
		/// <returns>结果</returns>
		public static T RandEnum<T>() where T : struct
		{
			var type = typeof(T);
			if (type.IsEnum == false) throw new InvalidOperationException();
			var array = Enum.GetValues(type);
			var index = Rand.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
			return (T)array.GetValue(index);
		}

		/// <summary>
		/// 随机生成字节数组
		/// </summary>
		/// <param name="length">长度</param>
		/// <returns>结果</returns>
		public static byte[] RandBytes(int length)
		{
			var data = new byte[length];
			Rand.NextBytes(data);
			return data;
		}

		/// <summary>
		/// 随机生成日期
		/// </summary>
		/// <param name="minValue">最小日期</param>
		/// <param name="maxValue">最大日期</param>
		/// <returns>结果</returns>
		public static DateTime RandDateTime(DateTime minValue, DateTime maxValue)
		{
			var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * Rand.NextDouble());
			return new DateTime(ticks);
		}

		/// <summary>
		/// 随机生成日期
		/// </summary>
		/// <returns>结果</returns>
		public static DateTime RandDateTime()
		{
			return RandDateTime(DateTime.MinValue, DateTime.MaxValue);
		}

		#endregion

		#region Unity 获取随机数函数(Vector,Quaternion,Color)
		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <returns>结果</returns>
		public static Vector2 RandVector2(float min = float.MinValue, float max = float.MaxValue)
		{
			return new Vector2(RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="from">开始</param>
		/// <param name="to">结束</param>
		/// <returns>结果</returns>
		public static Vector2 RandVector2(Vector2 from, Vector2 to)
		{
			return new Vector2(RandFloat(from.x, to.x), RandFloat(from.y, to.y));
		}

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <returns>结果</returns>
		public static Vector3 RandVector3(float min = float.MinValue, float max = float.MaxValue)
		{
			return new Vector3(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="from">开始</param>
		/// <param name="to">结束</param>
		/// <returns>结果</returns>
		public static Vector3 RandVector3(Vector3 from, Vector3 to)
		{
			return new Vector3(RandFloat(from.x, to.x), RandFloat(from.y, to.y), RandFloat(from.z, to.z));
		}

		/// <summary>
		/// 随机一个距离内的点
		/// </summary>
		/// <param name="center">中心点</param>
		/// <param name="radius">半径</param>
		/// <returns>结果</returns>
		public static Vector3 RandVector3(Vector3 center, float radius)
		{
			//以平方进行比对，比正常的Magnitude计算更快
			var range = radius * radius;
			float distance;
			var from = center - Vector3.one * radius;
			var to = center + Vector3.one * radius;
			Vector3 result;
			do
			{
				result = RandVector3(from, to);
				distance = Vector3.SqrMagnitude(result - center);
			} while (distance >= range);

			return result;
		}

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <returns>结果</returns>
		public static Vector4 RandVector4(float min = float.MinValue, float max = float.MaxValue)
		{
			return new Vector4(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="from">开始</param>
		/// <param name="to">结束</param>
		/// <returns>结果</returns>
		public static Vector4 RandVector4(Vector4 from, Vector4 to)
		{
			return new Vector4(RandFloat(from.x, to.x), RandFloat(from.y, to.y), RandFloat(from.z, to.z), RandFloat(from.w, to.w));
		}

		/// <summary>
		/// 随机生成一个旋转
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <returns>结果</returns>
		public static Quaternion RandQuaternion(float min = 0f, float max = 360f)
		{
			return Quaternion.Euler(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// 随机生成一个旋转
		/// </summary>
		/// <param name="from">开始</param>
		/// <param name="to">结束</param>
		/// <returns>结果</returns>
		public static Quaternion RandQuaternion(Quaternion from, Quaternion to)
		{
			return new Quaternion(RandFloat(from.x, to.x), RandFloat(from.y, to.y), RandFloat(from.z, to.z), RandFloat(from.w, to.w));
		}

		/// <summary>
		/// 随机生成一个颜色（0-1）
		/// </summary>
		/// <param name="min">最小值（0 - 1）</param>
		/// <param name="max">最大值（0 - 1）</param>
		/// <param name="alpha">透明度（0 - 1）</param>
		/// <returns>结果</returns>
		public static Color RandColor(float min = 0f, float max = 1f, float alpha = 1f)
		{
			return new Color(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max), alpha);
		}

		/// <summary>
		/// 随机生成一个颜色（0-255）
		/// </summary>
		/// <param name="min">最小值（0 - 255）</param>
		/// <param name="max">最大值（0 - 255）</param>
		/// <param name="alpha">透明度（0 - 255）</param>
		/// <returns>结果</returns>
		public static Color RandColor256(float min = 0f, float max = 256f, float alpha = 256f)
		{
			return new Color(RandFloat(min, max) / 255f, RandFloat(min, max) / 255f, RandFloat(min, max) / 255f, alpha / 255f);
		}

		/// <summary>
		/// 随机生成一个颜色
		/// </summary>
		/// <param name="from">开始</param>
		/// <param name="to">结束</param>
		/// <returns>结果</returns>
		public static Color RandColor(Color from, Color to)
		{
			return new Color(RandFloat(from.r, to.r), RandFloat(from.g, to.g), RandFloat(from.b, to.b), RandFloat(from.a, to.a));
		}

		#endregion
	}
}

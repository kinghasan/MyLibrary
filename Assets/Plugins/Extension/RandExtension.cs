using System;
using UnityEngine;
using Random = System.Random;

namespace Aya.Extension
{
    public static class RandExtension
    {
        /// <summary>
        /// �������������̬ʵ��
        /// </summary>
        internal static Random Rand = new Random();

		#region Rand Seed
		/// <summary>
		/// �������������
		/// </summary>
		/// <param name="seed">����</param>
		public static void SetRandSeed(int seed)
		{
			Rand = new Random(seed);
		}

		/// <summary>
		/// �������������
		/// </summary>
		public static void ResetRandSeed()
		{
			Rand = new Random();
		}
		#endregion

		#region CSharp ��ȡ���������(int, float, bool, byte[], enum, dateTime)
		/// <summary>
		/// ����һ��ָ����Χ��������������������Χ������Сֵ(���������ֵ)
		/// </summary>
		/// <param name="min">��Сֵ</param>
		/// <param name="max">���ֵ</param>
		public static int RandInt(int min, int max)
		{
			return Rand.Next(min, max);
		}

		/// <summary>
		/// ����һ��ָ����Χ����������������������Χ������Сֵ(���������ֵ)
		/// </summary>
		/// <param name="min">��Сֵ</param>
		/// <param name="max">���ֵ</param>
		public static float RandFloat(float min, float max)
		{
			return min + UnityEngine.Random.value * (max - min);
		}

		/// <summary>
		/// ����һ��0.0��1.0�����С��
		/// </summary>
		public static float RandZeroToOne()
		{
			return UnityEngine.Random.value;
		}

		/// <summary>
		/// ����һ��true��false��ֵ
		/// </summary>
		/// <param name="value">false�ĸ��ʣ�Ĭ��0.5</param>
		/// <returns>���</returns>
		public static bool RandBool(float value = 0.5f)
		{
			return Rand.NextDouble() > value;
		}

		/// <summary>
		/// �������ö��ֵ
		/// </summary>
		/// <typeparam name="T">ö������</typeparam>
		/// <returns>���</returns>
		public static T RandEnum<T>() where T : struct
		{
			var type = typeof(T);
			if (type.IsEnum == false) throw new InvalidOperationException();
			var array = Enum.GetValues(type);
			var index = Rand.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
			return (T)array.GetValue(index);
		}

		/// <summary>
		/// ��������ֽ�����
		/// </summary>
		/// <param name="length">����</param>
		/// <returns>���</returns>
		public static byte[] RandBytes(int length)
		{
			var data = new byte[length];
			Rand.NextBytes(data);
			return data;
		}

		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="minValue">��С����</param>
		/// <param name="maxValue">�������</param>
		/// <returns>���</returns>
		public static DateTime RandDateTime(DateTime minValue, DateTime maxValue)
		{
			var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * Rand.NextDouble());
			return new DateTime(ticks);
		}

		/// <summary>
		/// �����������
		/// </summary>
		/// <returns>���</returns>
		public static DateTime RandDateTime()
		{
			return RandDateTime(DateTime.MinValue, DateTime.MaxValue);
		}

		#endregion

		#region Unity ��ȡ���������(Vector,Quaternion,Color)
		/// <summary>
		/// �������һ������
		/// </summary>
		/// <param name="min">��Сֵ</param>
		/// <param name="max">���ֵ</param>
		/// <returns>���</returns>
		public static Vector2 RandVector2(float min = float.MinValue, float max = float.MaxValue)
		{
			return new Vector2(RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// �������һ������
		/// </summary>
		/// <param name="from">��ʼ</param>
		/// <param name="to">����</param>
		/// <returns>���</returns>
		public static Vector2 RandVector2(Vector2 from, Vector2 to)
		{
			return new Vector2(RandFloat(from.x, to.x), RandFloat(from.y, to.y));
		}

		/// <summary>
		/// �������һ������
		/// </summary>
		/// <param name="min">��Сֵ</param>
		/// <param name="max">���ֵ</param>
		/// <returns>���</returns>
		public static Vector3 RandVector3(float min = float.MinValue, float max = float.MaxValue)
		{
			return new Vector3(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// �������һ������
		/// </summary>
		/// <param name="from">��ʼ</param>
		/// <param name="to">����</param>
		/// <returns>���</returns>
		public static Vector3 RandVector3(Vector3 from, Vector3 to)
		{
			return new Vector3(RandFloat(from.x, to.x), RandFloat(from.y, to.y), RandFloat(from.z, to.z));
		}

		/// <summary>
		/// ���һ�������ڵĵ�
		/// </summary>
		/// <param name="center">���ĵ�</param>
		/// <param name="radius">�뾶</param>
		/// <returns>���</returns>
		public static Vector3 RandVector3(Vector3 center, float radius)
		{
			//��ƽ�����бȶԣ���������Magnitude�������
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
		/// �������һ������
		/// </summary>
		/// <param name="min">��Сֵ</param>
		/// <param name="max">���ֵ</param>
		/// <returns>���</returns>
		public static Vector4 RandVector4(float min = float.MinValue, float max = float.MaxValue)
		{
			return new Vector4(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// �������һ������
		/// </summary>
		/// <param name="from">��ʼ</param>
		/// <param name="to">����</param>
		/// <returns>���</returns>
		public static Vector4 RandVector4(Vector4 from, Vector4 to)
		{
			return new Vector4(RandFloat(from.x, to.x), RandFloat(from.y, to.y), RandFloat(from.z, to.z), RandFloat(from.w, to.w));
		}

		/// <summary>
		/// �������һ����ת
		/// </summary>
		/// <param name="min">��Сֵ</param>
		/// <param name="max">���ֵ</param>
		/// <returns>���</returns>
		public static Quaternion RandQuaternion(float min = 0f, float max = 360f)
		{
			return Quaternion.Euler(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// �������һ����ת
		/// </summary>
		/// <param name="from">��ʼ</param>
		/// <param name="to">����</param>
		/// <returns>���</returns>
		public static Quaternion RandQuaternion(Quaternion from, Quaternion to)
		{
			return new Quaternion(RandFloat(from.x, to.x), RandFloat(from.y, to.y), RandFloat(from.z, to.z), RandFloat(from.w, to.w));
		}

		/// <summary>
		/// �������һ����ɫ��0-1��
		/// </summary>
		/// <param name="min">��Сֵ��0 - 1��</param>
		/// <param name="max">���ֵ��0 - 1��</param>
		/// <param name="alpha">͸���ȣ�0 - 1��</param>
		/// <returns>���</returns>
		public static Color RandColor(float min = 0f, float max = 1f, float alpha = 1f)
		{
			return new Color(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max), alpha);
		}

		/// <summary>
		/// �������һ����ɫ��0-255��
		/// </summary>
		/// <param name="min">��Сֵ��0 - 255��</param>
		/// <param name="max">���ֵ��0 - 255��</param>
		/// <param name="alpha">͸���ȣ�0 - 255��</param>
		/// <returns>���</returns>
		public static Color RandColor256(float min = 0f, float max = 256f, float alpha = 256f)
		{
			return new Color(RandFloat(min, max) / 255f, RandFloat(min, max) / 255f, RandFloat(min, max) / 255f, alpha / 255f);
		}

		/// <summary>
		/// �������һ����ɫ
		/// </summary>
		/// <param name="from">��ʼ</param>
		/// <param name="to">����</param>
		/// <returns>���</returns>
		public static Color RandColor(Color from, Color to)
		{
			return new Color(RandFloat(from.r, to.r), RandFloat(from.g, to.g), RandFloat(from.b, to.b), RandFloat(from.a, to.a));
		}

		#endregion
	}
}

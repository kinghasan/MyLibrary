using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;

namespace Aya.Extension
{
    public static class ObjectExtension
    {
        /// <summary>
        /// ת��
        /// </summary>
        /// <typeparam name="T">Ŀ������</typeparam>
        /// <param name="obj">����</param>
        /// <returns>���</returns>
        public static T CastType<T>(this object obj)
        {
            try
            {
                var result = (T)obj.CastType(typeof(T));
                return result;
            }
            catch
            {
                var result = default(T);
                return result;
            }
        }

        /// <summary>
        /// ת��
        /// </summary>
        /// <param name="obj">����</param>
        /// <param name="type">����</param>
        /// <returns>���</returns>
        public static object CastType(this object obj, Type type)
        {
            try
            {
                var result = Convert.ChangeType(obj, type, CultureInfo.InvariantCulture);
                return result;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
                var result = type.DefaultValue();
                return result;
            }
        }

        /// <summary>
        /// �����ֶΣ���Ҫ������ͬ��
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void CopyField(this object from, object to)
        {
            var type = to.GetType();
            var bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var properties = type.GetFields(bindingFlags);
            foreach (var field in properties)
            {
                var value = field.GetValue(from);
                field.SetValue(to, value);
            }
        }

        /// <summary>
        /// ת��Ϊ string
        /// </summary>
        /// <param name="obj">����</param>
        /// <returns>���</returns>
        public static string AsString(this object obj)
        {
            var result = ReferenceEquals(obj, null) ? null : $"{obj}";
            return result;
        }

        #region Default

        /// <summary>
        /// ��ȡ���͵�Ĭ��ֵ
        /// </summary>
        /// <param name="type">����</param>
        /// <returns>���</returns>
        public static object DefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// �Ƿ�Ϊ�ɿ�����
        /// </summary>
        /// <param name="type">����</param>
        /// <returns>���</returns>
        public static bool IsNullableType(this Type type)
        {
            if (!type.IsPrimitive && !type.IsValueType)
            {
                return !type.IsEnum;
            }
            return false;
        }

        #endregion

        #region Field

        /// <summary>
        /// ��ȡ�ֶ�(����˽��/����/��̬)
        /// </summary>
        /// <param name="obj">����</param>
        /// <param name="fieldName">������</param>
        /// <param name="flags">�󶨱��</param>
        /// <returns>���</returns>
        public static object GetField(this object obj, string fieldName,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
        {
            var fieldInfo = obj.GetType().GetField(fieldName, flags);
            return fieldInfo != null ? fieldInfo.GetValue(obj) : null;
        }

        /// <summary>
        /// �����ֶ�(����˽��/����/��̬)
        /// </summary>
        /// <param name="obj">����</param>
        /// <param name="fieldName">������</param>
        /// <param name="value">ֵ</param>
        /// <param name="flags">�󶨱��</param>
        public static void SetField(this object obj, string fieldName, object value,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
        {
            var fieldInfo = obj.GetType().GetField(fieldName, flags);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(obj, value);
            }
        }

        /// <summary>
        /// ��ȡ����ָ�����Ե��ֶ�(����˽��/����/��̬)
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="obj">����</param>
        /// <param name="flags">�󶨱��</param>
        /// <returns>�ֶ��б�</returns>
        public static List<FieldInfo> GetFieldsWithAttribute<T>(this object obj,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static) where T : Attribute
        {
            var result = new List<FieldInfo>();
            var fields = obj.GetType().GetFields(flags);
            for (var i = 0; i < fields.Length; i++)
            {
                var fieldInfo = fields[i];
                var attribute = fieldInfo.GetCustomAttribute<T>();
                if (attribute != null)
                {
                    result.Add(fieldInfo);
                }
            }

            return result;
        }

        #endregion
    }
}

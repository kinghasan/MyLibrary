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
        /// 转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
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
        /// 转换
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
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
        /// 复制字段（需要类型相同）
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
        /// 转换为 string
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static string AsString(this object obj)
        {
            var result = ReferenceEquals(obj, null) ? null : $"{obj}";
            return result;
        }

        #region Default

        /// <summary>
        /// 获取类型的默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static object DefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// 是否为可空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
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
        /// 获取字段(包含私有/公有/静态)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName">属性名</param>
        /// <param name="flags">绑定标记</param>
        /// <returns>结果</returns>
        public static object GetField(this object obj, string fieldName,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
        {
            var fieldInfo = obj.GetType().GetField(fieldName, flags);
            return fieldInfo != null ? fieldInfo.GetValue(obj) : null;
        }

        /// <summary>
        /// 设置字段(包含私有/公有/静态)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName">属性名</param>
        /// <param name="value">值</param>
        /// <param name="flags">绑定标记</param>
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
        /// 获取包含指定特性的字段(包含私有/公有/静态)
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="flags">绑定标记</param>
        /// <returns>字段列表</returns>
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

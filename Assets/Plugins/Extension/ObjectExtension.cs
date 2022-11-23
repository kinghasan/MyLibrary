using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Aya.Extension
{
    public static class ObjectExtension
    {
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
    }
}

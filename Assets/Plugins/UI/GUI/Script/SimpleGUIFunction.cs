using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

namespace Aya.UI
{
    public static class SimpleGUIFunction
    {
        /// <summary>
        /// 标签
        /// </summary>
        /// <param name="label"></param>
        /// <param name="style"></param>
        /// <param name="tooltip"></param>
        public static void Label(string label, GUIStyle style = null, string tooltip = null)
        {
            using (GUIHorizontal.Create())
            {
                if (style != null)
                    GUILayout.Label(new GUIContent(label, tooltip), style);
                else
                    GUILayout.Label(new GUIContent(label, tooltip));
            }
        }

        /// <summary>
        /// 空行
        /// </summary>
        /// <param name="count"></param>
        public static void Space(int count)
        {
            GUILayout.Space(count);
        }

        /// <summary>
        /// 绘制类中支持的属性字段输入框
        /// </summary>
        /// <param name="self"></param>
        /// <param name="type"></param>
        public static void DrawTypeField(this object self, Type type)
        {
            var propertyInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic).ToList();
            propertyInfos.ForEach((e) =>
            {
                //判断Tips
                var tipsAttribute = e.GetCustomAttribute(typeof(UIAttribute), false);
                var tips = tipsAttribute as UIAttribute;

                if (e.FieldType == typeof(int))
                {
                    var intValue = (int)e.GetValue(self);
                    e.SetValue(self, IntField(e.Name, intValue, tips == null ? null : (string)tips.Value));
                }
                else if (e.FieldType == typeof(float))
                {
                    var floatValue = (float)e.GetValue(self);
                    e.SetValue(self, FloatField(e.Name, floatValue, tips == null ? null : (string)tips.Value));
                }
                else if (e.FieldType == typeof(string))
                {
                    var stringValue = (string)e.GetValue(self);
                    e.SetValue(self, StringField(e.Name, stringValue, tips == null ? null : (string)tips.Value));
                }
            });
        }

        public static List<T> ListField<T>(string title, List<T> list, string tooltip = null) where T : UIList
        {
            var result = list;
            foreach(var value in list)
            {
                var style = new GUIStyle();
                style.fontSize = 25;
                style.alignment = TextAnchor.MiddleCenter;
                style.fontStyle = FontStyle.Bold;
                Label(title, style, tooltip);

                Debug.Log(list.GetType());
                value.DrawTypeField(list.GetType());
            }
            return result;
        }

        /// <summary>
        /// String输入框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        /// <param name="tooltip"></param>
        /// <returns></returns>
        public static string StringField(string title, string value, string tooltip = null)
        {
            var result = value;
            using (GUIHorizontal.Create())
            {
                GUILayout.Label(new GUIContent(title, tooltip));
                result = EditorGUILayout.TextField(result);
            }
            return result;
        }

        /// <summary>
        /// Float输入框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        /// <param name="tooltip"></param>
        /// <returns></returns>
        public static float FloatField(string title, float value,string tooltip = null)
        {
            var result = value;
            using (GUIHorizontal.Create())
            {
                GUILayout.Label(new GUIContent(title, tooltip));
                result = EditorGUILayout.FloatField(result);
            }
            return result;
        }

        /// <summary>
        /// Int输入框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        /// <param name="tooltip"></param>
        /// <returns></returns>
        public static int IntField(string title, int value, string tooltip = null)
        {
            var result = value;
            using (GUIHorizontal.Create())
            {
                GUILayout.Label(new GUIContent(title, tooltip));
                result = EditorGUILayout.IntField(result);
            }
            return result;
        }
    }

    public class UIList
    {

    }

    public class UIAttribute : Attribute
    {
        public object Value;

        public UIAttribute(object value)
        {
            Value = value;
        }
    }
}

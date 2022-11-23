using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Aya.UI;
using System;
using System.Reflection;

namespace Aya.Types
{
    [CustomPropertyDrawer(typeof(TypeReference))]
    public class TypeReferenceInspector : PropertyDrawer
    {
        public bool Enable;
        public TypeReferenceAttribute Attribute;
        public SerializedProperty AssemblyProperty;
        public SerializedProperty TypeProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Attribute == null)
            {
                Attribute = GetAttribute<TypeReferenceAttribute>(property, true);
                Enable = Attribute != null;
                AssemblyProperty = property.FindPropertyRelative("AssemblyName");
                TypeProperty = property.FindPropertyRelative("TypeName");
            }

            if (Attribute != null)
            {
                using (GUIHorizontal.Create())
                {
                    EditorGUILayout.LabelField(property.name);
                    var Root = new SearchableDropdownItem(Attribute.Type.Name);
                    var Drop = new SearchableDropdown(Root, (chooseType) =>
                    {
                        if (chooseType.Value == null)
                        {
                            TypeProperty.stringValue = "";
                            AssemblyProperty.stringValue = "";
                        }
                        else
                        {
                            var type = chooseType.Value as Type;
                            TypeProperty.stringValue = type.Name;
                            AssemblyProperty.stringValue = type.Assembly.FullName;
                        }
                        TypeProperty.serializedObject.ApplyModifiedProperties();
                        AssemblyProperty.serializedObject.ApplyModifiedProperties();
                    });

                    var types = TypeCache.GetTypesDerivedFrom(Attribute.Type);

                    var none = new SearchableDropdownItem("<None>", null);
                    Root.AddChild(none);
                    foreach (var type in types)
                    {
                        var item = new SearchableDropdownItem(type.Name, type);
                        Root.AddChild(item);
                    }

                    var buttonRect = EditorGUILayout.GetControlRect();
                    var btnType = GUI.Button(buttonRect, TypeProperty.stringValue == "" ? "None" : TypeProperty.stringValue, EditorStyles.popup);
                    if (btnType)
                    {
                        position.width = EditorGUIUtility.currentViewWidth;
                        Drop.Show(position);
                    }
                }
            }
        }

        protected T GetAttribute<T>(SerializedProperty serializedProperty, bool inherit) where T : Attribute
        {
            if (serializedProperty == null) { return null; }
            var type = serializedProperty.serializedObject.targetObject.GetType();
            FieldInfo field = null;
            PropertyInfo property = null;
            foreach (var name in serializedProperty.propertyPath.Split('.'))
            {
                field = type.GetField(name);
                if (field == null)
                {
                    property = type.GetProperty(name);
                    if (property == null)
                    {
                        return null;
                    }
                    type = property.PropertyType;
                }
                else
                {
                    type = field.FieldType;
                }
            }

            T[] attributes;

            if (field != null)
            {
                attributes = field.GetCustomAttributes(typeof(T), inherit) as T[];
            }
            else if (property != null)
            {
                attributes = property.GetCustomAttributes(typeof(T), inherit) as T[];
            }
            else
            {
                return null;
            }
            return attributes != null && attributes.Length > 0 ? attributes[0] : null;
        }
    }
}

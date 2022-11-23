using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(SimpleCollider))]
public class SimpleColliderEditor : Editor
{
    public SimpleCollider Target => target as SimpleCollider;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        var type = typeof(SimpleCollider);
        var methods = TypeCache.GetMethodsWithAttribute<SimpleColliderMethod>();
        var list = new List<GUIContent>();
        foreach (var methodInfo in methods)
        {
            var content = new GUIContent(methodInfo.Name);
            list.Add(content);
        }
        EditorGUILayout.LabelField("Choose Method");
        var method = default(MethodInfo);
        if (!string.IsNullOrEmpty(Target.ChooseMethod))
            method = type.GetMethod(Target.ChooseMethod);
        var index = EditorGUILayout.Popup(method == null ? 0 : methods.IndexOf(method), list.ToArray());
        Target.ChooseMethod = methods[index].Name;
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}

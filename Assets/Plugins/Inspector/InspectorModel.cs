using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InspectorTestData))]
public class InspectorModel : Editor
{
    //数据对象 要保存的数据不能用GET SET
    private InspectorTestData _target { get { return target as InspectorTestData; } }
    private string _tips;
    private bool _showTips;
    private GUIStyle _tipStyle = new GUIStyle();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //更新数据
        serializedObject.Update();

        //属性字段-------------
        _target.Name = EditorGUILayout.TextField(_target.Name);

        if (GUILayout.Button("重置"))
        {
            _target.Name = "";
        }
        //--------------------------

        //设为未保存状态
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        //保存数据
        serializedObject.ApplyModifiedProperties();
    }
}

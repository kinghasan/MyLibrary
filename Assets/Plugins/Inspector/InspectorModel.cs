using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InspectorTestData))]
public class InspectorModel : Editor
{
    //���ݶ��� Ҫ��������ݲ�����GET SET
    private InspectorTestData _target { get { return target as InspectorTestData; } }
    private string _tips;
    private bool _showTips;
    private GUIStyle _tipStyle = new GUIStyle();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //��������
        serializedObject.Update();

        //�����ֶ�-------------
        _target.Name = EditorGUILayout.TextField(_target.Name);

        if (GUILayout.Button("����"))
        {
            _target.Name = "";
        }
        //--------------------------

        //��Ϊδ����״̬
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        //��������
        serializedObject.ApplyModifiedProperties();
    }
}

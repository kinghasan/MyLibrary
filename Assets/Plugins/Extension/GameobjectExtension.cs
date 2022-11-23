using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameobjectExtension
{
    /// <summary>
    /// ���ò㼶(��������)
    /// </summary>
    /// <param name="self"></param>
    /// <param name="mask">��NameToLayer��ȡ�㼶</param>
    public static void SetLayers(this GameObject self, LayerMask mask)
    {
        var children = self.GetComponentsInChildren<Transform>();
        foreach(var child in children)
        {
            child.gameObject.layer = mask;
        }
    }

    /// <summary>
    /// ��ȡ����������������
    /// </summary>
    /// <typeparam name="T">�������</typeparam>
    /// <param name="gameObject">����</param>
    /// <returns>���</returns>
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        var component = gameObject.GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }

        return component;
    }

    /// <summary>
    /// ��ȡ����������������
    /// </summary>
    /// <param name="gameObject">����</param>
    /// <param name="type">�������</param>
    /// <returns>���</returns>
    public static Component GetOrAddComponent(this GameObject gameObject, Type type)
    {
        var component = gameObject.GetComponent(type);
        if (component == null)
        {
            component = gameObject.AddComponent(type);
        }

        return component;
    }
}

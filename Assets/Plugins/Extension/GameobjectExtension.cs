using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameobjectExtension
{
    /// <summary>
    /// 设置层级(包括子类)
    /// </summary>
    /// <param name="self"></param>
    /// <param name="mask">用NameToLayer获取层级</param>
    public static void SetLayers(this GameObject self, LayerMask mask)
    {
        var children = self.GetComponentsInChildren<Transform>();
        foreach(var child in children)
        {
            child.gameObject.layer = mask;
        }
    }

    /// <summary>
    /// 获取组件，不存在则添加
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="gameObject">物体</param>
    /// <returns>结果</returns>
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
    /// 获取组件，不存在则添加
    /// </summary>
    /// <param name="gameObject">物体</param>
    /// <param name="type">组件类型</param>
    /// <returns>结果</returns>
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

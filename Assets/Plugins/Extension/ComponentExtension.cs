using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class ComponentExtension
    {
        public static T GetOrAddComponent<T>(this Transform transform) where T : Component
        {
            var component = transform.GetComponent<T>();
            if (component == null)
            {
                component = transform.gameObject.AddComponent<T>();
            }

            return component;
        }

        public static Component GetOrAddComponent(this Transform transform, Type type)
        {
            var component = transform.GetComponent(type);
            if (component == null)
            {
                component = transform.gameObject.AddComponent(type);
            }

            return component;
        }
    }
}

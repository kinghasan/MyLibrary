using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCallBack<T> where T : Collider
{
    public List<ColliderFilter<T>> Filters = new List<ColliderFilter<T>>();

    public void Add(LayerMask layer, Action<T> action)
    {
        var filter = new ColliderFilter<T>()
        {
            Layer = layer,
            Action = action
        };
        Filters.Add(filter);
    }
}
public struct ColliderFilter<T>
{
    public LayerMask Layer;
    public Action<T> Action;
}

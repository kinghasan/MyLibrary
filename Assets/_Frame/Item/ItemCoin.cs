using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : ItemBase<Rigidbody>
{
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public override void OnTargetEffect(Rigidbody target)
    {
        Debug.Log(target.name);
    }
}

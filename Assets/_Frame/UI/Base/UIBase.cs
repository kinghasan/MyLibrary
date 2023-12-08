using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : GameEntity
{
    public virtual void Show(params object[] args)
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}

public abstract class UIBase<T> : UIBase where T : UIBase<T>
{
    public static T Ins { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        Ins = this as T;
    }
}

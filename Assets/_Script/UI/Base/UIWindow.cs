using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : UIBase
{
    public override void Show(params object[] args)
    {
        base.Show(args);
    }

    public override void Hide()
    {
        base.Hide();
    }

    public virtual void Refresh()
    {

    }

    public virtual void Back()
    {
        UI.HideWindow(this);
    }
}

public abstract class UIWindow<T> : UIWindow where T : UIWindow<T>
{
    public static T Ins { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        Ins = this as T;
    }
}

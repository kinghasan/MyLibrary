using Aya.Types;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIWindow : UIBase
{
    [BoxGroup("Next")] public GameState NextState;
    [BoxGroup("Next")] public UINextType ToNextType;
    [BoxGroup("Next"), ShowIf("ToNextType", UINextType.Button)] public Button CloseBtn;
    [BoxGroup("Enter")] public string SwitchCamera;
    [BoxGroup("Enter")] public string SwitchBackground;

    public override void Show(params object[] args)
    {
        base.Show(args);
        if (!string.IsNullOrEmpty(SwitchCamera))
        {
            Camera.Switch(SwitchCamera);
        }
        if (!string.IsNullOrEmpty(SwitchBackground))
        {
            Background.Switch(SwitchBackground);
        }
    }

    public override void Hide()
    {
        base.Hide();
    }

    public virtual void Refresh()
    {

    }

    protected virtual void Update()
    {
        if (!Game.IsGaming) return;

        if (ToNextType == UINextType.Click)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Game.Enter(NextState);
            }
        }
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

public enum UINextType
{
    Button,
    Click,
    Script,
}
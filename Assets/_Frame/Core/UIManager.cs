using System;
using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using UnityEngine;

public class UIManager : GameEntity<UIManager>
{
    public Material GLMaterial;
    public int LockCount = 5;
    //UI字典
    public Dictionary<Type, UIWindow> WindowDic = new Dictionary<Type, UIWindow>();
    //当前打开的UI列表
    public List<UIWindow> WindowStack { get; set; } = new List<UIWindow>();
    //当前窗口
    public UIWindow Current { get; set; }
    public Transform _lockTarget;
    private Mesh _lockTargetMesh;
    private Rect _lockTargetMeshRect => RenderExtension.GetMeshRectInScreen(_lockTargetMesh);

    protected override void Awake()
    {
        base.Awake();
        var windows = GetComponentsInChildren<UIWindow>(true);
        foreach (var window in windows)
        {
            WindowDic.Add(window.GetType(), window);
            window.Hide();
        }
    }

    private void Update()
    {
        if (_lockTarget != null)
        {
            DrawLockRange(_lockTargetMeshRect);
        }
    }

    #region Show
    public void ShowWindow<T>(params object[] args) where T : UIWindow
    {
        if(WindowDic.TryGetValue(typeof(T),out var ui))
            ShowWindow(ui, args);
    }

    public void ShowWindow(Type type,params object[] args)
    {
        if (WindowDic.TryGetValue(type, out var ui))
            ShowWindow(ui, args);
    }

    public void ShowWindow(UIWindow ui,params object[] args)
    {
        if (WindowStack.Contains(ui))
        {
            var uiIndex = WindowStack.IndexOf(ui);
            for (var i = WindowStack.Count - 1; i > uiIndex; i--)
            {
                var coverUi = WindowStack[i];
                HideWindow(coverUi);
            }
        }
        else
        {
            if (WindowStack.Count > 0)
            {
                WindowStack.Last().Hide();
            }

            ui.Show(args);
            WindowStack.Add(ui);
            Current = ui;
        }
    }
    #endregion

    #region Hide
    public void HideWindow<T>() where T : UIWindow
    {
        var type = typeof(T);
        if (WindowDic.TryGetValue(type,out var ui))
            HideWindow(ui);
    }

    public void HideWindow(UIWindow ui)
    {
        if (WindowStack.Contains(ui))
        {
            ui.Hide();
            WindowStack.Remove(ui);
        }
        else return;

        if (WindowStack.Count <= 0) return;
        var lastUi = WindowStack.Last();
        lastUi.Show();
        Current = lastUi;
    }
    #endregion

    #region Target
    public void SetLockTarget(Transform target,Mesh targetMesh = null)
    {
        _lockTarget = target;
        if (targetMesh != null)
            _lockTargetMesh = targetMesh;
        else
            _lockTargetMesh = target.GetComponentInChildren<MeshFilter>().mesh;
    }

    public void DrawLockRange(Rect rect)
    {
        GL.PushMatrix();
        if (!GLMaterial)
            return;
        GLMaterial.SetPass(0);
        GL.LoadPixelMatrix();

        GL.Begin(GL.LINES);
        GL.Color(new Color(1, 0, 0, 1));

        GL.Vertex3(rect.xMin, rect.yMin, 0);
        GL.Vertex3(rect.xMin, rect.yMin + LockCount, 0);
        GL.Vertex3(rect.xMin, rect.yMin, 0);
        GL.Vertex3(rect.xMin + LockCount, rect.yMin, 0);

        GL.Vertex3(rect.xMax, rect.yMin, 0);
        GL.Vertex3(rect.xMax, rect.yMin + LockCount, 0);
        GL.Vertex3(rect.xMax, rect.yMin, 0);
        GL.Vertex3(rect.xMax - LockCount, rect.yMin, 0);

        GL.Vertex3(rect.xMin, rect.yMax, 0);
        GL.Vertex3(rect.xMin, rect.yMax - LockCount, 0);
        GL.Vertex3(rect.xMin, rect.yMax, 0);
        GL.Vertex3(rect.xMin + LockCount, rect.yMax, 0);

        GL.Vertex3(rect.xMax, rect.yMax, 0);
        GL.Vertex3(rect.xMax, rect.yMax - LockCount, 0);
        GL.Vertex3(rect.xMax, rect.yMax, 0);
        GL.Vertex3(rect.xMax - LockCount, rect.yMax, 0);

        GL.End();

        GL.PopMatrix();
    }

    #endregion
}

using Aya.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public abstract class GameEntity : MonoListener
{
    public GameManager Game => GameManager.Ins;
    public CameraManager Camera => CameraManager.Ins;
    public Camera MainCamera => Camera.main;
    public Camera UICamera => UI.MainCamera;
    public UIManager UI => UIManager.Ins;
    public LevelManager Level => LevelManager.Ins;
    public GamePool GamePool => GamePool.Ins;
    public SaveManager Save => SaveManager.Ins;
    public GameTool Tool => GameTool.Ins;
    public GameState GameState => Game.CurrentGameState;
    public ProgramState ProgramState => Game.CurrentProgramState;
    public bool IsGaming => ProgramState == ProgramState.Game;

    protected override void Awake()
    {
        base.Awake();

        //CacheProperty();
    }

    public static Dictionary<Type, List<PropertyInfo>> PropertyDic { get; set; } = new Dictionary<Type, List<PropertyInfo>>();
    public virtual void CacheProperty()
    {
        var type = GetType();
        if (!PropertyDic.TryGetValue(type, out var _))
        {
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic).ToList();
            PropertyDic.Add(type, propertyInfos);
        }
    }

    public Vector3 WorldToUIPosition(Vector3 worldPosition)
    {
        // Canvas Overlay
        // return RectTransformUtility.WorldToScreenPoint(MainCamera, worldPosition);
        // Canvas Camera / World
        var sceneCamera = MainCamera;
        var uiCamera = UICamera;
        var screenPoint = RectTransformUtility.WorldToScreenPoint(sceneCamera, worldPosition);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(UIManager.Ins.GetComponent<RectTransform>(), screenPoint, uiCamera, out var result);
        return result;
    }

    public void Dispatch(object eventType, params object[] args)
    {
        EventManager.Ins.SendDispatch(eventType, args);
    }
}

public abstract class GameEntity<T> : GameEntity where T : GameEntity<T>
{
    public static T Ins { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        Ins = this as T;
    }
}

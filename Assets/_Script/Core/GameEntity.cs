using Aya.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public abstract class GameEntity : MonoListener
{
    public GameManager Game => GameManager.Ins;
    public CameraManager Camera => CameraManager.Ins;
    public UIManager UI => UIManager.Ins;
    public LevelManager Level => LevelManager.Ins;
    public GamePool GamePool => GamePool.Ins;
    public GameTool Tool => GameTool.Ins;
    public GameState State => Game.CurrentState;

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

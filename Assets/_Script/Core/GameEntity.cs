using Aya.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public BackgroundManager Background => BackgroundManager.Ins;
    public GameTool Tool => GameTool.Ins;
    public UpgradeManager Upgrade => UpgradeManager.Ins;

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

    /// <summary>
    /// 世界坐标转UI坐标(默认Main摄像机)
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 判断是否点击在UI上，真机适用
    /// </summary>
    /// <returns></returns>
    public bool IsPointerOverGameObject()
    {
        PointerEventData eventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;
        List<RaycastResult> list = new List<RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, list);
        return list.Count > 0;
    }

    #region Transform

    [NonSerialized] public Transform Trans;

    public Vector3 Forward
    {
        get => Trans.forward;
        set => Trans.forward = value;
    }

    public Vector3 Backward
    {
        get => -Trans.forward;
        set => Trans.forward = -value;
    }

    public Vector3 Right
    {
        get => Trans.right;
        set => Trans.right = value;
    }

    public Vector3 Left
    {
        get => -Trans.right;
        set => Trans.right = -value;
    }

    public Vector3 Up
    {
        get => Trans.up;
        set => Trans.up = value;
    }

    public Vector3 Down
    {
        get => -Trans.up;
        set => Trans.up = -value;
    }

    public Vector3 Position
    {
        get => Trans.position;
        set => Trans.position = value;
    }

    public Vector3 LocalPosition
    {
        get => Trans.localPosition;
        set => Trans.localPosition = value;
    }

    public Quaternion Rotation
    {
        get => Trans.rotation;
        set => Trans.rotation = value;
    }

    public Quaternion LocalRotation
    {
        get => Trans.localRotation;
        set => Trans.localRotation = value;
    }

    public Vector3 EulerAngles
    {
        get => Trans.eulerAngles;
        set => Trans.eulerAngles = value;
    }

    public Vector3 LocalEulerAngles
    {
        get => Trans.localEulerAngles;
        set => Trans.localEulerAngles = value;
    }

    public Vector3 LocalScale
    {
        get => Trans.localScale;
        set => Trans.localScale = value;
    }

    public float LocalScaleValue
    {
        set => Trans.localScale = Vector3.one * value;
    }

    #endregion

    #region Parent

    public Transform Parent
    {
        get => Trans.parent;
        set
        {
            if (!gameObject.activeInHierarchy) return;
            Trans.parent = value;
        }
    }

    //public void SetParentToLevel()
    //{
    //    Parent = CurrentLevel.Trans;
    //}

    public void ClearParent()
    {
        Parent = null;
    }

    #endregion
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

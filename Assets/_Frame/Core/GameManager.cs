using UnityEngine;
using Aya.SimpleAction;
using Aya.Events;
using System;
using System.Collections.Generic;
using Aya.Extension;
using Aya.Types;

public class GameManager : GameEntity<GameManager>
{
    [TypeReference(typeof(GameState))] public TypeReference LaunchPhase;
    public Transform Handler;
    public GameState CurrentGameState { get; set; }
    public MaterialPropertyBlock SimpleBlock { get; set; }

    [NonSerialized] public Dictionary<GamePhaseType, GameState> PhaseDic;
    [NonSerialized] public Dictionary<Type, GameState> PhaseTypeDic;
    [NonSerialized] public List<GameState> PhaseList;

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 1f;
        SimpleBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
#if !SuperSonic
        StartGame();
#else
        UI.ShowWindow<UILoading>();
#endif
    }

    public virtual void StartGame()
    {
        Background.Init();

        PhaseList = Handler.GetComponents<GameState>().ToList();
        PhaseDic = PhaseList.ToDictionary(p => p.Type);
        PhaseTypeDic = PhaseList.ToDictionary(p => p.GetType());

        Level.LevelStart();
    }

    /// <summary>
    /// 初始化插件，全局参数等内容
    /// </summary>
    public void Init()
    {
        SDKTool.Init();
    }

    public T Get<T>() where T : GameState
    {
        var type = typeof(T);
        return Get(type) as T;
    }

    public GameState Get(Type type)
    {
        if (PhaseTypeDic.TryGetValue(type, out var handler)) return handler;
        handler = Handler.GetOrAddComponent(type) as GameState;
        if (handler == null) return default;
        PhaseTypeDic.Add(type, handler);
        PhaseDic.Add(handler.Type, handler);
        return handler;
    }

    public void Enter<T>(params object[] args) where T : GameState
    {
        Enter(typeof(T), args);
    }

    public void Enter(Type phaseType, params object[] args)
    {
        var nextPhase = Get(phaseType);
        Enter(nextPhase, args);
    }

    public void Enter(GamePhaseType gamePhaseType, params object[] args)
    {
        var nextPhase = PhaseDic[gamePhaseType];
        Enter(nextPhase, args);
    }
    public void Enter(GameState nextState, params object[] args)
    {
        if (CurrentGameState != null) CurrentGameState.Exit();
        nextState.Enter(args);
        CurrentGameState = nextState;
    }

    #region Dispatch GameState
    [Listen(GamePhaseType.Ready)]
    public void Ready()
    {
        if (UI == null) return;
        UI.ShowWindow<UIReady>();
    }

    [Listen(GamePhaseType.Gaming)]
    public void PlayGame()
    {
        UI.ShowWindow<UIGame>();
        var info = Upgrade.GetInfo<TestData>();
    }

    [Listen(GamePhaseType.Pause)]
    public void Wait()
    {
        UI.ShowWindow<UIWait>();
    }

    [Listen(GamePhaseType.Win)]
    public void Win()
    {
        UI.ShowWindow<UIWin>();
    }

    [Listen(GamePhaseType.Lose)]
    public void Lose()
    {
        UI.ShowWindow<UILose>();
    }
    #endregion
}

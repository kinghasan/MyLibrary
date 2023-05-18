using UnityEngine;
using Aya.SimpleAction;
using Aya.Events;

public class GameManager : GameEntity<GameManager>
{
    public GameState CurrentGameState { get; set; }
    public ProgramState CurrentProgramState { get; set; }
    public MaterialPropertyBlock SimpleBlock { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 1f;
        SimpleBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        CurrentProgramState = ProgramState.Game;
        Background.Init();
        Level.Init();
    }

    public void Enter(GameState state)
    {
        CurrentGameState = state;
        Dispatch(state);
    }

    #region Dispatch GameState
    [Listen(GameState.Ready)]
    public void Ready()
    {
        if (UI == null) return;
        UI.ShowWindow<UIReady>();
    }

    [Listen(GameState.Game)]
    public void StartGame()
    {
        UI.ShowWindow<UIGame>();
        var info = Upgrade.GetInfo<TestData>();
    }

    [Listen(GameState.Wait)]
    public void Wait()
    {
        UI.ShowWindow<UIWait>();
    }

    [Listen(GameState.Win)]
    public void Win()
    {
        UI.ShowWindow<UIWin>();
    }

    [Listen(GameState.Lose)]
    public void Lose()
    {
        UI.ShowWindow<UILose>();
    }
    #endregion
}

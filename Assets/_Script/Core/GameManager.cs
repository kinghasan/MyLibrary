using UnityEngine;
using Aya.SimpleAction;
using Aya.Events;

public class GameManager : GameEntity<GameManager>
{
    public GameState CurrentGameState { get; set; }
    public ProgramState CurrentProgramState { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 1f;
        BackgroundInit();
    }

    private void Start()
    {
        CurrentProgramState = ProgramState.Game;
        Level.Init();
    }

    public void Enter(GameState state)
    {
        CurrentGameState = state;
        Dispatch(state);
    }

    #region Dispatch
    [Listen(GameState.Ready)]
    public void Ready()
    {
        if (UI == null) return;
        UI.ShowWindow<UIReady>();
        Debug.Log("Ready");
    }

    [Listen(GameState.Game)]
    public void StartGame()
    {
        UI.ShowWindow<UIGame>();
        Debug.Log("Game");
    }

    [Listen(GameState.Wait)]
    public void Wait()
    {
        UI.ShowWindow<UIWait>();
        Debug.Log("Wait");
    }

    [Listen(GameState.Win)]
    public void Win()
    {
        UI.ShowWindow<UIWin>();
        Debug.Log("Win");
    }

    [Listen(GameState.Lose)]
    public void Lose()
    {
        UI.ShowWindow<UILose>();
        Debug.Log("Lose");
    }
    #endregion

    #region Background
    public Transform BackgroundTrans { get; set; }
    public Transform CurrentBackground { get; set; }

    /// <summary>
    /// ±³¾°¹¦ÄÜ³õÊ¼»¯
    /// </summary>
    public void BackgroundInit()
    {
        BackgroundTrans = transform.Find("Background");
        for (var i = 0; i < BackgroundTrans.childCount; i++)
        {
            var child = BackgroundTrans.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ÇÐ»»±³¾°
    /// </summary>
    /// <param name="name"></param>
    public void SwitchBackground(string name)
    {
        var background = BackgroundTrans.Find(name);
        if (background == null) return;

        if (background != CurrentBackground && CurrentBackground != null)
            CurrentBackground.gameObject.SetActive(false);
        background.gameObject.SetActive(true);
    }
    #endregion
}

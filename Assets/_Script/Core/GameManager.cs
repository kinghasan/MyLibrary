using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.SimpleAction;
using Aya.Events;

[RequireComponent(typeof(SimpleActionManager))]
[RequireComponent(typeof(SaveManager))]
[RequireComponent(typeof(GamePool))]
[RequireComponent(typeof(EventManager))]
public class GameManager : GameEntity<GameManager>
{
    public GameState CurrentState { get; set; }

    private void Start()
    {
        Enter(GameState.Ready);
    }

    public void Enter(GameState state)
    {
        CurrentState = state;
        Dispatch(state);
    }

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
}

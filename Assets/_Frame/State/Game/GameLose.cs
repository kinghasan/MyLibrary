using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLose : GameState
{
    public override GamePhaseType Type => GamePhaseType.Lose;

    public override void Enter(params object[] args)
    {
        base.Enter(args);
        UI.ShowWindow<UILose>();
    }
}

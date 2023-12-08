using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReady : GameState
{
    public override GamePhaseType Type => GamePhaseType.Ready;

    public override void Enter(params object[] args)
    {
        base.Enter(args);
        UI.ShowWindow<UIReady>();
    }
}

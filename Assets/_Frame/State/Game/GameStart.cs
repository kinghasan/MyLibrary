using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : GameState
{
    public override GamePhaseType Type => GamePhaseType.Gaming;

    public override void Enter(params object[] args)
    {
        base.Enter(args);
        UI.ShowWindow<UIGame>();
    }
}

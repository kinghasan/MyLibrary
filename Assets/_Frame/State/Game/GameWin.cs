using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : GameState
{
    public override GamePhaseType Type => GamePhaseType.Win;

    public override void Enter(params object[] args)
    {
        base.Enter(args);
        UI.ShowWindow<UIWin>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReady : UIWindow<UIReady>
{
    public void GameStart()
    {
        Game.Enter(GameState.Game);
    }
}

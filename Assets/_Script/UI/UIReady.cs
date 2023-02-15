using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.UI;

public class UIReady : UIWindow<UIReady>
{
    public GameObject ReadyArea;

    protected override void Awake()
    {
        base.Awake();
        UIEventListener.Get(ReadyArea).onClick += (uiGameObject, data) =>
        {
            GameStart();
        };
    }

    public void GameStart()
    {
        Game.Enter(GameState.Game);
    }
}

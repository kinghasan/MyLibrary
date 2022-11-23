using Aya.Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : GameEntity<SaveManager>
{
    public SInt LevelIndex;
    public SInt ExampleLevel;

    protected override void Awake()
    {
        base.Awake();
        LevelIndex = new SInt(nameof(LevelIndex), 1);
        ExampleLevel = new SInt(nameof(LevelIndex), 1);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UpgradeStep : StepBase<UpgradeData>
{
    protected override void Awake()
    {
        base.Awake();
        Debug.Log(1<< 2);
    }

    [Button("NextStep")]
    public void Next()
    {
        StepComplete();
        Debug.Log(CurrentStep.Step);
    }
}

[Serializable]
public class UpgradeData : StepData
{
    [Tooltip("½ðÇ®")]public float Money;
}

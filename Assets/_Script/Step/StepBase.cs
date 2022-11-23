using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class StepBase<T> : GameEntity where T : StepData
{
    public List<T> StepList;

    public T CurrentStep { get; set; }
    public int StepIndex { get; set; }
    public bool IsMaxLevel => StepIndex == StepList.Count - 1;

    private void Start()
    {
        RefreshStep();
    }

    public virtual void RefreshStep()
    {
        CurrentStep = StepList[StepIndex];
    }

    public virtual void StepComplete()
    {
        if (!IsMaxLevel)
            StepIndex++;
        RefreshStep();
    }
}

[Serializable]
public class StepData
{
    //½×¶Î
    [Title("½×¶ÎÊý¾Ý")]
    [Tooltip("½×¶Î")] public int Step;
}

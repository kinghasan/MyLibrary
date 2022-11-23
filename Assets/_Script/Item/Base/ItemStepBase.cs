using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStepBase<T> : ItemBase where T : StepData
{
    public List<T> StepList;

    public T CurrentStep { get; set; }
    public int StepIndex { get; set; }
    public bool IsMaxLevel => StepIndex == StepList.Count - 1;

    public override void Init()
    {
        base.Init();
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

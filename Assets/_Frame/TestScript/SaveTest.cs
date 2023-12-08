using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Aya.Save;

public class SaveTest : MonoBehaviour
{
    public int Value;
    private SInt SaveInt = new SInt(nameof(SaveInt), 0);
    private SBool SaveBool = new SBool(nameof(SaveBool), false);

    private void Awake()
    {
        Value = SaveInt.Value;
    }

    [Button("Debug")]
    public void DebugValue()
    {
        if (Application.isPlaying)
            Debug.Log(SaveBool);
    }

    [Button("Bool")]
    public void BoolRest()
    {
        if (Application.isPlaying)
            SaveBool.Value = !SaveBool.Value;
    }

    [Button("+1")]
    public void AddValue()
    {
        if (Application.isPlaying)
        {
            Value++;
            SaveInt.Value = Value;
        }
    }

    [Button("-1")]
    public void RemoveValue()
    {
        if (Application.isPlaying)
        {
            Value--;
            SaveInt.Value = Value;
        }
    }
}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.Events;

public class EventTester : GameEntity
{
    public TestList Type;

    [Listen(TestList.Test1)]
    public void ShowTest1()
    {
        Debug.Log("Test1");
    }

    [Listen(TestList.Test2)]
    public void ShowTest2()
    {
        Debug.Log("Test2");
    }

    [Listen(TestList.Test3)]
    public void ShowTest3()
    {
        Debug.Log("Test3");
    }

    [Button("SendEvent")]
    public void SendEunm()
    {
        Dispatch(Type, null);
    }
}

public enum TestList
{
    Test1,
    Test2,
    Test3
}

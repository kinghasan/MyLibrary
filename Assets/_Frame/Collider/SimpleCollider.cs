using System;
using UnityEngine;

public class SimpleCollider : GameEntity
{
    public string ChooseMethod;

    private void OnTriggerEnter(Collider other)
    {
        var method = typeof(SimpleCollider).GetMethod(ChooseMethod);
        method?.Invoke(this, new object[] { other });
    }

    [SimpleColliderMethod]
    public void ShowName(Collider other)
    {
        Debug.Log(other.name);
    }

    [SimpleColliderMethod]
    public void ShowTime(Collider other)
    {
        Debug.Log(Time.realtimeSinceStartup);
    }
}

//方法需要用Public
public class SimpleColliderMethod : Attribute
{

}

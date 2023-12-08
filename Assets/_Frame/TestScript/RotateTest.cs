using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Aya.Extension;
using Aya.Types;
using Aya.SimpleAction;

public class RotateTest : GameEntity
{
    public float RotateY;
    [TypeReference(typeof(ABSS))] public TypeReference Type;
    public Vector3 V1;
    public Vector3 V2;

    [Button("Rotate")]
    public void Rotate()
    {
        //transform.SetRotateY(RotateY);
        //transform.RotateAround(Vector3.up, Vector3.forward, 5);
        transform.RotateWithTarget(Vector3.up * 2, Vector3.forward, 90);
    }

    [Button("ShowCross")]
    public void ShowCross()
    {
        Debug.Log(transform.forward);
        Debug.Log(Vector3.Cross(transform.forward, V2));
        SimpleActionFunction.SimpleValue(0f, 1f, 2f, v =>
        {
            transform.RotateY(Vector3.Cross(transform.forward, V2).y * 5f);
        });
    }

    [Button("ShowMar2D")]
    public void ShowMar2D()
    {
        var v2 = (Vector2)transform.position;
        Debug.Log(v2.magnitude + ":" + GetMar2D());
    }

    [Button("ShowMar")]
    public void ShowMar()
    {
        Debug.Log(transform.position.magnitude + ":" + GetMar());
    }

    public float GetMar()
    {
        var x = transform.position.x * transform.position.x;
        var y = transform.position.y * transform.position.y;
        var z = transform.position.z * transform.position.z;
        var xyz = x + y + z;
        return Mathf.Sqrt(xyz);
    }

    public float GetMar2D()
    {
        var x = transform.position.x * transform.position.x;
        var y = transform.position.y * transform.position.y;
        var xy = x + y;
        return Mathf.Sqrt(xy);
    }

    public class ABSS
    {

    }

    public class ADST : ABSS
    {

    }
}

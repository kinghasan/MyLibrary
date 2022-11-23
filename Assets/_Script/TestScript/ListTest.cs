using Aya.TweenPro;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.SimpleAction;
using Aya.Extension;

public class ListTest : GameEntity
{
    public Transform Target;
    public Transform PointPrefab;
    public List<Transform> TransList;
    public Vector2 PointSize;

    protected override void Awake()
    {
        base.Awake();

        var positions = new Vector2[TransList.Count];
        for(var i = 0; i < TransList.Count; i++)
        {
            positions[i] = TransList[i].position;
        }
        var list = MathUtilExtension.GetPointsInPolygon(positions, PointSize);
        foreach(var point in list)
        {
            var obj = Instantiate(PointPrefab);
            obj.transform.position = point;
        }

        //GrassManager.Ins.SpawnGrass(positions, 100);

        //var sort = SortExtension.SortVector2(4, 4, Vector3.one);

        //SimpleActionFunction.SimpleValue(0, 2f, 5f, v =>
        //{
        //    //Debug.Log(v);
        //});

        //SimpleActionFunction.SimpleRotation(transform, QuaternionExtension.Left, 5f);
    }

    [Button("Click")]
    public void Click()
    {
        SimpleActionFunction.SimpleScale(Target, 10f, 5f);
        //this.ExecuteDelay(() =>
        //{
        //    SimpleActionFunction.SimpleParabola(Target, Target.position, transform.position, Height, 1f);
        //}, 1f);
    }
}

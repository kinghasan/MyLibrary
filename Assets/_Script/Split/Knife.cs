using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : GameEntity
{
    private float _lastTime;

    private void Start()
    {
        foreach(var mesh in GetComponent<MeshFilter>().mesh.normals)
        {
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.realtimeSinceStartup - _lastTime < 2f) return;
        var able = other.GetComponent<MeshDestroyAble>();
        if (able != null)
        {
            _lastTime = Time.realtimeSinceStartup;
            var plane = new Plane(transform.GetComponent<MeshFilter>().mesh.normals[0], transform.position);
            MeshDestroyMachine.Ins.SliceMesh(plane, able);
        }
    }
}

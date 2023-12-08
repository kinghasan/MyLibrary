using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.Physical;
using Aya.Extension;

/// <summary>
/// 和草地交互，组件放置于移动物体身上并添加Collider组件
/// </summary>
public class GrassInteraction : GameEntity
{
    public LayerMask GrassLayer;
    public float LerpSpeed;

    protected override void Awake()
    {
        base.Awake();
        var listener = gameObject.AddComponent<ColliderListener>();
        listener.onTriggerStay.Add(GrassLayer, EnterGrass);
        listener.onTriggerExit.Add(GrassLayer, ExitGrass);
    }

    public void EnterGrass(Collider other)
    {
        var mesh = other.transform.GetComponentInChildren<MeshRenderer>();
        if (FlowerList.Contains(other.transform))
            FlowerList.Remove(other.transform);
        var selfPos = transform.position;
        var grassPos = mesh.transform.position;
        var direction = (grassPos - selfPos).normalized;
        mesh.transform.forward = Vector3.Lerp(mesh.transform.forward, direction, LerpSpeed);
    }

    public void ExitGrass(Collider other)
    {
        var mesh = other.transform.GetComponentInChildren<MeshRenderer>();
        FlowerList.Add(mesh.transform);
    }

    private List<Transform> FlowerList = new List<Transform>();
    private void Update()
    {
        for (var i = FlowerList.Count - 1; i >= 0; i--)
        {
            var flower = FlowerList[i];
            flower.SetLocalRotationX(Mathf.Lerp(flower.localRotation.x, 0, LerpSpeed));
            flower.SetLocalRotationY(Mathf.Lerp(flower.localRotation.y, 0, LerpSpeed));
            flower.SetLocalRotationZ(Mathf.Lerp(flower.localRotation.z, 0, LerpSpeed));
            if (flower.localRotation == Quaternion.identity)
                FlowerList.Remove(flower);
        }
    }
}

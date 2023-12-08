using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : GameEntity
{
    [Tooltip("黑洞物理模型")]
    public MeshFilter MeshFilter;
    [Tooltip("黑洞物理组件")]
    public MeshCollider MeshCollider;
    [Tooltip("黑洞显示")]
    public MeshRenderer Plane;
    public float StartedDistance = 1f;
    public float HoleSize = 1f;
    public bool UseMethod;
    private Mesh _mesh;
    public List<int> VerticesList { get; set; }
    public List<Vector3> Offset { get; set; }

    protected override void Awake()
    {
        base.Awake();
        if (!UseMethod)
            Init();
    }

    public void Init()
    {
        if (VerticesList != null) return;
        VerticesList = new List<int>();
        Offset = new List<Vector3>();
        _mesh = MeshFilter.mesh;
        for (var i = 0; i < _mesh.vertices.Length; i++)
        {
            var distance = Vector3.Distance(transform.position, _mesh.vertices[i]);
            if (distance < StartedDistance)
            {
                VerticesList.Add(i);
                Offset.Add(_mesh.vertices[i] - transform.position);
            }
        }

        var vertices = _mesh.vertices;
        for (var i = 0; i < VerticesList.Count; i++)
        {
            vertices[VerticesList[i]] = transform.position + Offset[i] * HoleSize;
        }
        _mesh.vertices = vertices;

        MeshFilter.mesh = _mesh;
        MeshCollider.sharedMesh = _mesh;
        Plane.material.SetFloat("_Radius", HoleSize / 2f);
        Plane.material.SetVector("_CirclePosition", transform.position);
    }

    public void UpdateVertices(Vector3 range)
    {
        var vertices = _mesh.vertices;
        for (var i = 0; i < VerticesList.Count; i++)
        {
            vertices[VerticesList[i]] = transform.position + (Offset[i] + range) * HoleSize;
        }
        _mesh.vertices = vertices;

        MeshFilter.mesh = _mesh;
        MeshCollider.sharedMesh = _mesh;

        Plane.material.SetFloat("_Radius", HoleSize / 2f);
        Plane.material.SetVector("_CirclePosition", transform.position);
    }
}

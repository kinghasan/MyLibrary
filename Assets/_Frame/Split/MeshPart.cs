using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshPart
{
    private List<Vector3> _Verticies = new List<Vector3>();
    private List<Vector3> _Normals = new List<Vector3>();
    private List<List<int>> _Triangles = new List<List<int>>();
    private List<Vector2> _UVs = new List<Vector2>();

    public Vector3[] Vertices;
    public Vector3[] Normals;
    public int[][] Triangles;
    public Vector2[] UV;

    public GameObject go;
    public Rigidbody rb;
    public Bounds Bounds = new Bounds();

    public MeshPart()
    {

    }

    public void AddTriangle(int submesh,
        Vector3 vert1, Vector3 vert2, Vector3 vert3,
        Vector3 normal1, Vector3 normal2, Vector3 normal3,
        Vector2 uv1, Vector2 uv2, Vector2 uv3)
    {
        if (_Triangles.Count - 1 < submesh)
            _Triangles.Add(new List<int>());

        _Triangles[submesh].Add(_Verticies.Count);
        _Verticies.Add(vert1);
        _Triangles[submesh].Add(_Verticies.Count);
        _Verticies.Add(vert2);
        _Triangles[submesh].Add(_Verticies.Count);
        _Verticies.Add(vert3);
        _Normals.Add(normal1);
        _Normals.Add(normal2);
        _Normals.Add(normal3);
        _UVs.Add(uv1);
        _UVs.Add(uv2);
        _UVs.Add(uv3);

        Bounds.min = Vector3.Min(Bounds.min, vert1);
        Bounds.min = Vector3.Min(Bounds.min, vert2);
        Bounds.min = Vector3.Min(Bounds.min, vert3);
        Bounds.max = Vector3.Min(Bounds.max, vert1);
        Bounds.max = Vector3.Min(Bounds.max, vert2);
        Bounds.max = Vector3.Min(Bounds.max, vert3);
    }

    public void FillArrays()
    {
        Vertices = _Verticies.ToArray();
        Normals = _Normals.ToArray();
        UV = _UVs.ToArray();
        Triangles = new int[_Triangles.Count][];
        for (var i = 0; i < _Triangles.Count; i++)
            Triangles[i] = _Triangles[i].ToArray();
    }

    public void MakeGameobject(MeshDestroyAble original)
    {
        go = new GameObject(original.name);
        go.transform.position = original.transform.position;
        go.transform.rotation = original.transform.rotation;
        go.transform.localScale = original.transform.localScale;

        var mesh = new Mesh();
        mesh.name = original.OrigionMesh.name;
        mesh.vertices = Vertices;
        mesh.normals = Normals;
        mesh.uv = UV;
        for (var i = 0; i < Triangles.Length; i++)
            mesh.SetTriangles(Triangles[i], i, true);
        Bounds = mesh.bounds;

        var renderer = go.AddComponent<MeshRenderer>();
        renderer.materials = original.meshRenderer.materials;
        var filter = go.AddComponent<MeshFilter>();
        filter.mesh = mesh;
        var collider = go.AddComponent<MeshCollider>();
        collider.convex = true;
        rb = go.AddComponent<Rigidbody>();
        var meshDestroy = go.AddComponent<MeshDestroyAble>();
        meshDestroy.Init(filter, renderer, original.cutCascades, original.explodeForce);
    }
}


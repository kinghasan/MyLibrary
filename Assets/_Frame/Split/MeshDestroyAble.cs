using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDestroyAble : GameEntity
{
    [Header("Render")]
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public Mesh CombineMesh;

    [Header("Destroy Settings")]
    public int cutCascades = 1;
    public float explodeForce = 0;

    public Rect MeshRectInScreen
    {
        get
        {
            return GetMeshRectInScreen(OrigionMesh);
        }
    }

    public Mesh OrigionMesh
    {
        get
        {
            if (meshFilter == null)
            {
                meshFilter = GetComponentInChildren<MeshFilter>();
                meshRenderer = GetComponentInChildren<MeshRenderer>();
                CombineInstance combieInstance;
                combieInstance = new CombineInstance { mesh = meshFilter.mesh, transform = meshRenderer.transform.localToWorldMatrix, subMeshIndex = 0 };
                Mesh mesh = new Mesh();
                mesh.CombineMeshes(new CombineInstance[] { combieInstance }, true, true);
                CombineMesh = mesh;
            }

            return meshFilter.mesh;
        }
    }

    private Rect GetMeshRectInScreen(Mesh mesh)
    {
        Rect rect = new Rect();
        Vector3[] worldVertex = GetMeshWorldVertex(mesh);
        if (worldVertex != null)
        {
            float xMin = float.MaxValue, xMax = float.MinValue, yMin = float.MaxValue, yMax = float.MinValue;
            for (var i = 0; i < worldVertex.Length; i++)
            {
                Vector3 screenVertex = Camera.main.WorldToScreenPoint(worldVertex[i]);
                if (screenVertex.x < xMin)
                    xMin = screenVertex.x;
                if (screenVertex.x > xMax)
                    xMax = screenVertex.x;
                if (screenVertex.y < yMin)
                    yMin = screenVertex.y;
                if (screenVertex.y > yMax)
                    yMax = screenVertex.y;
            }
            rect = Rect.MinMaxRect(xMin, yMin, xMax, yMax);
        }
        return rect;
    }

    private Vector3[] GetMeshWorldVertex(Mesh mesh)
    {
        if (mesh != null)
        {
            Vector3 center = mesh.bounds.center;
            Vector3 size = mesh.bounds.size / 2;
            Vector3[] worldVertex = new Vector3[]
            {
                center - size,
                center + new Vector3(-size.x,size.y,size.z),
                center + new Vector3(size.x,-size.y,size.z),
                center + new Vector3(size.x,size.y,-size.z),
                center + new Vector3(-size.x,-size.y,size.z),
                center + new Vector3(size.x,-size.y,-size.z),
                center + new Vector3(-size.x,size.y,-size.z),
                center + size,
            };
            return worldVertex;
        }
        return null;
    }

    public void Init(MeshFilter meshFilter, MeshRenderer meshRenderer, int cutCascades, float explodeForce)
    {
        this.meshFilter = meshFilter;
        this.meshRenderer = meshRenderer;
        this.cutCascades = cutCascades;
        this.explodeForce = explodeForce;
    }
}


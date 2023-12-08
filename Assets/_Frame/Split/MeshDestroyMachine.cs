using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDestroyMachine : GameEntity<MeshDestroyMachine>
{
    private bool edgeSet = false;
    private Vector3 edgeVertex = Vector3.zero;
    private Vector2 edgeUV = Vector2.zero;
    private Plane edgePlane = new Plane();

    /// <summary>
    /// 爆炸的切割
    /// </summary>
    public List<MeshPart> DestroyMesh(MeshDestroyAble destroyAble)
    {
        Mesh originalMesh = destroyAble.OrigionMesh;
        if (originalMesh == null) { return null; }
        originalMesh.RecalculateBounds();
        List<MeshPart> parts = new List<MeshPart>();
        List<MeshPart> subParts = new List<MeshPart>();

        var mainPart = new MeshPart()
        {
            UV = originalMesh.uv,
            Vertices = originalMesh.vertices,
            Normals = originalMesh.normals,
            Triangles = new int[originalMesh.subMeshCount][],
            Bounds = originalMesh.bounds
        };
        for (int i = 0; i < originalMesh.subMeshCount; i++)
            mainPart.Triangles[i] = originalMesh.GetTriangles(i);

        parts.Add(mainPart);

        for (int c = 0; c < destroyAble.cutCascades; c++)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                Bounds bounds = parts[i].Bounds;
                bounds.Expand(0.5f);

                Plane plane = new Plane(UnityEngine.Random.onUnitSphere,
                    new Vector3(UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                    UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
                    UnityEngine.Random.Range(bounds.min.z, bounds.max.z)));

                subParts.Add(GenerateMesh(parts[i], plane, true));
                subParts.Add(GenerateMesh(parts[i], plane, false));
            }
            parts = new List<MeshPart>(subParts);
            subParts.Clear();
        }

        for (var i = 0; i < parts.Count; i++)
        {
            parts[i].MakeGameobject(destroyAble);
            parts[i].rb.AddForceAtPosition(parts[i].Bounds.center * destroyAble.explodeForce, transform.position);
        }

        Destroy(destroyAble.gameObject);

        return parts;
    }

    /// <summary>
    /// 刀剑的切割
    /// </summary>
    public List<MeshPart> SliceMesh(Plane plane, MeshDestroyAble destroyAble)
    {
        Mesh originalMesh = destroyAble.OrigionMesh;
        if (originalMesh == null) { return null; }
        originalMesh.RecalculateBounds();
        var mainPart = new MeshPart()
        {
            UV = originalMesh.uv,
            Vertices = originalMesh.vertices,
            Normals = originalMesh.normals,
            Triangles = new int[originalMesh.subMeshCount][],
            Bounds = originalMesh.bounds
        };
        for (int i = 0; i < originalMesh.subMeshCount; i++)
            mainPart.Triangles[i] = originalMesh.GetTriangles(i);

        List<MeshPart> parts = new List<MeshPart>();
        parts.Add(GenerateMesh(mainPart, plane, true));
        parts.Add(GenerateMesh(mainPart, plane, false));

        for (var i = 0; i < parts.Count; i++)
        {
            parts[i].MakeGameobject(destroyAble);
        }

        Destroy(destroyAble.gameObject);

        return parts;
    }

    /// <summary>
    /// 用平面切网格
    /// </summary>
    private MeshPart GenerateMesh(MeshPart original, Plane plane, bool left)
    {
        var partMesh = new MeshPart() { };
        var ray1 = new Ray();
        var ray2 = new Ray();

        for (var i = 0; i < original.Triangles.Length; i++)
        {
            var triangles = original.Triangles[i];
            edgeSet = false;

            for (var j = 0; j < triangles.Length; j = j + 3)
            {
                //判断是否被切割
                var sideA = plane.GetSide(original.Vertices[triangles[j]]) == left;
                var sideB = plane.GetSide(original.Vertices[triangles[j + 1]]) == left;
                var sideC = plane.GetSide(original.Vertices[triangles[j + 2]]) == left;

                var sideCount = (sideA ? 1 : 0) + (sideB ? 1 : 0) + (sideC ? 1 : 0);
                if (sideCount == 0)
                {
                    continue;
                }
                if (sideCount == 3)
                {
                    partMesh.AddTriangle(i,
                        original.Vertices[triangles[j]],
                        original.Vertices[triangles[j + 1]],
                        original.Vertices[triangles[j + 2]],
                        original.Normals[triangles[j]],
                        original.Normals[triangles[j + 1]],
                        original.Normals[triangles[j + 2]],
                        original.UV[triangles[j]],
                        original.UV[triangles[j + 1]],
                        original.UV[triangles[j + 2]]);
                    continue;
                }

                //对于剖面
                //判断三角形中单独的点的是哪一个
                var singleIndex = sideB == sideC ? 0 : sideA == sideC ? 1 : 2;

                //求切割点的位置
                ray1.origin = original.Vertices[triangles[j + singleIndex]];
                var dir1 = original.Vertices[triangles[j + ((singleIndex + 1) % 3)]] - original.Vertices[triangles[j + singleIndex]];
                ray1.direction = dir1;
                plane.Raycast(ray1, out var enter1);
                var lerp1 = enter1 / dir1.magnitude;

                //求切割点的位置
                ray2.origin = original.Vertices[triangles[j + singleIndex]];
                var dir2 = original.Vertices[triangles[j + ((singleIndex + 2) % 3)]] - original.Vertices[triangles[j + singleIndex]];
                ray2.direction = dir2;
                plane.Raycast(ray2, out var enter2);
                var lerp2 = enter2 / dir2.magnitude;

                //将两个切割点加入子网格
                AddEdge(i, partMesh, left ? plane.normal * -1f : plane.normal,
                        ray1.origin + ray1.direction.normalized * enter1,
                        ray2.origin + ray2.direction.normalized * enter2,
                        Vector2.Lerp(original.UV[triangles[j + singleIndex]], original.UV[triangles[j + ((singleIndex + 1) % 3)]], lerp1),
                        Vector2.Lerp(original.UV[triangles[j + singleIndex]], original.UV[triangles[j + ((singleIndex + 2) % 3)]], lerp2));

                //对于切割边缘
                if (sideCount == 1)
                {
                    partMesh.AddTriangle(i,
                        original.Vertices[triangles[j + singleIndex]],
                        ray1.origin + ray1.direction.normalized * enter1,
                        ray2.origin + ray2.direction.normalized * enter2,
                        original.Normals[triangles[j + singleIndex]],
                        Vector3.Lerp(original.Normals[triangles[j + singleIndex]], original.Normals[triangles[j + ((singleIndex + 1) % 3)]], lerp1),
                        Vector3.Lerp(original.Normals[triangles[j + singleIndex]], original.Normals[triangles[j + ((singleIndex + 2) % 3)]], lerp2),
                        original.UV[triangles[j + singleIndex]],
                        Vector2.Lerp(original.UV[triangles[j + singleIndex]], original.UV[triangles[j + ((singleIndex + 1) % 3)]], lerp1),
                        Vector2.Lerp(original.UV[triangles[j + singleIndex]], original.UV[triangles[j + ((singleIndex + 2) % 3)]], lerp2));

                    continue;
                }

                if (sideCount == 2)
                {
                    partMesh.AddTriangle(i,
                        ray1.origin + ray1.direction.normalized * enter1,
                        original.Vertices[triangles[j + ((singleIndex + 1) % 3)]],
                        original.Vertices[triangles[j + ((singleIndex + 2) % 3)]],
                        Vector3.Lerp(original.Normals[triangles[j + singleIndex]], original.Normals[triangles[j + ((singleIndex + 1) % 3)]], lerp1),
                        original.Normals[triangles[j + ((singleIndex + 1) % 3)]],
                        original.Normals[triangles[j + ((singleIndex + 2) % 3)]],
                        Vector2.Lerp(original.UV[triangles[j + singleIndex]], original.UV[triangles[j + ((singleIndex + 1) % 3)]], lerp1),
                        original.UV[triangles[j + ((singleIndex + 1) % 3)]],
                        original.UV[triangles[j + ((singleIndex + 2) % 3)]]);
                    partMesh.AddTriangle(i,
                        ray1.origin + ray1.direction.normalized * enter1,
                        original.Vertices[triangles[j + ((singleIndex + 2) % 3)]],
                        ray2.origin + ray2.direction.normalized * enter2,
                        Vector3.Lerp(original.Normals[triangles[j + singleIndex]], original.Normals[triangles[j + ((singleIndex + 1) % 3)]], lerp1),
                        original.Normals[triangles[j + ((singleIndex + 2) % 3)]],
                        Vector3.Lerp(original.Normals[triangles[j + singleIndex]], original.Normals[triangles[j + ((singleIndex + 2) % 3)]], lerp2),
                        Vector2.Lerp(original.UV[triangles[j + singleIndex]], original.UV[triangles[j + ((singleIndex + 1) % 3)]], lerp1),
                        original.UV[triangles[j + ((singleIndex + 2) % 3)]],
                        Vector2.Lerp(original.UV[triangles[j + singleIndex]], original.UV[triangles[j + ((singleIndex + 2) % 3)]], lerp2));

                    continue;
                }
            }
        }

        partMesh.FillArrays();

        return partMesh;
    }

    /// <summary>
    /// 添加边
    /// </summary>
    private void AddEdge(int subMesh, MeshPart meshPart, Vector3 normal, Vector3 vertex1, Vector3 vertex2, Vector2 uv1, Vector2 uv2)
    {
        if (!edgeSet)
        {
            edgeSet = true;
            edgeVertex = vertex1;
            edgeUV = uv1;
        }
        else
        {
            edgePlane.Set3Points(edgeVertex, vertex1, vertex2);

            meshPart.AddTriangle(subMesh,
                edgeVertex, edgePlane.GetSide(edgeVertex + normal) ? vertex1 : vertex2, edgePlane.GetSide(edgeVertex + normal) ? vertex2 : vertex1,
                normal, normal, normal,
                edgeUV, uv1, uv2);
        }
    }
}


using UnityEngine;
using Aya.Extension;
using Sirenix.OdinInspector;

/// <summary>
/// 绘制多边形
/// TODO: 存在两点重合时绘制有问题
/// </summary>
public class PlaneDrawer : MonoBehaviour
{
    public Material material;
    public Transform[] vertices;
    private MeshRenderer mRenderer;
    private MeshFilter mFilter;

    public void Init()
    {
        Draw();
    }

    void Update()
    {
        //Draw();
    }

    [Button("Draw")]
    public void ClickDraw()
    {
        Draw();
    }

    [ContextMenu("Draw")]
    public void Draw()
    {
        Vector2[] vertices2D = new Vector2[vertices.Length];
        Vector3[] vertices3D = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertice = vertices[i].position;
            vertices2D[i] = new Vector2(vertice.x, vertice.z);
            vertice.y = 0f;
            vertices3D[i] = vertice;
        }

        Triangulator tr = new Triangulator(vertices2D);
        int[] triangles = tr.Triangulate();

        Mesh mesh = new Mesh();
        mesh.vertices = vertices3D;
        mesh.triangles = triangles;

        if (mRenderer == null)
        {
            mRenderer = gameObject.GetOrAddComponent<MeshRenderer>();
        }
        var newMaterial = new Material(material);
        mRenderer.material = newMaterial;
        if (mFilter == null)
        {
            mFilter = gameObject.GetOrAddComponent<MeshFilter>();
        }
        mFilter.mesh = mesh;
    }
}

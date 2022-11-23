using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    /// <summary>
    /// ģ�Ͳ��������
    /// </summary>
    public static class RenderExtension
    {
        public static MaterialPropertyBlock _block;
        /// <summary>
        /// ������Ⱦ��
        /// </summary>
        public static MaterialPropertyBlock Block
        {
            get
            {
                if (_block == null)
                    _block = new MaterialPropertyBlock();
                return _block;
            }
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="self"></param>
        /// <param name="shared">�Ƿ��ȡ���������</param>
        /// <returns></returns>
        public static Material GetMaterialProperty(this Transform self,bool shared = false)
        {
            var render = self.GetComponentInChildren<Renderer>();
            return shared ? render.sharedMaterial : render.material;
        }

        /// <summary>
        /// ��ȡ��Χ�ж�������
        /// </summary>
        /// <param name="mesh">�ж�Mesh</param>
        /// <returns></returns>
        public static Vector3[] GetMeshVextexInWorld(Mesh mesh)
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

        /// <summary>
        /// ��ȡģ����Ļ�ռ��Χ��
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        public static Rect GetMeshRectInScreen(Mesh mesh)
        {
            Rect rect = new Rect();
            Vector3[] worldVertex = GetMeshVextexInWorld(mesh);
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
    }
}

using System.Collections.Generic;
using UnityEngine;

public class RopeTest : MonoBehaviour
{
    public MeshFilter TargetMesh;               //网格
    public Transform BallGroup;                 //球体集合(关节上所有球放这)
    public List<Transform> m_listBall;         //存放所有球体
    private List<MeshData> m_listMeshData;      //节点数据
    public int NodeStopCount { get; set; }

    void Start()
    {
        m_listBall = new List<Transform>();
        foreach (Transform tran in BallGroup)
        {
            m_listBall.Add(tran);
        }
        NodeStopCount = m_listBall.Count;


        m_listMeshData = new List<MeshData>();
        int totleMeshPoint = TargetMesh.mesh.vertices.Length;
        for (int i = 0; i < totleMeshPoint; i++)
        {
            MeshData data = new MeshData();

            data.Index = i;
            data.target = __FindNearest(TargetMesh.mesh.vertices[i]);
            if (data.target == null) Debug.Log("有空的");
            data.offset = TargetMesh.mesh.vertices[i] - data.target.localPosition;
            m_listMeshData.Add(data);
        }
    }

    private float _targetY;
    private float _lastChange;
    private bool _alive;
    private bool _end;
    // Update is called once per frame
    void Update()
    {
        MoveMeshPoint();
        var index = 1f;
        foreach(var ball in m_listBall)
        {
            //ball.GetComponent<Rigidbody>().isKinematic = true;
            //if (index <= NodeStopCount)
            //{
            //    ball.GetComponent<Rigidbody>().isKinematic = true;
            //}
            //else
            //{
            //    ball.GetComponent<Rigidbody>().isKinematic = false;
            //}
            if (index == 1)
            {
                if (ball.GetComponent<Rigidbody>().isKinematic == false)
                    _end = true;
            }
            if (index == 20)
            {
                if(Time.realtimeSinceStartup- _lastChange > 0.2f)
                {
                    _lastChange = Time.realtimeSinceStartup;
                    _targetY = Random.Range(-1f, 1f);
                }
                if(_alive || ball.GetComponent<Rigidbody>().isKinematic == false)
                {
                    _alive = true;
                    ball.GetComponent<Rigidbody>().isKinematic = !_end;
                    if (!_end)
                    {
                        var pos = ball.transform.localPosition;
                        //pos.y = 0f - (20f - NodeStopCount) / 20f * 2f;
                        pos.y = Mathf.Lerp(pos.y, _targetY, 0.01f);
                        ball.transform.localPosition = pos;
                    }
                }
            }
            index++;
            continue;
            //if (index == 20)
            //{
            //    ball.GetComponent<Rigidbody>().isKinematic = true;
            //    var pos = ball.transform.localPosition;
            //    pos.y = -1f;
            //    ball.transform.localPosition = pos;
            //}
            index++;
        }
    }

    private void MoveMeshPoint()
    {
        Vector3[] v3 = TargetMesh.mesh.vertices;
        for (int i = 0; i < m_listMeshData.Count; i++)
        {
            MeshData curData = m_listMeshData[i];
            Vector3 dir = curData.target.transform.TransformDirection(curData.offset);
            v3[i] = curData.target.localPosition + dir;
        }
        TargetMesh.mesh.vertices = v3;
    }

    private Transform __FindNearest(Vector3 v3)
    {
        if (m_listBall != null)
        {
            float MaxDis = 999999;
            Transform MaxTran = null;
            for (int i = 0; i < m_listBall.Count; i++)
            {
                float curDis = Vector3.Distance(m_listBall[i].localPosition, v3);
                if (curDis < MaxDis)
                {
                    MaxDis = curDis;
                    MaxTran = m_listBall[i];
                }
            }
            return MaxTran;
        }
        return null;
    }

}

public class MeshData
{
    public int Index;               //索引
    public Transform target;        //目标球球
    public Vector3 offset;          //与目标球球位置差距
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.Extension;

public class MeshDestroyTest : GameEntity
{
    public Transform MeshTrans;
    public Transform LockTarget;
    public Vector2 Pos;
    public Vector2 PosEnd;
    private List<MeshDestroyAble> _destroyList = new List<MeshDestroyAble>();

    protected override void Awake()
    {
        base.Awake();
        foreach(var mesh in MeshTrans.GetComponentsInChildren<MeshDestroyAble>())
        {
            _destroyList.Add(mesh);
        }
    }

    private void Start()
    {
        UI.SetLockTarget(LockTarget);
        return;
        foreach (var mesh in _destroyList)
        {
            startPos = Pos;
            endPos = PosEnd;
            Plane plane = new Plane(GetNormal(), (startPos + endPos) * 2); ;
            var obj = GameObject.CreatePrimitive(PrimitiveType.Plane); //Test for plane
            obj.transform.up = plane.normal;
            obj.transform.SetLocalPositionY(Mathf.Lerp(Pos.y, PosEnd.y, 0.5f));
            MeshDestroyMachine.Ins.SliceMesh(plane, mesh);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy();
        Slice();
    }

    private void Destroy()
    {
        //鼠标左键点击摧毁
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                MeshDestroyAble destroyAble = hit.collider.GetComponent<MeshDestroyAble>();
                if (destroyAble != null)
                {
                    MeshDestroyMachine.Ins.DestroyMesh(destroyAble);
                }
            }
        }
    }

    private Vector3 startPos;
    private Vector3 endPos;

    private void Slice()
    {
        //鼠标右键拖拽切割
        if (Input.GetMouseButtonDown(1))
        {
            startPos = MouseWorldPos();
        }

        if (Input.GetMouseButton(1))
        {
            endPos = MouseWorldPos();
            //DrawLine();
        }

        if (Input.GetMouseButtonUp(1))
        {
            endPos = MouseWorldPos();
            Plane plane = new Plane(GetNormal(), (startPos + endPos) * 2);
            foreach(var mesh in _destroyList)
            {
                var rect = mesh.MeshRectInScreen;
                var start = Camera.main.WorldToScreenPoint(startPos);
                var end = Camera.main.WorldToScreenPoint(endPos);
                var inside = (start.x < rect.xMax && start.y < rect.yMax && end.x > rect.xMin && end.y > rect.yMin);
                if (inside)
                {
                    MeshDestroyMachine.Ins.SliceMesh(plane, mesh);
                }
            }
            //GameObject.CreatePrimitive(PrimitiveType.Plane).transform.up = plane.normal; //Test for plane
            //判断平面是否切到物体的方法：startPos和endPos构成的线是否和模型屏幕空间包围盒相交
            //判断刀是否切到物体：基于刀的法线和位置构建平面，然后切
        }
    }

    private Vector3 MouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
    }

    private Vector3 GetNormal()
    {
        return Vector3.Cross(startPos - endPos, Camera.main.transform.forward);
    }

    private void DrawLine()
    {
        Debug.DrawLine(startPos, endPos, Color.red);
    }
}


using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandle : MonoBehaviour
{
    #region 公有变量
    //------------------------------------------------------------------------------------

    //------------------------------------------------------------------------------------
    #endregion

    #region 私有变量
    //------------------------------------------------------------------------------------
    private Transform dragGameObject;
    private Vector3 offset;
    private bool isPicking;
    private Vector3 targetScreenPoint;
    public int StopNodeCount;
    public List<RopeTest> RopeList;
    //------------------------------------------------------------------------------------
    #endregion

    #region 公有方法
    #endregion

    #region 私有方法
    //------------------------------------------------------------------------------------

    [Button("Add")]
    public void Add()
    {
        StopNodeCount++;
    }

    [Button("Down")]
    public void Down()
    {
        StopNodeCount--;
    }

    private void Update()
    {
        foreach(var rope in RopeList)
        {
            rope.NodeStopCount = StopNodeCount;
        }
        return;
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckGameObject())
            {
                offset = dragGameObject.transform.position - UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPoint.z));
            }
        }

        if (isPicking)
        {
            //当前鼠标所在的屏幕坐标
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPoint.z);
            //把当前鼠标的屏幕坐标转换成世界坐标
            Vector3 curWorldPoint = UnityEngine.Camera.main.ScreenToWorldPoint(curScreenPoint);
            Vector3 targetPos = curWorldPoint + offset;

            dragGameObject.position = targetPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPicking = false;
            if (dragGameObject != null)
            {
                dragGameObject = null;
            }
        }

    }
    //------------------------------------------------------------------------------------
    /// <summary>
    /// 检查是否点击到cbue
    /// </summary>
    /// <returns></returns>
    bool CheckGameObject()
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000f))
        {
            isPicking = true;
            //得到射线碰撞到的物体
            dragGameObject = hitInfo.collider.gameObject.transform;

            targetScreenPoint = UnityEngine.Camera.main.WorldToScreenPoint(dragGameObject.position);
            return true;
        }
        return false;
    }
    //------------------------------------------------------------------------------------
    #endregion
}

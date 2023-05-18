using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class PhysicsExtension
    {
        public static List<Vector3> GetParabola(Vector3 startPos, Vector3 direct, float power)
        {
            Vector3 MoveSpeed = direct * power;
            //定义一个列表存放所有的计算的点
            List<Vector3> pointList = new List<Vector3>();
            pointList.Add(startPos);//第一个点就是当前物体的点
                                                          //通过第一个点逐步计算下一个点
            for (int i = 1; i < 50; i++)//随便先计算五十个点
            {
                float time = i * 0.02f;//每隔0.1秒计算一次
                float timePow = time * time;//time的平方
                                            //下一个点
                Vector3 point = new Vector3(pointList.First().x + MoveSpeed.x * time, pointList.First().y + MoveSpeed.y * time - 0.2f * Physics.gravity.magnitude * timePow,
                    pointList.First().z + MoveSpeed.z * time);
                //进行射线检测，判断和上一个是否有障碍阻挡,有阻挡就结束计算
                Ray ray = new Ray(pointList.Last(), point - pointList.Last());//创建射线
                Physics.Raycast(ray.origin, ray.direction, out var hit, Vector3.Distance(pointList.Last(), point));//发射射线
                if (hit.collider != null)
                {
                    //Debug.Log("中间有障碍物阻挡了！！" + hit.collider.name + ":" + Player.Pipe.MoveTrans.position + ":" + hit.point);
                }
                else
                {
                    pointList.Add(point);//加入到点的列表中
                }
            }

            return pointList;
        }
    }
}

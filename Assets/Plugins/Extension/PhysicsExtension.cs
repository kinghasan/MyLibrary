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
            //����һ���б������еļ���ĵ�
            List<Vector3> pointList = new List<Vector3>();
            pointList.Add(startPos);//��һ������ǵ�ǰ����ĵ�
                                                          //ͨ����һ�����𲽼�����һ����
            for (int i = 1; i < 50; i++)//����ȼ�����ʮ����
            {
                float time = i * 0.02f;//ÿ��0.1�����һ��
                float timePow = time * time;//time��ƽ��
                                            //��һ����
                Vector3 point = new Vector3(pointList.First().x + MoveSpeed.x * time, pointList.First().y + MoveSpeed.y * time - 0.2f * Physics.gravity.magnitude * timePow,
                    pointList.First().z + MoveSpeed.z * time);
                //�������߼�⣬�жϺ���һ���Ƿ����ϰ��赲,���赲�ͽ�������
                Ray ray = new Ray(pointList.Last(), point - pointList.Last());//��������
                Physics.Raycast(ray.origin, ray.direction, out var hit, Vector3.Distance(pointList.Last(), point));//��������
                if (hit.collider != null)
                {
                    //Debug.Log("�м����ϰ����赲�ˣ���" + hit.collider.name + ":" + Player.Pipe.MoveTrans.position + ":" + hit.point);
                }
                else
                {
                    pointList.Add(point);//���뵽����б���
                }
            }

            return pointList;
        }
    }
}

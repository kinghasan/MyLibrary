using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class RigidbodyExtension
    {
        /// <summary>
        /// �ö���ϵͳģ���ƶ�
        /// </summary>
        /// <param name="rigid"></param>
        /// <param name="direct">ÿ����ӵĶ���</param>
        /// <param name="maxSpeed">������ܺͣ�����ʱʧ��</param>
        public static void VelocityMove(this Rigidbody rigid, Vector3 direct, float maxSpeed)
        {
            if (rigid.velocity.magnitude > maxSpeed) return;

            rigid.velocity += direct;
        }

        /// <summary>
        /// �����ƶ�
        /// </summary>
        /// <param name="rigid"></param>
        /// <param name="direct">ÿ����ӵĶ���</param>
        /// <param name="maxSpeed">������ܺͣ�����ʱʧ��</param>
        public static void RotateMove(this Rigidbody rigid, Vector3 direct, float maxSpeed)
        {
            if (rigid.angularVelocity.magnitude > maxSpeed) return;

            rigid.AddForce(direct);
        }
    }
}

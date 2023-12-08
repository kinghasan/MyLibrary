using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class RigidbodyExtension
    {
        /// <summary>
        /// 用动力系统模拟移动
        /// </summary>
        /// <param name="rigid"></param>
        /// <param name="direct">每次添加的动力</param>
        /// <param name="maxSpeed">最大动力总和，超出时失控</param>
        public static void VelocityMove(this Rigidbody rigid, Vector3 direct, float maxSpeed)
        {
            if (rigid.velocity.magnitude > maxSpeed) return;

            rigid.velocity += direct;
        }

        /// <summary>
        /// 滚动移动
        /// </summary>
        /// <param name="rigid"></param>
        /// <param name="direct">每次添加的动力</param>
        /// <param name="maxSpeed">最大动力总和，超出时失控</param>
        public static void RotateMove(this Rigidbody rigid, Vector3 direct, float maxSpeed)
        {
            if (rigid.angularVelocity.magnitude > maxSpeed) return;

            rigid.AddForce(direct);
        }
    }
}

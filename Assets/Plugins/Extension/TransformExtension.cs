using UnityEngine;

namespace Aya.Extension
{
    public static class TransformExtension
    {
        public static void SetLocalPositionX(this Transform trans,float value)
        {
            var localPosition = trans.localPosition;
            localPosition.x = value;
            trans.localPosition = localPosition;
        }

        public static void SetLocalPositionY(this Transform trans, float value)
        {
            var localPosition = trans.localPosition;
            localPosition.y = value;
            trans.localPosition = localPosition;
        }

        public static void SetLocalPositionZ(this Transform trans, float value)
        {
            var localPosition = trans.localPosition;
            localPosition.z = value;
            trans.localPosition = localPosition;
        }

        public static void SetLocalRotationX(this Transform trans, float value)
        {
            var localRotation = trans.localRotation;
            localRotation.x = value;
            trans.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, localRotation.z);
        }

        public static void SetLocalRotationY(this Transform trans, float value)
        {
            var localRotation = trans.localRotation;
            localRotation.y = value;
            trans.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, localRotation.z);
        }

        public static void AddLocalRotationY(this Transform trans, float value)
        {
            var localRotation = trans.localRotation;
            localRotation.y += value;
            trans.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, localRotation.z);
        }

        public static void SetLocalRotationZ(this Transform trans, float value)
        {
            var localRotation = trans.localRotation;
            localRotation.z = value;
            trans.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, localRotation.z);
        }

        /// <summary>
        /// ��Y����ת
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        public static Quaternion RotateY(this Transform self, float angle)
        {
            var result = Quaternion.AngleAxis(angle, Vector3.up);
            self.rotation *= result;
            return self.rotation;
        }

        /// <summary>
        /// ��X����ת
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        public static Quaternion RotateX(this Transform self, float angle)
        {
            var result = Quaternion.AngleAxis(angle, Vector3.right);
            self.rotation *= result;
            return self.rotation;
        }

        /// <summary>
        /// ��Z����ת
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        public static Quaternion RotateZ(this Transform self, float angle)
        {
            var result = Quaternion.AngleAxis(angle, Vector3.forward);
            self.rotation *= result;
            return self.rotation;
        }

        /// <summary>
        /// ��ָ��������ת
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target">ָ������</param>
        /// <param name="axis">��ת�Ƕ�</param>
        /// <param name="angle">��תֵ</param>
        /// <returns></returns>
        public static Vector3 RotateWithTarget(this Transform self,Vector3 target,Vector3 axis,float angle)
        {
            var rotate = Quaternion.AngleAxis(angle, axis) * (self.transform.position - target) + target;
            self.transform.position = rotate;
            return self.transform.position;
        }
    }
}

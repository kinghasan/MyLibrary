using UnityEngine;

namespace Aya.Extension
{
    public static class QuaternionExtension
    {
        public static Quaternion Vector3Left = Quaternion.Euler(Vector3.down * 90f);
        public static Quaternion Vector3Right = Quaternion.Euler(Vector3.up * 90f);
        public static Quaternion Vector2Left = Quaternion.Euler(Vector3.back * 90f);
        public static Quaternion Vector2Right = Quaternion.Euler(Vector3.forward * 90f);

    }
}

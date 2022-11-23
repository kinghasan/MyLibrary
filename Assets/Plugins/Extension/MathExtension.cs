using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class MathExtension
    {
        public static long mod = 1000000007;

        /// <summary>
        /// 获取指定数量排序总和
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static long GetMathSoftCount(int x)
        {
            long res = 1;
            for (int i = x; i > 1; i--) res = (res * i) % mod;
            return res;
        }

        /// <summary>
        /// 是否质数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPrime(int n)
        {
            int m = (int)Mathf.Sqrt(n);
            for (int i = 2; i <= m; i++)
            {
                if (n % i == 0 && i != n)
                    return false;
            }
            return true;
        }
    }
}

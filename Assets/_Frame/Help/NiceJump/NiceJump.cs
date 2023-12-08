using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiceJump
{
    /// <summary>
    /// 增加重力(跳跃时，不按空格使用)
    /// </summary>
    /// <param name="body"></param>
    /// <param name="value"></param>
    public static void AddForce(Rigidbody body, float value)
    {
        body.AddForce(Vector3.down * value);
    }
}

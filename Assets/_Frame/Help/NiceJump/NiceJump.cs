using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiceJump
{
    /// <summary>
    /// ��������(��Ծʱ�������ո�ʹ��)
    /// </summary>
    /// <param name="body"></param>
    /// <param name="value"></param>
    public static void AddForce(Rigidbody body, float value)
    {
        body.AddForce(Vector3.down * value);
    }
}

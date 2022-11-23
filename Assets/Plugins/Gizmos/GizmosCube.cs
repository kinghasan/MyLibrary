using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosCube : MonoBehaviour
{
    public float Size = 1f;
    public Color Color = new Color(0, 0, 1f, 0.5f);
    public bool IsWire = false;

    void OnDrawGizmos()
    {
        Gizmos.color = Color;
        if (!IsWire)
        {
            Gizmos.DrawCube(transform.position, new Vector3(Size, Size, Size));
        }
        else
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(Size, Size, Size));
        }
    }
}

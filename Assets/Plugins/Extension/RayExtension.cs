using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class RayExtension
    {
        public static bool RayCastByMouse(float length,LayerMask layer,out RaycastHit hitInfo)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, length, layer))
                return true;

            return false;
        }
    }
}
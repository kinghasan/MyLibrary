using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.Extension;

public class Painter : GameEntity
{
    public Camera cam;
    [Space]
    public Color paintColor;
    public bool mouseSingleClick;
    public bool Erase;

    [Min(0.01f)]
    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            //var layerMask = 1 << 9;
            Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Paintable"));
            if (hit.collider != null)
            {
                transform.position = hit.point;
                var paintable = hit.collider.GetComponent<Paintable>();
                PainterManager.Ins.Paint(paintable, hit.point, radius, strength, hardness, Erase ? paintable.BaseColor : paintColor);
            }
        }
    }
    //void Update()
    //{
    //    bool click;
    //    click = mouseSingleClick ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0);

    //    if (click)
    //    {
    //        Vector3 position = Input.mousePosition;
    //        Ray ray = cam.ScreenPointToRay(position);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit, 100.0f))
    //        {
    //            Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
    //            transform.position = hit.point;
    //            Paintable p = hit.collider.GetComponent<Paintable>();
    //            if (p != null)
    //            {
    //                PainterManager.Ins.Paint(p, hit.point, radius, hardness, strength, paintColor);
    //            }
    //        }
    //    }

    //}
}

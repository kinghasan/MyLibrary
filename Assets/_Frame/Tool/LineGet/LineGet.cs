/////////////////////////////////////////////////////////////////////////////
//
//  Script   : LineGet.cs
//  Info     : 物体边缘线条
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(LineRenderer))]
public class LineGet : GameEntity<LineGet>
{
    public bool UseTransform;
    [Tooltip("长度"), HideIf("UseTransform")]
    public float Long;
    [Tooltip("段数"), HideIf("UseTransform")]
    public int PosCount;
    [ShowIf("UseTransform")]
    public List<Transform> TransList;
    [Tooltip("交互图层")]
    public LayerMask Layer;
    [Tooltip("使用函数初始化")]
    public bool InitWithMethod;
    public LineRenderer _lineRender { get; set; }
    public bool _alive { get; set; }
    public Vector3 _firstPos { get; set; }
    public float _posLength { get; set; }

    protected override void Awake()
    {
        base.Awake();
        if (!InitWithMethod)
            Init();
    }

    public void Init()
    {
        if (_lineRender == null)
            _lineRender = GetComponent<LineRenderer>();
        _alive = true;
        var pos = transform.position;
        pos.z -= Long / 2f;
        _firstPos = pos;
        _posLength = Long / PosCount;
        if (UseTransform)
            _lineRender.positionCount = TransList.Count;
        else
            _lineRender.positionCount = PosCount;

        transform.rotation = Quaternion.LookRotation(Vector3.up);
    }

    public void End()
    {
        _alive = false;
    }

    private void Update()
    {
        if (!_alive) return;
        if (UseTransform)
            GetPosition(TransList);
        else
            GetPosition();
    }

    public void GetPosition()
    {
        var posArr = new Vector3[PosCount];
        for (var i = 0; i < posArr.Length; i++)
        {
            var pos = _firstPos;
            pos.z += _posLength * i;
            pos.y += 10f;
            var ray = new Ray(pos, Vector3.down);
            bool raycast = Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo, 20f, Layer);
            var hitPos = hitInfo.point;
            hitPos.y += 1f;
            if (raycast)
                posArr[i] = hitPos;
            else
                posArr[i] = new Vector3(_firstPos.x, _firstPos.y, pos.z);
        }
        _lineRender.SetPositions(posArr);
    }

    public void GetPosition(List<Transform> transList)
    {
        var posArr = new Vector3[transList.Count];
        for (var i = 0; i < transList.Count; i++)
        {
            var pos = transList[i].position;
            pos.y += 10f;
            var ray = new Ray(pos, Vector3.down);
            bool raycast = Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo, 20f, Layer);
            if (raycast)
                posArr[i] = hitInfo.point;
            else
                posArr[i] = new Vector3(_firstPos.x, _firstPos.y, pos.z);
        }
        _lineRender.SetPositions(posArr);
    }
}

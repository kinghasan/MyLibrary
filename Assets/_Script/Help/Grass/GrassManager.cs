using Aya.Extension;
using System.Collections.Generic;
using UnityEngine;
using Aya.SimpleAction;

public class GrassManager : GameEntity<GrassManager>
{
    public AnimationCurve JumpCurve;
    public AnimationCurve BoomCurve;
    public float GrassHigh;
    public float GrassStartHeight;
    public Vector2 GrassSize;
    public float SpawnTime;
    public List<GameObject> GrassList;

    public HashSet<GameObject> GrassInsList { get; set; }

    protected override void Awake()
    {
        base.Awake();
        GrassInsList = new HashSet<GameObject>();
    }

    private class SortData : System.IComparable
    {
        public float min;
        public GameObject Grass;

        public int CompareTo(object obj)
        {
            var data = obj as SortData;
            if (min > data.min)
                return 1;
            else if (min == data.min)
                return 0;
            else
                return -1;
        }
    }

    public void DeSpawn()
    {
        foreach(var flower in GrassInsList)
        {
            GamePool.DeSpawn(flower);
        }

        GrassInsList.Clear();
    }

    /// <summary>
    /// 3D°æ
    /// </summary>
    /// <param name="range"></param>
    /// <param name="value"></param>
    public void SpawnGrass(Vector3[] range, int value)
    {
        var randomList = new List<Vector2>();
        var points = new Vector2[range.Length];
        for(var i = 0; i < points.Length; i++)
        {
            points[i] = new Vector2(range[i].x, range[i].z);
        }

        var pointList = MathUtilExtension.GetPointsInPolygon(points, GrassSize);

        for (var i = 0; i < value; i++)
        {
            if (pointList.Count == 0) break;
            var grass = GamePool.Spawn(GrassList.Random());
            var point = pointList.Random();
            pointList.Remove(point);

            grass.transform.position = point;

            GrassInsList.Add(grass);

            this.ExecuteDelay(() =>
            {
                SimpleActionFunction.SimpleValue(0f, 1f, SpawnTime, v =>
                {
                    grass.transform.SetLocalPositionZ(JumpCurve.Evaluate(v) * GrassHigh);
                    grass.transform.localScale = Vector3.one * BoomCurve.Evaluate(v);
                });
            }, i * 0.001f);
        }
    }

    /// <summary>
    /// 2D°æ
    /// </summary>
    /// <param name="range"></param>
    /// <param name="value"></param>
    public void SpawnGrass(Vector2[] range,int value)
    {
        var sortList = new List<SortData>();
        var randomList = new List<Vector2>();

        var pointList = MathUtilExtension.GetPointsInPolygon(range, GrassSize);

        for (var i = 0; i < value; i++)
        {
            if (pointList.Count == 0) break;
            var grass = GamePool.Spawn(GrassList.Random());
            var point = pointList.Random();
            pointList.Remove(point);

            grass.transform.position = point;

            GrassInsList.Add(grass);

            this.ExecuteDelay(() =>
            {
                SimpleActionFunction.SimpleValue(0f, 1f, SpawnTime, v =>
                {
                    grass.transform.SetLocalPositionZ(JumpCurve.Evaluate(v) * GrassHigh);
                    grass.transform.localScale = Vector3.one * BoomCurve.Evaluate(v);
                });
            }, i * 0.001f);
        }
    }
}

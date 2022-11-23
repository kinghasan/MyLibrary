using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.Extension;

/// <summary>
/// 对象池
/// </summary>
public class GamePool : GameEntity<GamePool>
{
    private Dictionary<GameObject, List<GameObject>> SpawnDic;
    private Dictionary<GameObject, List<GameObject>> DeSpawnDic;
    private Transform PoolTrans;

    protected override void Awake()
    {
        base.Awake();
        SpawnDic = new Dictionary<GameObject, List<GameObject>>();
        DeSpawnDic = new Dictionary<GameObject, List<GameObject>>();
        PoolTrans = new GameObject("Pool").transform;
    }

    public GameObject Spawn(GameObject prefab, Transform parent = null, Vector3 localPosition = default(Vector3))
    {
        //判断字典是否含有该对象
        if (DeSpawnDic.TryGetValue(prefab, out var objList))
        {
            if (objList.Count > 0)
            {
                var ins = objList[0];
                ins.SetActive(true);

                if (parent != null)
                    ins.transform.SetParent(parent);
                else
                    ins.transform.SetParent(PoolTrans);

                ins.transform.localPosition = localPosition;
                objList.Remove(ins);

                if (SpawnDic.TryGetValue(prefab, out var spawnList))
                {
                    spawnList.Add(ins);
                }
                else
                {
                    SpawnDic.Add(prefab, new List<GameObject>());
                    SpawnDic[prefab].Add(ins);
                }

                return ins;
            }
            else
            {
                var ins = Object.Instantiate(prefab);

                if (parent != null)
                    ins.transform.SetParent(parent);
                else
                    ins.transform.SetParent(PoolTrans);

                ins.transform.localPosition = localPosition;

                if(SpawnDic.TryGetValue(prefab, out var spawnList))
                {
                    spawnList.Add(ins);
                }
                else
                {
                    SpawnDic.Add(prefab, new List<GameObject>());
                    SpawnDic[prefab].Add(ins);
                }

                return ins;
            }
        }
        else
        {
            var ins = Object.Instantiate(prefab);

            if (parent != null)
                ins.transform.SetParent(parent);
            else
                ins.transform.SetParent(PoolTrans);

            ins.transform.localPosition = localPosition;

            if (SpawnDic.TryGetValue(prefab, out var spawnList))
            {
                spawnList.Add(ins);
            }
            else
            {
                SpawnDic.Add(prefab, new List<GameObject>());
                SpawnDic[prefab].Add(ins);
            }

            return ins;
        }
    }

    public T Spawn<T>(T prefab,Transform parent = null,Vector3 localPosition = default(Vector3)) where T : Component
    {
        var ins = Spawn(prefab.gameObject, parent, localPosition);
        return ins.GetComponent<T>();
    }

    public void DeSpawn(GameObject ins)
    {
        var dicKey = default(GameObject);
        foreach(var key in SpawnDic.Keys)
        {
            var list = SpawnDic[key];
            if (list.Contains(ins))
            {
                dicKey = key;
            }
        }

        if (dicKey != null)
        {
            ins.SetActive(false);
            if (DeSpawnDic.TryGetValue(dicKey, out var objList))
            {
                objList.Add(ins);
            }
            else
            {
                DeSpawnDic.Add(dicKey, new List<GameObject>());
                DeSpawnDic[dicKey].Add(ins);
            }
        }
        else
        {
            Object.Destroy(ins);
        }
    }

    public void DeSpawn<T>(T ins) where T : Component
    {
        DeSpawn(ins.gameObject);
    }
}

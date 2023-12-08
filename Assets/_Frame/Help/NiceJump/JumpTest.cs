using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.Physical;
using Sirenix.OdinInspector;
using Aya.Extension;

public class JumpTest : GameEntity
{
    public float Height;
    public LayerMask LayerMask;
    public GameObject Prefab;

    private bool Jumping;
    private Rigidbody Rigid;
    private List<GameObject> InsList;

    protected override void Awake()
    {
        base.Awake();
        Rigid = GetComponent<Rigidbody>();
        var collider = gameObject.AddComponent<ColliderListener>();
        collider.onTriggerEnter.Add(LayerMask, JumpDown);
        InsList = new List<GameObject>();
    }

    [Button("Ìí¼Ó")]
    public void Add()
    {
        var ins = GamePool.Spawn(Prefab);
        InsList.Add(ins);
        Debug.Log(InsList.Count);
    }

    [Button("É¾³ý")]
    public void Remove()
    {
        var ins = InsList.Last();
        GamePool.DeSpawn(ins);
        InsList.Remove(ins);
        Debug.Log(InsList.Count);
    }

    public void JumpDown(Collider other)
    {
        Jumping = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Jumping)
        {
            Rigid.velocity += Vector3.up * Height;
            Jumping = true;
        }

        if (!Jumping) return;

        if (Input.GetKey(KeyCode.Space))
        {

        }
        else
        {
            NiceJump.AddForce(Rigid, 1f);
        }
    }
}

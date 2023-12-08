using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : GameEntity
{
    public Transform TornadoTrans;
    public float TornadoRange;
    public float TornadoPower;

    // Update is called once per frame
    void Update()
    {
        var colliders = Physics.OverlapSphere(TornadoTrans.position, TornadoRange, LayerMask.GetMask("Item"));
        foreach (Collider collider in colliders)
        {
            var rigidbody = (Rigidbody)collider.gameObject.GetComponent(typeof(Rigidbody));
            if (rigidbody == null)
            {
                continue;
            }
            rigidbody.AddExplosionForce(TornadoPower * -1f, TornadoTrans.position + Vector3.down * 0.5f, TornadoRange);
        }
    }
}

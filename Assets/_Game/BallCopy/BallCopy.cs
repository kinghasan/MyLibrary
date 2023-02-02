using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.SimpleAction;

public class BallCopy : MonoBehaviour
{
    public Transform CopyTrans;
    public Transform CopyPrefab;

    private void Start()
    {
        StartCoroutine(Copy());
    }

    public IEnumerator Copy()
    {
        yield return new WaitForSeconds(5f);
        for (var i = 0; i < 20; i++)
        {
            var x = Random.Range(-100f, 100f);
            var z = Random.Range(-100f, 100f);
            var ball = Instantiate(CopyPrefab, CopyTrans);
            ball.GetComponent<Rigidbody>().velocity = new Vector3(x, 0f, z);
            //ball.GetComponent<Rigidbody>().angularVelocity = new Vector3(50f, 50f, 50f);
            //ball.GetComponent<Rigidbody>().velocity = new Vector3(x, 0f, z);
            SimpleActionManager.Ins.AddSimpleAction(new SimpleAction(() =>
            {
                //ball.GetComponent<Rigidbody>().isKinematic = false;
            }, 0.5f));
        }
        yield return null;
    }
}

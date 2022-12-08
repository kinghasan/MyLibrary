using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Grid : MonoBehaviour
{
    public List<ObiRope> RopeTestList;

    private void Update()
    {
        foreach(var rope in RopeTestList)
        {
            foreach(var part in rope.ListPart)
            {
                if (part.target.position.z < transform.position.z)
                {
                    part.attachmentType = ObiParticleAttachment.AttachmentType.Dynamic;
                }
            }
        }
        var pos = transform.position;
        pos.z += Time.deltaTime * 2f;
        transform.position = pos;
    }
}

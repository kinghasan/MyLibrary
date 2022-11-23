using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Physical
{
    [RequireComponent(typeof(Collider))]
    public class ColliderListener : MonoBehaviour
    {
        public Collider Collider { get; set; }
        public ColliderCallBack<Collider> onTriggerEnter = new ColliderCallBack<Collider>();
        public ColliderCallBack<Collider> onTriggerStay = new ColliderCallBack<Collider>();
        public ColliderCallBack<Collider> onTriggerExit = new ColliderCallBack<Collider>();

        private void OnTriggerEnter(Collider other)
        {
            var layer = other.gameObject.layer;
            foreach(var filter in onTriggerEnter.Filters)
            {
                if ((filter.Layer.value & (int)Mathf.Pow(2, layer)) == (int)Mathf.Pow(2, layer))
                {
                    filter.Action?.Invoke(other);
                }
                //if (filter.Layer.value == layer)
                //    filter.Action?.Invoke(other);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            var layer = other.gameObject.layer;
            foreach (var filter in onTriggerStay.Filters)
            {
                if ((filter.Layer.value & (int)Mathf.Pow(2, layer)) == (int)Mathf.Pow(2, layer))
                {
                    filter.Action?.Invoke(other);
                }
                //if (filter.Layer.value == layer)
                //    filter.Action?.Invoke(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var layer = other.gameObject.layer;
            foreach (var filter in onTriggerExit.Filters)
            {
                if ((filter.Layer.value & (int)Mathf.Pow(2, layer)) == (int)Mathf.Pow(2, layer))
                {
                    filter.Action?.Invoke(other);
                }
                //if (filter.Layer.value == layer)
                //    filter.Action?.Invoke(other);
            }
        }
    }
}

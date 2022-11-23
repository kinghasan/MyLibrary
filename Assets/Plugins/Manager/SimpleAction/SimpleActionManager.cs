using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.SimpleAction
{
    public class SimpleActionManager : MonoBehaviour
    {
        public static SimpleActionManager Ins { get; protected set; }

        protected  void Awake()
        {
            Ins = this as SimpleActionManager;
        }

        public List<SimpleAction> ActionList = new List<SimpleAction>();
        public List<SimpleAction> BrokenList = new List<SimpleAction>();

        private double LastTimeSinceStartup = -1f;
        private void Update()
        {
            var currentTime = Time.realtimeSinceStartup;
            if (LastTimeSinceStartup < 0f)
            {
                LastTimeSinceStartup = currentTime;
            }
            var deltaTime = (float)(currentTime - LastTimeSinceStartup);
            LastTimeSinceStartup = currentTime;
            Update(deltaTime);
        }

        public void Update(float deltaTime)
        {
            foreach (var action in ActionList)
            {
                if (action.IsBroken)
                {
                    BrokenList.Add(action);
                    continue;
                }

                action.Update(deltaTime);
            }

            for (var i = 0; i < BrokenList.Count; i++)
            {
                ActionList.Remove(BrokenList[i]);
            }
        }

        public void AddSimpleAction(SimpleAction simpleAction)
        {
            ActionList.Add(simpleAction);
        }
    }
}

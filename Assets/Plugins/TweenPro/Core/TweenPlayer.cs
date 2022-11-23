using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Aya.TweenPro
{
    [ExecuteInEditMode]
    [AddComponentMenu("TweenPro/TweenPro Player")]
    public class TweenPlayer : MonoBehaviour
    {
        public TweenAnimation Animation = new TweenAnimation();
        private bool Playing;

        public virtual void Awake()
        {
            Playing = false;
            Animation.Awake();
            //StartCoroutine(TestUpdate());
        }

        //public IEnumerator TestUpdate()
        //{
        //    var wait = new WaitForEndOfFrame();
        //    while (true)
        //    {
        //        Debug.Log(EditorApplication.timeSinceStartup);
        //        yield return wait; 
        //    }
        //}

        private double LastTimeSinceStartup = -1f;
        public void TweenUpdate()
        {
            if (Playing)
            {
                var currentTime = EditorApplication.timeSinceStartup;
                if (LastTimeSinceStartup < 0f)
                {
                    LastTimeSinceStartup = currentTime;
                }

                var deltaTime = (float)(currentTime - LastTimeSinceStartup);
                LastTimeSinceStartup = currentTime;
                Animation.Update(deltaTime);
            }
         }

        public void Play()
        {
            Playing = true;
        }

        public void Play(float startPos)
        {
            if (startPos > 1f)
                startPos = 1f;
            if (startPos < 0f)
                startPos = 0f;

            //Animation.AnimationProgress = startPos;
            Playing = true;
        }

        public void Stop()
        {
            Playing = false;
            LastTimeSinceStartup = -1f;
        }
    }


    [CustomEditor(typeof(TweenPlayer))]
    public class TweenEditor : Editor
    {
        public virtual TweenPlayer Target => target as TweenPlayer;
        public TweenAnimation animation => Target.Animation;

        public override void OnInspectorGUI()
        {
            Target.TweenUpdate();
            animation.OnInspectorGUI();
        }

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        public void OnEnable()
        {
            animation.InitEditor(this, Target);
        }
    }
}

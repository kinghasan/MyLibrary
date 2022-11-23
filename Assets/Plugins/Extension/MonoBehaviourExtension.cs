using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class MonoBehaviourExtension
    {
        public static Coroutine ExecuteDelay(this MonoBehaviour mono,Action action,float seconds, bool timeScale = true)
        {
            return mono.StartCoroutine(ExecuteDelayCoroutine(action, seconds, timeScale));
        }

        private static IEnumerator ExecuteDelayCoroutine(Action action, float seconds, bool timeScale)
        {
            if (timeScale)
            {
                yield return new WaitForSeconds(seconds);
            }
            else
            {
                yield return new WaitForSecondsRealtime(seconds);
            }

            action();
        }
    }
}

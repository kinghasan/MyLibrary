using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Aya.Events
{
    public class EventManager : MonoBehaviour
    {
        #region Instance

        protected static EventManager Instance;

        public static EventManager Ins
        {
            get
            {
                if (Instance != null) return Instance;
                Instance = (EventManager)FindObjectOfType(typeof(EventManager));
                if (Instance != null) return Instance;
                var obj = new GameObject
                {
                    hideFlags = HideFlags.HideAndDontSave,
                    name = "EventManager"
                };
                DontDestroyOnLoad(obj);
                Instance = obj.AddComponent<EventManager>();
                return Instance;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        protected static void Init()
        {
//            var ins = Ins;
//            UEventCallback.OnError += (eventHandler, eventType, args, exception) =>
//            {
//                Debug.LogError(exception.ToString());
//#if UNITY_EDITOR
//                EventHandler.CacheLog(eventHandler, eventType, args, false, exception);
//#endif
//            };
        }

        #endregion

        /// <summary>
        /// key:�¼�����
        /// value(key):����
        /// value(value):�¼������б�
        /// </summary>
        public Dictionary<object, Dictionary<MethodInfo, List<object>>> EventMethodInfoDic = new Dictionary<object, Dictionary<MethodInfo, List<object>>>();

        public void AddListener(object eventType, object target, MethodInfo method)
        {
            //������ڸ��¼�����������¼����󵽶�Ӧ����
            if (EventMethodInfoDic.ContainsKey(eventType))
            {
                var targetEventDic = EventMethodInfoDic[eventType];
                if (targetEventDic.ContainsKey(method))
                {
                    var objectList = targetEventDic[method];
                    if (objectList.Contains(target)) return;
                    objectList.Add(target);
                }
                targetEventDic.Add(method, new List<object>());
                targetEventDic[method].Add(target);
                return;
            }

            //��������������
            EventMethodInfoDic.Add(eventType, new Dictionary<MethodInfo, List<object>>());
            EventMethodInfoDic[eventType].Add(method, new List<object>());
            EventMethodInfoDic[eventType][method].Add(target);
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="args">�¼�����</param>
        public void SendDispatch(object eventType, object[] args)
        {
            if (EventMethodInfoDic.ContainsKey(eventType))
            {
                var methodDic = EventMethodInfoDic[eventType];
                foreach (var method in methodDic.Keys)
                {
                    foreach (var target in methodDic[method])
                    {
                        method.Invoke(target, args);
                    }
                }
            }
        }
    }
}

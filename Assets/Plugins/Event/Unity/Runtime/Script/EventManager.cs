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
        /// key:事件类型
        /// value(key):函数
        /// value(value):事件对象列表
        /// </summary>
        public Dictionary<object, Dictionary<MethodInfo, List<object>>> EventMethodInfoDic = new Dictionary<object, Dictionary<MethodInfo, List<object>>>();

        public void AddListener(object eventType, object target, MethodInfo method)
        {
            //如果存在该事件类型则添加事件对象到对应函数
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

            //如果不存在则添加
            EventMethodInfoDic.Add(eventType, new Dictionary<MethodInfo, List<object>>());
            EventMethodInfoDic[eventType].Add(method, new List<object>());
            EventMethodInfoDic[eventType][method].Add(target);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="args">事件参数</param>
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

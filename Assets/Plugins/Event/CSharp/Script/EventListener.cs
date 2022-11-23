using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Aya.Events
{
    public class EventListener
    {
        #region Property

        public static Dictionary<Type, Dictionary<MethodInfo, List<Attribute>>> MethodMap = new Dictionary<Type, Dictionary<MethodInfo, List<Attribute>>>();

        /// <summary>
        /// 事件监听对象
        /// </summary>
        public object Listener { get; }

        #endregion

        #region Construct

        public EventListener(object listener)
        {
            Listener = listener;
        }

        #endregion

        public void Register()
        {
            var objType = Listener.GetType();
            // 如果该类型对象已经被注册过，则直接遍历该类型所有需要监听的方法并注册到事件分发器
            if (MethodMap.ContainsKey(objType))
            {
                var tempObjEventDic = MethodMap[objType];
                foreach (var kv in tempObjEventDic)
                {
                    var method = kv.Key;
                    var eventAttributeList = kv.Value;
                    for (var i = 0; i < eventAttributeList.Count; i++)
                    {
                        var eventAttribute = eventAttributeList[i];
                        _addListenerWithAttribute(eventAttribute, method);
                    }
                }

                return;
            }

            // 如果是未注册过的对象类型，则遍历所有被标记需要监听的方法，进行注册
            var objEventDic = new Dictionary<MethodInfo, List<Attribute>>();
            MethodMap.Add(objType, objEventDic);
            var methodInfos = objType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            for (var i = 0; i < methodInfos.Length; i++)
            {
                var methodInfo = methodInfos[i];
                var attributes = methodInfo.GetCustomAttributes(typeof(Attribute), false);
                if (attributes.Length <= 0) continue;

                var eventAttributeList = new List<Attribute>();
                objEventDic.Add(methodInfo, eventAttributeList);

                for (var j = 0; j < attributes.Length; j++)
                {
                    var attribute = attributes[j];
                    if (attribute == null) return;
                    var attributeTemp = attribute as Attribute;
                    if (attributeTemp == null) return;
                    _addListenerWithAttribute(attributeTemp, methodInfo);
                    eventAttributeList.Add(attributeTemp);
                }
            }
        }

        public void DeRegister()
        {

        }

        /// <summary>
        /// 根据特性标签添加事件监听
        /// </summary>
        /// <param name="attribute">特性</param>
        /// <param name="methodInfo">监听方法</param>
        private void _addListenerWithAttribute(Attribute attribute, MethodInfo methodInfo)
        {
            // Listen Attribute
            if (attribute is ListenAttribute attrListen)
            {
                //var priority = attrListen.Priority;
                //var interrupt = attrListen.Interrupt;
                foreach (var eventType in attrListen.Types)
                {
                    _addListener(eventType, methodInfo);
                }
            }

        }

        /// <summary>
        /// 将监听器中的方法注册到事件分发器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="methodInfo">监听 方法</param>
        /// <param name="priority">优先级</param>
        /// <param name="interrupt">是否中断事件队列</param>
        private void _addListener(object eventType, MethodInfo methodInfo)
        {
            var manager = EventManager.Ins;
            manager.AddListener(eventType, Listener, methodInfo);
        }
    }
}

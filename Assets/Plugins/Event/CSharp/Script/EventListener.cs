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
        /// �¼���������
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
            // ��������Ͷ����Ѿ���ע�������ֱ�ӱ���������������Ҫ�����ķ�����ע�ᵽ�¼��ַ���
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

            // �����δע����Ķ������ͣ���������б������Ҫ�����ķ���������ע��
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
        /// �������Ա�ǩ����¼�����
        /// </summary>
        /// <param name="attribute">����</param>
        /// <param name="methodInfo">��������</param>
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
        /// ���������еķ���ע�ᵽ�¼��ַ���
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="methodInfo">���� ����</param>
        /// <param name="priority">���ȼ�</param>
        /// <param name="interrupt">�Ƿ��ж��¼�����</param>
        private void _addListener(object eventType, MethodInfo methodInfo)
        {
            var manager = EventManager.Ins;
            manager.AddListener(eventType, Listener, methodInfo);
        }
    }
}

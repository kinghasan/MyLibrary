using System;

namespace Aya.Events
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ListenAttribute : Attribute
    {
        /// <summary>
        /// �¼�����
        /// </summary>
        public object[] Types { get; }

        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// �Ƿ��ж��¼�����
        /// </summary>
        public bool Interrupt { get; }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="eventTypes">�¼���������(�����ظ����������¼������������ȼ�)</param>
        public ListenAttribute(params object[] eventTypes)
        {
            Types = eventTypes;
            //Priority = 0;
            //Interrupt = false;
        }
    }
}

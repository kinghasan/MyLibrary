using System;

namespace Aya.Events
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ListenAttribute : Attribute
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public object[] Types { get; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// 是否中断事件队列
        /// </summary>
        public bool Interrupt { get; }

        /// <summary>
        /// 监听事件
        /// </summary>
        /// <param name="eventTypes">事件类型数组(不可重复，监听多事件不能设置优先级)</param>
        public ListenAttribute(params object[] eventTypes)
        {
            Types = eventTypes;
            //Priority = 0;
            //Interrupt = false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.SimpleAction
{
    public class SimpleAction
    {
        public Action Action;
        public bool IsBroken;
        public float Timer = -1;
        public float StartTime;
        public float Progress => StartTime / Timer;

        public SimpleAction() { }

        public SimpleAction(Action action, float timer = -1)
        {
            Action = action;
            Timer = timer;
        }

        public virtual void Update(float deltaTime)
        {
            Action?.Invoke();
            if (Timer > 0)
            {
                if (StartTime >= Timer)
                {
                    Death();
                }
                StartTime += deltaTime;
                if (StartTime >= Timer)
                {
                    StartTime = Timer;
                }
            }
        }

        public void Death()
        {
            IsBroken = true;
        }
    }

    //public class SimpleAction<T> : SimpleAction where T : UnityEngine.Object
    //{
    //    public new Action<T> Action;

    //    public SimpleAction(Action<T> action)
    //    {
    //        Action = action;
    //    }

    //    public virtual void Update(T target)
    //    {
    //        Action?.Invoke(target);
    //    }
    //}

    public static class SimpleActionFunction
    {
        /// <summary>
        /// 简易直线移动
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="timer"></param>
        public static void SimplePosition(Transform trans,Vector3 from,Vector3 to,float timer)
        {
            var simpleAction = new SimpleAction();
            simpleAction.Action = () =>
            {
                var lerp = Vector3.Lerp(from, to, simpleAction.Progress);
                trans.position = lerp;
            };
            simpleAction.Timer = timer;
            SimpleActionManager.Ins.AddSimpleAction(simpleAction);
        }

        /// <summary>
        /// 简易抛物线
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="height"></param>
        /// <param name="timer"></param>
        public static void SimpleParabola(Transform trans, Vector3 from, Vector3 to, float height, float timer)
        {
            var maxY = Mathf.Max(from.y, to.y) + height;
            var maxHeight = Vector3.zero;
            maxHeight.y = maxY;
            var height1 = from.y;
            var height2 = to.y;
            var simpleAction = new SimpleAction();
            simpleAction.Action = () =>
            {
                var lerp = Vector3.Lerp(from, to, simpleAction.Progress);
                var height = simpleAction.Progress <= 0.5f ? Vector3.Lerp(from, maxHeight, simpleAction.Progress / 0.5f ) 
                    : Vector3.Lerp(maxHeight, to, (simpleAction.Progress - 0.5f) / 0.5f);
                lerp.y = height.y;
                trans.position = lerp;
            };
            simpleAction.Timer = timer;
            SimpleActionManager.Ins.AddSimpleAction(simpleAction);
        }

        /// <summary>
        /// 简易进度数值
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="timer"></param>
        /// <param name="valueUpdate"></param>
        public static void SimpleValue(float from,float to,float timer,Action<float> valueUpdate)
        {
            var simpleAction = new SimpleAction();
            simpleAction.Timer = timer;
            simpleAction.Action = () =>
            {
                var value = Mathf.Lerp(from, to, simpleAction.Progress);
                valueUpdate?.Invoke(value);
            };
            SimpleActionManager.Ins.AddSimpleAction(simpleAction);
        }

        /// <summary>
        /// 简易缩放(需要XYZ数值相同)
        /// </summary>
        /// <param name="self"></param>
        /// <param name="to"></param>
        /// <param name="timer"></param>
        public static void SimpleScale(Transform self, float to, float timer)
        {
            var simpleAction = new SimpleAction();
            simpleAction.Timer = timer;
            var fromScale = self.localScale.x;
            simpleAction.Action = () =>
            {
                var value = Mathf.Lerp(fromScale, to, simpleAction.Progress);
                self.transform.localScale = Vector3.one * value;
            };
            SimpleActionManager.Ins.AddSimpleAction(simpleAction);
        }

        /// <summary>
        /// 简易旋转
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="to"></param>
        /// <param name="timer"></param>
        public static void SimpleRotation(Transform trans, Quaternion to, float timer)
        {
            var simpleAction = new SimpleAction();
            simpleAction.Timer = timer;
            var from = trans.rotation;
            simpleAction.Action = () =>
            {
                trans.rotation = Quaternion.Lerp(from, to, simpleAction.Progress);
            };
            SimpleActionManager.Ins.AddSimpleAction(simpleAction);
        }
    }
}

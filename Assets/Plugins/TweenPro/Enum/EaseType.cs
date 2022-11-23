using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.TweenPro
{
    public static class EaseType
    {
        [EaseFunction(typeof(EaseCustom), "Custom", "Custom")]
        public const int Custom = -1;
        [EaseFunction(typeof(EaseLinear), "Linear", "Linear")]
        public const int Linear = 0;
        [EaseFunction(typeof(EaseFall), "Fall", "Fall")]
        public const int Fall = 1;

        public static readonly Dictionary<int, EaseFunction> FunctionDic = new Dictionary<int, EaseFunction>();
        public static readonly Dictionary<int, EaseFunctionAttribute> FunctionInfoDic = new Dictionary<int, EaseFunctionAttribute>();

        static EaseType()
        {
            Cache(typeof(EaseType));
        }

        public static void Cache(Type type)
        {
            var fieldInfos = type.GetFields();
            foreach (var fieldInfo in fieldInfos)
            {
                var attributes = fieldInfo.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (!(attribute is EaseFunctionAttribute easeFunctionAttribute)) continue;
                    var index = (int)fieldInfo.GetValue(null);
                    var function = (EaseFunction)Activator.CreateInstance(easeFunctionAttribute.Type);
                    FunctionDic.Add(index, function);
                    FunctionInfoDic.Add(index, easeFunctionAttribute);
                }
            }
        }
    }


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EaseFunctionAttribute : Attribute
    {
        public Type Type { get; set; }
        public string DisplayName { get; set; }
        public string MenuPath { get; set; }

        public EaseFunctionAttribute(Type type, string displayName, string menuPath)
        {
            Type = type;
            DisplayName = displayName;
            MenuPath = menuPath;
        }
    }

    public abstract class EaseFunction
    {
        public abstract int Type { get; }
        public virtual bool SupportStrength { get; } = false;
        public virtual float DefaultStrength { get; } = 1f;

        public static Keyframe[] DefaultKeyFrames = new[] { new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1) };

        public virtual AnimationCurve Ease()
        {
            return default;
        }
    }

    public class EaseCustom : EaseFunction
    {
        public override int Type => EaseType.Custom;
        public override AnimationCurve Ease()
        {
            return new AnimationCurve(DefaultKeyFrames);
        }
    }

    public class EaseLinear : EaseFunction
    {
        public override int Type => EaseType.Linear;
        public override AnimationCurve Ease()
        {
            var keys = new Keyframe[]
            {
                new Keyframe(0,0,1,1),
                new Keyframe(1,1,1,1)
            };
            var curve = new AnimationCurve(keys);
            return curve;
        }
    }

    public class EaseFall : EaseFunction
    {
        public override int Type => EaseType.Fall;
        public override AnimationCurve Ease()
        {
            var keys = new Keyframe[]
            {
                new Keyframe(0,1,1,1),
                new Keyframe(1,0,1,1)
            };
            var curve = new AnimationCurve(keys);
            return curve;
        }
    }
}

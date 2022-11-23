using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Aya.UI;

namespace Aya.TweenPro
{
    [Serializable]
    public abstract class TweenBase
    {
        public static string EditorName;

        public TweenAnimation Animation;
        [HideInInspector] public bool FoldOut;

        public Action UpdateAction;

        public TweenBase()
        {
            FoldOut = true;
        }

        public abstract void OnInspectorGUI();

        public abstract void DrawHead();

        public abstract void DrawBody();

        public abstract void DrawBottom();

        public abstract void Play(float time);

        public abstract void Reset();

        public abstract void ResetAnimation();
    }

    public abstract class Tween : TweenBase
    {
        public int Ease;
        public AnimationCurve Curve = new AnimationCurve(EaseFunction.DefaultKeyFrames);

        public override void OnInspectorGUI()
        {
            DrawTitle();
            if (FoldOut)
            {
                DrawHead();
                DrawBody();
                DrawBottom();
            }
        }

        public void DrawTitle()
        {
            using (GUIHorizontal.Create())
            {
                FoldOut = EditorGUILayout.Toggle(GUIContent.none, FoldOut, EditorStyles.foldout, GUILayout.Width(GUIUtil.CharacterWidth));
                var btnFlexibleInfo = GUILayout.Button(GetType().GetProperty("EditorName").GetValue(null).ToString(), EditorStyles.boldLabel, GUILayout.MinWidth(0), GUILayout.MaxWidth(Screen.width));
                if (btnFlexibleInfo)
                    FoldOut = !FoldOut;

                // Menu Button
                var btnContextMenu = GUIUtil.DrawHeaderContextMenuButton();
                if (btnContextMenu)
                {
                    var menu = CreateContextMenu();
                    menu.ShowAsContext();
                }
            }
        }

        internal GenericMenu CreateContextMenu()
        {
            var menu = new GenericMenu();

            // Remove
            menu.AddItem(new GUIContent("Remove Tween"), false, () =>
            {
                Animation.RemoveTween(this);
            });
            menu.AddSeparator("");
            // Remove
            menu.AddItem(new GUIContent("Reset"), false, () =>
            {
                Reset();
            });

            return menu;
        }

        internal GenericMenu CreateEaseTypeMenu()
        {
            var menu = new GenericMenu();
            foreach (var kv in EaseType.FunctionInfoDic)
            {
                var easeType = kv.Key;
                var easeFunctionAttribute = kv.Value;
                menu.AddItem(new GUIContent(easeFunctionAttribute.MenuPath), Ease == easeType, () =>
                {
                    var easeFunction = EaseType.FunctionDic[easeType];
                    Ease = easeType;
                    Curve = easeFunction.Ease();
                    //StrengthProperty.floatValue = easeFunction.SupportStrength ? easeFunction.DefaultStrength : 1f;
                    //EaseProperty.serializedObject.ApplyModifiedProperties();
                });

                if (easeType < 0)
                {
                    menu.AddSeparator("");
                }
            }

            return menu;
        }
    }

    public abstract class Tween<T> : Tween where T: UnityEngine.Object
    {
        public UnityEngine.Object TargetTrans;
        public T Target
        {
            get
            {
                if (TargetTrans != null)
                    return (T)TargetTrans;
                return null;
            }
            set
            {
                TargetTrans = value;
            }
        }

        public abstract void OnTargetChange();

        public override void DrawHead()
        {
            using (GUIHorizontal.Create())
            {
                GUILayout.Label("Target");
                using (GUICheck.Create(() =>
                {
                    OnTargetChange();
                }))
                {
                    TargetTrans = EditorGUILayout.ObjectField(TargetTrans, typeof(T), true);
                }
            }
        }

        public override void DrawBottom()
        {
            using (GUIHorizontal.Create())
            {
                // Ease
                GUILayout.Label(nameof(Ease), EditorStyles.label, GUILayout.Width(EditorStyle.LabelWidth));
                var displayEaseName = EaseType.FunctionInfoDic[Ease].DisplayName;
                var easeRect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight, EditorStyles.popup);
                var easeTypeBtn = GUI.Button(easeRect, displayEaseName, EditorStyles.popup);
                if (easeTypeBtn)
                {
                    var menu = CreateEaseTypeMenu();
                    menu.ShowAsContext();
                }

                // Curve
                GUILayout.Label(nameof(Curve), EditorStyles.label, GUILayout.Width(EditorStyle.LabelWidth));
                // var curveRect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight, EditorStyles.popup);
                if (Ease == EaseType.Custom)
                {
                    Curve = EditorGUILayout.CurveField(Curve);
                }
                else
                {
                    using (GUIEnableArea.Create(false))
                    {
                        EditorGUILayout.CurveField(Curve); //, GUILayout.Height(EditorStyle.SingleButtonWidth * 2f));
                    }
                }

                //using (GUIEnableArea.Create(IsCustomCurve))
                //{
                //    if (GUIUtil.DrawContextMenuButton())
                //    {
                //        var menu = CreateCurveMenu();
                //        menu.ShowAsContext();
                //    }
                //}
            }
        }

        public override void Reset()
        {
            TargetTrans = null;
        }
    }

    [Tween]
    public abstract class TransformTween : Tween<Transform>
    {
        public static new string EditorName => "Transform";

        public override void DrawBottom()
        {
            using (GUIHorizontal.Create())
            {
                // Ease
                GUILayout.Label(nameof(Ease), EditorStyles.label, GUILayout.Width(EditorStyle.LabelWidth));
                var displayEaseName = EaseType.FunctionInfoDic[Ease].DisplayName;
                var easeRect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight, EditorStyles.popup);
                var easeTypeBtn = GUI.Button(easeRect, displayEaseName, EditorStyles.popup);
                if (easeTypeBtn)
                {
                    var menu = CreateEaseTypeMenu();
                    menu.ShowAsContext();
                }

                // Curve
                GUILayout.Label(nameof(Curve), EditorStyles.label, GUILayout.Width(EditorStyle.LabelWidth));
                // var curveRect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight, EditorStyles.popup);
                if (Ease == EaseType.Custom)
                {
                    Curve = EditorGUILayout.CurveField(Curve);
                }
                else
                {
                    using (GUIEnableArea.Create(false))
                    {
                        EditorGUILayout.CurveField(Curve); //, GUILayout.Height(EditorStyle.SingleButtonWidth * 2f));
                    }
                }

                //using (GUIEnableArea.Create(IsCustomCurve))
                //{
                //    if (GUIUtil.DrawContextMenuButton())
                //    {
                //        var menu = CreateCurveMenu();
                //        menu.ShowAsContext();
                //    }
                //}
            }
        }

        public override void Reset()
        {
            TargetTrans = null;
        }
    }

    public class TransformPosition : TransformTween
    {
        public static new string EditorName => "Transform Position";
        public Vector3 From;
        public Vector3 To;

        public override void DrawBody()
        {
            From = EditorGUILayout.Vector3Field(new GUIContent(nameof(From)), From);
            To = EditorGUILayout.Vector3Field(new GUIContent(nameof(To)), To);
        }

        public override void Play(float time)
        {
            var obj = (Transform)Target;
            obj.localPosition = Vector3.Lerp(From, To, Curve.Evaluate(Animation.AnimationProgress));
        }

        public override void ResetAnimation()
        {
            var obj = (Transform)Target;
            if (obj == null) return;
            obj.localPosition = Vector3.Lerp(From, To, Curve.Evaluate(0f));
        }

        public override void Reset()
        {
            base.Reset();
            Target = null;
            From = Vector3.zero;
            To = Vector3.zero;
        }

        public override void OnTargetChange()
        {
            var target = (UnityEngine.Transform)Target;
            From = target.localPosition;
            To = target.localPosition;
        }
    }

    public class TransformScale : TransformTween
    {
        public static new string EditorName => "Transform Scale";
        public Vector3 From;
        public Vector3 To;

        public override void DrawBody()
        {
            From = EditorGUILayout.Vector3Field(new GUIContent(nameof(From)), From);
            To = EditorGUILayout.Vector3Field(new GUIContent(nameof(To)), To);
        }

        public override void Play(float time)
        {
            var obj = (Transform)Target;
            obj.localScale = Vector3.Lerp(From, To, Curve.Evaluate(Animation.AnimationProgress));
        }

        public override void ResetAnimation()
        {
            var obj = (Transform)Target;
            obj.localScale = Vector3.Lerp(From, To, Curve.Evaluate(0f));
        }

        public override void Reset()
        {
            base.Reset();
            Target = null;
            From = Vector3.one;
            To = Vector3.one;
        }

        public override void OnTargetChange()
        {
            var target = (UnityEngine.Transform)Target;
            From = target.localScale;
            To = target.localScale;
        }
    }

    public class TransformRotation : TransformTween
    {
        public static new string EditorName => "Transform Rotation";
        public Vector3 From;
        public Vector3 To;

        public override void DrawBody()
        {
            From = EditorGUILayout.Vector3Field(new GUIContent(nameof(From)), From);
            To = EditorGUILayout.Vector3Field(new GUIContent(nameof(To)), To);
        }

        public override void Play(float time)
        {
            var obj = (Transform)Target;
            var euler = Vector3.Lerp(From, To, Curve.Evaluate(Animation.AnimationProgress));
            obj.localRotation = Quaternion.Euler(euler.x, euler.y, euler.z);
        }

        public override void ResetAnimation()
        {
            var obj = (Transform)Target;
            var euler = Vector3.Lerp(From, To, Curve.Evaluate(0f));
            obj.localRotation = Quaternion.Euler(euler.x, euler.y, euler.z);
        }

        public override void Reset()
        {
            base.Reset();
            Target = null;
            From = Vector3.zero;
            To = Vector3.zero;
        }

        public override void OnTargetChange()
        {
            var target = (UnityEngine.Transform)Target;
            From = target.localRotation.eulerAngles;
            To = target.localRotation.eulerAngles;
        }
    }

    public class TweenAttribute : Attribute { }
}

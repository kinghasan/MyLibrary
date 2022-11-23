using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Aya.UI;

namespace Aya.TweenPro
{
    [Serializable]
    public class TweenAnimation
    {
        /// <summary>
        /// 动画时间
        /// </summary>
        [Min(1e-6f)] public float Duration;
        /// <summary>
        /// 播放次数
        /// </summary>
        [Min(1)] public int Count;

        public Editor Editor;
        public TweenPlayer Player;
        public SerializedProperty TweenDataProperty;
        public bool FoldOut;

        [SerializeReference]
        public List<TweenBase> TweenList = new List<TweenBase>();

        [TweenerProperty, NonSerialized] public SerializedProperty DurationProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty CountProperty;

        [TweenerProperty, NonSerialized] public SerializedProperty FoldOutProperty;

        /// <summary>
        /// 动画进度条
        /// </summary>
        public float AnimationProgress;
        public float PlayedTime;

        public SerializedObject SerializedObject => Editor.serializedObject;

        public void Awake()
        {
            foreach(var tween in TweenList)
            {
                //tween.Animation = this;
                tween.ResetAnimation();
            }
        }

        public void InitEditor(Editor editor,TweenPlayer player)
        {
            Editor = editor;
            Player = player;
            TweenDataProperty = SerializedObject.FindProperty("Animation");
            InitProperty();
        }

        /// <summary>
        /// 初始化属性字段
        /// </summary>
        public void InitProperty()
        {
            TweenerPropertyAttribute.Cache(this, TweenDataProperty);
        }

        /// <summary>
        /// 更新编辑器面板
        /// </summary>
        public void OnInspectorGUI()
        {
            DrawProgress();
            DrawProperties();
            SerializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 绘制进度条
        /// </summary>
        public void DrawProgress()
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.BeginHorizontal();
            var icon = EditorGUIUtility.FindTexture("PlayButton");
            if (GUILayout.Button(icon, EditorStyles.miniButtonMid, GUILayout.Width(EditorGUIUtility.singleLineHeight)))
            {
                Player.Play();
            }
            var rect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);
            var valuePos = Mathf.Round(rect.width * AnimationProgress);
            // Back
            EditorGUI.DrawRect(rect, Color.black);

            // Progress
            var rectProgress = rect;
            rectProgress.width = valuePos;
            EditorGUI.DrawRect(rectProgress, Color.green);
            GUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制属性
        /// </summary>
        public void DrawProperties()
        {
            using (GUIVertical.Create())
            {
                using (GUIHorizontal.Create())
                {
                    FoldOutProperty.boolValue = EditorGUILayout.Toggle(GUIContent.none, FoldOutProperty.boolValue, EditorStyles.foldout, GUILayout.Width(GUIUtil.CharacterWidth));
                    var btnTitle = GUILayout.Button("Animation", EditorStyles.boldLabel);
                    var btnFlexibleInfo = GUILayout.Button("", EditorStyles.label, GUILayout.MinWidth(0), GUILayout.MaxWidth(Screen.width));
                    if (btnTitle || btnFlexibleInfo)
                        FoldOutProperty.boolValue = !FoldOutProperty.boolValue;
                }

                if (FoldOut)
                {
                    var durationName = nameof(Duration);
                    EditorGUILayout.PropertyField(DurationProperty, new GUIContent(durationName, "动画持续时间"));
                    var countName = nameof(Count);
                    EditorGUILayout.PropertyField(CountProperty, new GUIContent(countName, "播放次数"));
                }
            }

            foreach (var tween in TweenList)
            {
                if (tween.Animation != this) tween.Animation = this;
                using (GUIVertical.Create())
                {
                    tween.OnInspectorGUI();
                }
            }

            DrawButton();
        }

        /// <summary>
        /// 绘制添加按钮
        /// </summary>
        public void DrawButton()
        {
            using (GUIVertical.Create())
            {
                var buttonRect = EditorGUILayout.GetControlRect();
                if (GUI.Button(buttonRect, "Add Tween"))
                {
                    AddTween(buttonRect);
                }
            }
            using (GUIVertical.Create())
            {
                if (GUILayout.Button("Reset"))
                {
                    Reset();
                }
            }
        }

        public void AddTween(Rect buttonRect)
        {
            var Root = new SearchableDropdownItem("Tween");
            var Drop = new SearchableDropdown(Root, (item) =>
            {
                var tweenType = item.Value as Type;
                var instance = Activator.CreateInstance(tweenType) as TweenBase;
                AddTween(instance);
             });

            var RootTweens = TypeCache.GetTypesWithAttribute(typeof(TweenAttribute));
            foreach (var tween in RootTweens)
            {
                var groupItem = new SearchableDropdownItem(tween.GetProperty("EditorName").GetValue(null).ToString())
                {
                    icon = EditorGUIUtility.FindTexture("PlayButton"),
                };

                var Tweens = TypeCache.GetTypesDerivedFrom(tween);
                foreach (var tweenIns in Tweens)
                {
                    var item = new SearchableDropdownItem(tweenIns.GetProperty("EditorName").GetValue(null).ToString(), tweenIns);

                    groupItem.AddChild(item);
                }

                Root.AddChild(groupItem);
            }

            Drop.Show(buttonRect);
        }

        public void Update(float deltaTime)
        {
            PlayedTime += deltaTime;
            AnimationProgress = PlayedTime / Duration;
            foreach(var tween in TweenList)
            {
                if (tween.Animation != this) tween.Animation = this;
                tween.UpdateAction?.Invoke();
                tween.Play(deltaTime);
            }

            if (AnimationProgress >= 1f)
            {
                Stop();
            }
        }

        public void Stop()
        {
            PlayedTime = 0f;
            AnimationProgress = 0f;
            Player.Stop();
        }

        public void Reset()
        {
            Duration = 1f;
            Count = 1;
            AnimationProgress = 0f;
            PlayedTime = 0f;
            TweenList.Clear();
        }

        public void AddTween(TweenBase tween)
        {
            tween.Animation = this;
            TweenList.Add(tween);
        }

        public T AddTween<T>()where T : Tween
        {
            var tween = Activator.CreateInstance<T>();
            AddTween(tween);
            return tween;
        }

        public T AddTween<T, TT>(TT Target) where T : Tween<TT> where TT : UnityEngine.Object
        {
            var tween = Activator.CreateInstance<T>();
            tween.Target = Target;
            AddTween(tween);
            return tween;
        }

        public void RemoveTween(TweenBase tween)
        {
            TweenList.Remove(tween);
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TweenerPropertyAttribute : Attribute
    {
        public string PropertyName { get; set; }
        public bool HasSubProperty { get; set; }

        public TweenerPropertyAttribute()
        {
            PropertyName = null;
            HasSubProperty = false;
        }

        public TweenerPropertyAttribute(string propertyName, bool hasSubProperty)
        {
            PropertyName = propertyName;
            HasSubProperty = hasSubProperty;
        }

        public static void Cache(object target,SerializedProperty targetProperty)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var properties = target.GetType().GetFields(bindingFlags);
            foreach (var field in properties)
            {
                var tweenerPropertyAttribute = field.GetCustomAttribute(typeof(TweenerPropertyAttribute)) as TweenerPropertyAttribute;
                if (tweenerPropertyAttribute == null) continue;
                var PropertyName = field.Name;
                var value = targetProperty.FindPropertyRelative(PropertyName.Replace("Property", ""));
                field.SetValue(target, value);
            }
        }
    }
}

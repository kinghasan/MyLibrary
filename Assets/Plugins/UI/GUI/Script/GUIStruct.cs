using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Aya.UI
{
    public static class GUIUtil
    {
        public static float CharacterWidth = GUI.skin.label.CalcSize(new GUIContent("X")).x;

        public static bool DrawHeaderContextMenuButton()
        {
            var button = GUILayout.Button("⁝", EditorStyles.label, GUILayout.Width(CharacterWidth));
            return button;
        }
    }

    /// <summary>
    /// 下拉框
    /// </summary>
    public class SearchableDropdown : AdvancedDropdown
    {
        public SearchableDropdownItem Root;
        public Action<SearchableDropdownItem> OnSelected;

        public SearchableDropdown(SearchableDropdownItem root, Action<SearchableDropdownItem> onSelected = null) : base(new AdvancedDropdownState())
        {
            Root = root;
            OnSelected = onSelected;
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            return Root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            OnSelected?.Invoke(item as SearchableDropdownItem);
        }
    }

    public class SearchableDropdownItem : AdvancedDropdownItem
    {
        public object Value;

        public SearchableDropdownItem(string name, object value = null) : base(name)
        {
            Value = value;
        }
    }

    /// <summary>
    /// 单列样式
    /// </summary>
    public struct GUIVertical : IDisposable
    {
        public GUIVertical(params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
        }

        public static GUIVertical Create(params GUILayoutOption[] options)
        {
            return new GUIVertical(options);
        }

        public void Dispose()
        {
            GUILayout.EndVertical();
        }
    }

    /// <summary>
    /// 单行样式
    /// </summary>
    public struct GUIHorizontal : IDisposable
    {
        public GUIHorizontal(params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal();
        }

        public static GUIHorizontal Create(params GUILayoutOption[] options)
        {
            return new GUIHorizontal(options);
        }

        public void Dispose()
        {
            GUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// 开启数据更改检测
    /// </summary>
    public struct GUICheck : IDisposable
    {
        public Action checkAction;

        public GUICheck(Action action, params GUILayoutOption[] options)
        {
            checkAction = action;
            EditorGUI.BeginChangeCheck();
        }

        public static GUICheck Create(Action action, params GUILayoutOption[] options)
        {
            return new GUICheck(action, options);
        }

        public void Dispose()
        {
            if (EditorGUI.EndChangeCheck())
            {
                checkAction?.Invoke();
            }
        }
    }

    /// <summary>
    /// 允许区域
    /// </summary>
    public struct GUIEnableArea : IDisposable
    {
        public bool OriginalEnable;

        public static GUIEnableArea Create(bool enable, bool inheritParent = true)
        {
            return new GUIEnableArea(enable, inheritParent);
        }

        private GUIEnableArea(bool enable, bool inheritParent = true)
        {
            OriginalEnable = GUI.enabled;
            if (!GUI.enabled && inheritParent) enable = false;
            GUI.enabled = enable;
        }

        public void Dispose()
        {
            GUI.enabled = OriginalEnable;
        }
    }
}

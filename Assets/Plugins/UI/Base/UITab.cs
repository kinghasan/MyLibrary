using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.UI
{
    [Serializable]
    public class UITabItem
    {
        public int Index { get; set; }
        public Button TabButton;
        public GameObject TabPanel;
        public Action OnTabSelect = delegate { };
        public Action OnTabDeselect = delegate { };
    }


    public class UITab : MonoBehaviour
    {
        public Color ActiveColor = Color.white;
        public Color DeActiveColor = Color.white;
        [TableList] public List<UITabItem> Tabs = new List<UITabItem>();

        public int Index { get; set; }

        protected void Awake()
        {
            for (var i = 0; i < Tabs.Count; i++)
            {
                var tab = Tabs[i];
                var button = tab.TabButton;
                tab.Index = i;
                button.onClick.AddListener(() =>
                {
                    ActiveTab(tab.Index);
                });
            }
            if (Index == 0)
            {
                Index = -1;
                ActiveTab(0);
            }
        }

        public void ActiveTab(int index)
        {
            if (index == Index) return;
            if (index >= Tabs.Count) return;
            if (Index >= 0 && Index < Tabs.Count) Tabs[Index].OnTabDeselect();
            for (var i = 0; i < Tabs.Count; i++)
            {
                Tabs[i].TabPanel.SetActive(i == index);
                var graphic = Tabs[i].TabButton.GetComponent<MaskableGraphic>();
                if (graphic != null)
                {
                    graphic.color = i == index ? ActiveColor : DeActiveColor;
                }
            }

            Tabs[index].OnTabSelect();
            Index = index;
        }
    }
}
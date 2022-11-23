using Aya.UI;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Aya.Data
{
    [CreateAssetMenu(fileName = "GameSetting", menuName = "Setting/Game Setting")]
    public class GameSetting : DataSetting<GameSetting>
    {
        [FoldoutGroup("Example")] public List<Example> ExampleList;

        public Example CurrentExample => GetData(ExampleList, SaveManager.Ins.ExampleLevel.Value);

        public T GetData<T>(List<T> list, int level) where T : SettingData
        {
            foreach (var data in list)
            {
                if (data.Level == level)
                    return data;
            }
            return null;
        }
    }

    public class gameList : UIList
    {
        public int num;
        public float ye;
    }

    [Serializable]
    public class SettingData
    {
        public int Level;
        public int Coin;
        public float Value;
    }

    [Serializable]
    public class Example : SettingData
    {

    }
}

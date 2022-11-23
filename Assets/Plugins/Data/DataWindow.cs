using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Aya.UI;

namespace Aya.Data
{
    public class DataWindow : EditorWindow
    {
        private static List<DataSetting> _DataList;
        private static List<DataSetting> DataList
        {
            get
            {
                if (_DataList == null) _DataList = GetDatas();
                return _DataList;
            }
        }
        private static int CurrentDataIndex;
        private static DataSetting CurrentData;

        [MenuItem("GameData/GameSetting")]
        public static void OpenWindow()
        {
            var window = GetWindow(typeof(DataWindow), false, "GameSetting");
        }

        private void OnGUI()
        {
            using (GUIHorizontal.Create())
            {
                var strArr = new string[DataList.Count];
                for(var i =0;i< strArr.Length; i++)
                {
                    strArr[i] = DataList[i].GetType().Name;
                }
                CurrentDataIndex = GUILayout.Toolbar(CurrentDataIndex, strArr);
            }

            var dataSetting = DataList[CurrentDataIndex];
            dataSetting.Init();
        }

        public static List<DataSetting> GetDatas()
        {
            var resultList = new List<DataSetting>();
            var datas = Resources.LoadAll<DataSetting>("Setting/");
            foreach(var data in datas)
            {
                resultList.Add(data);
            }
            return resultList;
        }
    }
}

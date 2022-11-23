using Aya.UI;
using UnityEngine;

namespace Aya.Data
{
    public abstract class DataSetting :  ScriptableObject
    {
        public virtual void Init()
        {
            var style = new GUIStyle();
            style.fontSize = 25;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Bold;
            SimpleGUIFunction.Label("”Œœ∑…Ë÷√", style);
            SimpleGUIFunction.Space(20);

            var type = GetType();
            this.DrawTypeField(type);
        }
    }
    public abstract class DataSetting<TSetting> : DataSetting where TSetting : DataSetting<TSetting>
    {
        #region Ins / Load

        public static TSetting Ins
        {
            get
            {
                if (_instance == null) _instance = Load();
                return _instance;
            }
        }

        private static TSetting _instance;

        public static TSetting Load()
        {
            var setting = Instantiate(Resources.Load<TSetting>("Setting/" + typeof(TSetting).Name));
            setting.Init();
            return setting;
        }

        #endregion
    }
}

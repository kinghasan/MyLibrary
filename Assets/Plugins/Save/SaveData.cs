using System;
using System.IO;
using UnityEngine;

namespace Aya.Save
{
    public static class SaveData
    {
        static SaveData()
        {
            DefaultModuleName = Application.productName;
            var savePath = SavePath;
        }

        /// <summary>
        /// 存储路径
        /// </summary>
        public static string SavePath
        {
            get
            {
                if (!string.IsNullOrEmpty(_savePath)) return _savePath;
                _savePath = Path.Combine(Application.persistentDataPath, "Save");
                if (!Directory.Exists(_savePath))
                {
                    Directory.CreateDirectory(_savePath);
                }

                return _savePath;
            }
        }

        private static string _savePath;

        /// <summary>
        /// 存储文件扩展名
        /// </summary>
        public static string SaveFileExt => ".sav";

        /// <summary>
        /// 默认模块名
        /// </summary>
        public static string DefaultModuleName { get; }

        public static void SetValue<T>(string key, T value)
        {
            var path = Path.Combine(SavePath, DefaultModuleName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, key + SaveFileExt);
            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
            }

            var json = value.ToString();
            File.WriteAllText(path, json);
        }

        public static string GetValue(string key)
        {
            var path = Path.Combine(SavePath, DefaultModuleName);
            if (!Directory.Exists(path)) return default(string);

            path = Path.Combine(path, key + SaveFileExt);
            if (!File.Exists(path)) return default(string);

            var result = File.ReadAllText(path);
            return result;
        }
    }

    [Serializable]
    public abstract class SaveValue<T> : SaveValue
    {
        public string SavePath => SaveData.SavePath;
        public T DefaultValue { get; set; }
        public T Value
        {
            get
            {
                if (!NeedUpdate) return _CurrentValue;
                var value = SaveData.GetValue(SaveKey);
                if (string.IsNullOrEmpty(value))
                {
                    _CurrentValue = DefaultValue;
                    Value = DefaultValue;
                }
                else
                {
                    _CurrentValue = Parse(value);
                }
                NeedUpdate = false;
                return _CurrentValue;
            }
            set
            {
                SaveData.SetValue<T>(SaveKey, value);
                NeedUpdate = true;
            }
        }
        private T _CurrentValue { get; set; }
        private bool NeedUpdate { get; set; }

        public SaveValue(string key,T defaultValue)
        {
            SaveKey = key;
            DefaultValue = defaultValue;
            NeedUpdate = true;
        }

        public abstract T Parse(string value);
    }

    public class SaveValue
    {
        public string SaveKey { get; set; }
    }
}
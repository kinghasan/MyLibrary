using Aya.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class UpgradeManager : GameEntity<UpgradeManager>
{
    public static string DataPath = "Config/";

    public Dictionary<Type, UpgradeInfo> UpgradeInfoDic = new Dictionary<Type, UpgradeInfo>();

    public UpgradeInfo GetInfo<T>() where T : UpgradeData
    {
        var dataType = typeof(T);
        if (UpgradeInfoDic.TryGetValue(dataType, out var info))
        {
            return info;
        }

        info = Load(dataType);

        return info;
    }

    public UpgradeInfo Load(Type dataType)
    {
        var textAsset = LoadDataAsset(dataType);
        return Load(dataType, textAsset);
    }

    /// <summary>
    /// 将表格文件转化为UpgradeInfo
    /// </summary>
    /// <param name="dataType"></param>
    /// <param name="textAsset"></param>
    /// <returns></returns>
    public UpgradeInfo Load(Type dataType,TextAsset textAsset)
    {
        if (textAsset == null)
        {
            throw new NullReferenceException(nameof(TextAsset) + " " + dataType.Name + " Is NULL");
        }

        var lines = textAsset.text.Split('\n');
        var listType = typeof(List<>).MakeGenericType(typeof(UpgradeData));
        var dataList = Activator.CreateInstance(listType) as IList;
        if (dataList == null) return default;

        var fieldList = new List<FieldInfo>();
        for (var rowIndex = 0; rowIndex < lines.Length; rowIndex++)
        {
            var line = lines[rowIndex];
            var str = line.Trim();
            if (string.IsNullOrEmpty(str)) continue;

            var strArray = str.Split(new[] { ',' }, StringSplitOptions.None);
            if (rowIndex == 0)
            {
                for (var columnIndex = 0; columnIndex < strArray.Length; columnIndex++)
                {
                    var fieldName = strArray[columnIndex];
                    var fieldInfo = dataType.GetField(fieldName);
                    fieldList.Add(fieldInfo);
                }
            }
            else
            {
                var data = default(object);
                data = Activator.CreateInstance(dataType);

                for (var columnIndex = 0; columnIndex < fieldList.Count; columnIndex++)
                {
                    var filedInfo = fieldList[columnIndex];
                    var value = strArray[columnIndex];
                    if (filedInfo == null || string.IsNullOrEmpty(value))
                    {
                        continue;
                    }

                    if (filedInfo.FieldType == typeof(int))
                    {
                        filedInfo.SetValue(data, value.AsInt());
                    }
                    else if (filedInfo.FieldType == typeof(long))
                    {
                        filedInfo.SetValue(data, value.AsLong());
                    }
                    else if (filedInfo.FieldType == typeof(float))
                    {
                        filedInfo.SetValue(data, value.AsFloat());
                    }
                    else if (filedInfo.FieldType == typeof(string))
                    {
                        filedInfo.SetValue(data, value.AsString());
                    }
                    else if (filedInfo.FieldType == typeof(double))
                    {
                        filedInfo.SetValue(data, value.AsDouble());
                    }
                    else if (filedInfo.FieldType == typeof(decimal))
                    {
                        filedInfo.SetValue(data, value.AsDecimal());
                    }
                    else if (filedInfo.FieldType.IsEnum)
                    {
                        filedInfo.SetValue(data, Enum.ToObject(filedInfo.FieldType, value.AsInt()));
                    }
                    else if (filedInfo.FieldType == typeof(Color))
                    {
                        ColorUtility.TryParseHtmlString(value, out var color);
                        filedInfo.SetValue(data, color);
                    }
                    else if (filedInfo.FieldType == typeof(Vector3))
                    {
                        var xyz = value.Replace("(", "").Replace(")", "").Split(new[] { ',', ' ' });
                        var x = xyz[0].CastType<float>();
                        var y = xyz[1].CastType<float>();
                        var z = xyz[2].CastType<float>();
                        var vector3 = new Vector3(x, y, z);
                        filedInfo.SetValue(data, vector3);
                    }
                    else if (filedInfo.FieldType.IsSubclassOf(typeof(UnityEngine.Object)))
                    {
                        var prefab = Resources.Load(value, filedInfo.FieldType);
                        filedInfo.SetValue(data, prefab);
                    }
                }

                //data.SetField("PrefixKey", prefixKey);
                dataList.Add(data);
            }
        }

        var infoType = typeof(UpgradeInfo<>).MakeGenericType(dataType);
        var info = Activator.CreateInstance(infoType) as UpgradeInfo;
        if (info == null) return default;

        info.GetType().GetField("DataList").SetValue(info, dataList);
        //info.Init(prefixKey);
        info.Init();
        UpgradeInfoDic.Add(dataType, info);

        return info;
    }

    /// <summary>
    /// 读取表格文件
    /// </summary>
    /// <param name="dataType"></param>
    /// <returns></returns>
    public TextAsset LoadDataAsset(Type dataType)
    {
        var path = DataPath;
        path += dataType.Name;
        var textAsset = Resources.Load<TextAsset>(path);

        return textAsset;
    }
}

public class UpgradeInfo
{
    public List<UpgradeData> DataList;
    public Dictionary<int, UpgradeData> DataDic;

    public virtual void Init()
    {
        DataDic = DataList.ToDictionary(d => d.ID);
    }
}

public class UpgradeInfo<T> : UpgradeInfo where T : UpgradeData
{

}

public class UpgradeData
{
    public int ID;
}

public class TestData : UpgradeData
{
    public int Index;
}
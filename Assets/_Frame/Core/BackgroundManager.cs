using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : GameEntity<BackgroundManager>
{
    public List<BackgroundData> BackgroundData;
    public BackgroundData Current { get; set; }

    public void Init()
    {
        foreach(var data in BackgroundData)
        {
            data.Background.SetActive(false);
        }
    }

    public void Switch(string key)
    {
        var data = GetData(key);
        if (data != null && data != Current)
        {
            if (Current != null)
                Current.Background.SetActive(false);
            Current = data;
            Current.Background.SetActive(true);
        }
    }

    public BackgroundData GetData(string key)
    {
        foreach(var data in BackgroundData)
        {
            if(data.Key == key)
            {
                return data;
            }
        }
        return null;
    }
}

[Serializable]
public class BackgroundData
{
    public string Key;
    public GameObject Background;
}

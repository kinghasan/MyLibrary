using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager _Instance;
    public static CameraManager Ins
    {
        get
        {
            return _Instance;
        }
    }

    public string DefaultCamera;

    public List<CameraData> CameraList = new List<CameraData>();
    public string CurrentKey { get; set; }
    public CameraData Current { get; set; }
    public Camera main => Camera.main;

    private void Awake()
    {
        _Instance = this;
        Switch(DefaultCamera);
    }

    public CameraData GetCamera(string key)
    {
        foreach(var data in CameraList)
        {
            if (data.Key == key)
                return data;
        }
        return null;
    }

    public void SetFollow(Transform target)
    {
        if (target == null) return;
        Current.Camera.Follow = target;
    }

    public void SetLookAt(Transform target)
    {
        if (target == null) return;
        Current.Camera.LookAt = target;
    }

    public void Switch(string key, Transform follow = null, Transform lookAt = null)
    {
        if (key == CurrentKey) return;
        var cam = GetCamera(key);
        if (cam == null) return;

        foreach (var data in CameraList)
        {
            if (data.Key == key)
            {
                Current = data;
                CurrentKey = key;
                data.Camera.gameObject.SetActive(true);
            }
            else
            {
                data.Camera.gameObject.SetActive(false);
            }
        }

        SetFollow(follow);
        SetLookAt(lookAt);
    }
}

[Serializable]
public class CameraData
{
    public string Key;
    public CinemachineVirtualCamera Camera;
}

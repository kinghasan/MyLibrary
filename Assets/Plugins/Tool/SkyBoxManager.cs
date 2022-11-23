using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.ToolsPlugins
{
    public class SkyBoxManager : MonoBehaviour
    {
        public static SkyBoxManager Ins { get; set; }

        public List<SkyboxData> SkyboxList;
        public string Default;

        public ChangeType Type;
        [ShowIf("Type", ChangeType.Camera)]
        public Skybox Skybox;

        public SkyboxData CurrentSkybox { get; set; }
        public List<SkyboxData> NeedCheckList { get; set; }

        private void Awake()
        {
            SkyBoxManager.Ins = this;
            Switch(Default);
            NeedCheckList = new List<SkyboxData>();
            foreach (var skybox in SkyboxList)
            {
                if (skybox.ChangeWithHeight)
                    NeedCheckList.Add(skybox);
            }
        }

        private void FixedUpdate()
        {
            var camera = Camera.main;
            var height = camera.transform.position.y;
            foreach (var skybox in NeedCheckList)
            {
                if (height >= skybox.Height.x && height < skybox.Height.y)
                {
                    Switch(skybox.Key);
                }
            }
            foreach (var valueData in CurrentSkybox.ValueList)
            {
                if (Time.realtimeSinceStartup - valueData.LastUpdateTime >= valueData.UpdateInterval)
                {
                    Material material = null;
                    if (Type == ChangeType.RenderSettings)
                        material = RenderSettings.skybox;
                    else if (Type == ChangeType.Camera)
                        material = Skybox.material;

                    if (valueData.ChangeType == ChangeValueType.Lerp)
                    {
                        var T = 0f;
                        if (valueData.LerpPosT == PosTarget.x)
                        {
                            T = camera.transform.position.x;
                        }
                        else if (valueData.LerpPosT == PosTarget.y)
                        {
                            T = camera.transform.position.y;
                        }
                        else if (valueData.LerpPosT == PosTarget.z)
                        {
                            T = camera.transform.position.z;
                        }

                        if (T < valueData.LerpMinT || T > valueData.LerpMaxT) continue;
                        T -= valueData.LerpMinT;
                        var lerp = T / (valueData.LerpMaxT - valueData.LerpMinT);

                        var value = Mathf.LerpUnclamped(valueData.LerpRange.x, valueData.LerpRange.y, lerp);

                        if (valueData.Type == ValueType.Int)
                        {
                            material.SetInt(valueData.ValueName, (int)value);
                        }
                        else if (valueData.Type == ValueType.Float)
                        {
                            material.SetFloat(valueData.ValueName, value);
                        }
                    }
                    else if (valueData.ChangeType == ChangeValueType.Add)
                    {
                        if (valueData.Type == ValueType.Int)
                        {
                            var intV = material.GetInt(valueData.ValueName);
                            intV += valueData.IntValue;
                            material.SetInt(valueData.ValueName, intV);
                        }
                        else if (valueData.Type == ValueType.Float)
                        {
                            var intV = material.GetFloat(valueData.ValueName);
                            intV += valueData.FloatValue;
                            material.SetFloat(valueData.ValueName, intV);
                        }
                    }
                    else if (valueData.ChangeType == ChangeValueType.Color)
                    {
                        var T = 0f;
                        if (valueData.ColorPosT == PosTarget.x)
                        {
                            T = camera.transform.position.x;
                        }
                        else if (valueData.ColorPosT == PosTarget.y)
                        {
                            T = camera.transform.position.y;
                        }
                        else if (valueData.ColorPosT == PosTarget.z)
                        {
                            T = camera.transform.position.z;
                        }

                        if (T < valueData.ColorMinT || T > valueData.ColorMaxT) continue;
                        T -= valueData.ColorMinT;
                        var lerp = T / (valueData.ColorMaxT - valueData.ColorMinT);

                        var value = Color.LerpUnclamped(valueData.ColorRange.StartColor, valueData.ColorRange.EndColor, lerp);

                        material.SetColor(valueData.ValueName, value);
                    }

                    valueData.LastUpdateTime = Time.realtimeSinceStartup;
                }
            }
        }

        public void Switch(string key)
        {
            if (CurrentSkybox != null && CurrentSkybox.Key == key) return;
            foreach (var skybox in SkyboxList)
            {
                if (skybox.Key == key)
                {
                    CurrentSkybox = skybox;
                    if (Type == ChangeType.RenderSettings)
                        RenderSettings.skybox = CurrentSkybox.Material;
                    else if (Type == ChangeType.Camera)
                        Skybox.material = CurrentSkybox.Material;
                }
            }
        }
    }

    [Serializable]
    public class SkyboxData
    {
        public string Key;
        public Material Material;
        public bool ChangeWithHeight;
        [ShowIf("ChangeWithHeight")]
        public Vector2 Height;
        public List<SkyboxValueData> ValueList;
    }

    [Serializable]
    public class SkyboxValueData
    {
        [HideIf("ChangeType", ChangeValueType.Color)] public ValueType Type;
        public ChangeValueType ChangeType;
        public string ValueName;
        [ShowIf("ChangeType", ChangeValueType.Lerp)] public Vector2 LerpRange;
        [ShowIf("ChangeType", ChangeValueType.Lerp)] public PosTarget LerpPosT;
        [ShowIf("ChangeType", ChangeValueType.Lerp)] public float LerpMinT;
        [ShowIf("ChangeType", ChangeValueType.Lerp)] public float LerpMaxT;
        [ShowIf("ChangeType", ChangeValueType.Add)] public int IntValue;
        [ShowIf("ChangeType", ChangeValueType.Add)] public float FloatValue;
        [ShowIf("ChangeType", ChangeValueType.Color)] public ColorData ColorRange;
        [ShowIf("ChangeType", ChangeValueType.Color)] public PosTarget ColorPosT;
        [ShowIf("ChangeType", ChangeValueType.Color)] public float ColorMinT;
        [ShowIf("ChangeType", ChangeValueType.Color)] public float ColorMaxT;
        public float UpdateInterval;
        public float LastUpdateTime { get; set; }
    }

    [Serializable]
    public class ColorData
    {
        public Color StartColor;
        public Color EndColor;
    }

    [Serializable]
    public enum PosTarget
    {
        x,
        y,
        z
    }

    [Serializable]
    public enum ValueType
    {
        Int,
        Float,
    }

    [Serializable]
    public enum ChangeValueType
    {
        Lerp,
        Add,
        Color,
    }

    [Serializable]
    public enum ChangeType
    {
        RenderSettings,
        Camera,
    }
}
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : GameEntity
{
    public bool debugUV = true;
    public int textureSize = 1024;

    public Color BaseColor { get; set; }

    private Renderer rend;
    private RenderTexture maskTexture;
    private RenderTexture copyTexture;

    private int maskTextureID = Shader.PropertyToID("_MaskTex");

    public Renderer GetRenderer() => rend;
    public RenderTexture GetMask() => maskTexture;
    public RenderTexture GetCopy() => copyTexture;

    void Start()
    {
        maskTexture = new RenderTexture(textureSize, textureSize, 0);
        maskTexture.filterMode = FilterMode.Bilinear;

        copyTexture = new RenderTexture(textureSize, textureSize, 0);
        copyTexture.filterMode = FilterMode.Bilinear;
        PainterManager.Ins.ClearRT(copyTexture);

        rend = GetComponent<Renderer>();
        rend.material.SetTexture(maskTextureID, copyTexture);

        BaseColor = rend.material.color;

        if (debugUV)
        {
            PainterManager.Ins.InitUVMask(this);
        }
    }

    [Button("Clear")]
    public void Clear()
    {
        PainterManager.Ins.ClearRT(copyTexture);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        maskTexture.Release();
        copyTexture.Release();
    }
}

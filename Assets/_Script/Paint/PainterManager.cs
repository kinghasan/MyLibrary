using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PainterManager : GameEntity<PainterManager>
{
    public Shader texturePaint;
    private Material paintMaterial;

    int debugUVID = Shader.PropertyToID("_DebugUV");
    int positionID = Shader.PropertyToID("_Position");
    int hardnessID = Shader.PropertyToID("_Hardness");
    int strengthID = Shader.PropertyToID("_Strength");
    int radiusID = Shader.PropertyToID("_Radius");
    int colorID = Shader.PropertyToID("_Color");
    int textureID = Shader.PropertyToID("_MainTex");

    CommandBuffer command;

    protected override void Awake()
    {
        base.Awake();
        paintMaterial = new Material(texturePaint);

        command = new CommandBuffer();
        command.name = "CommmandBuffer - " + gameObject.name;
    }

    public void InitUVMask(Paintable paintable)
    {
        RenderTexture mask = paintable.GetMask();
        RenderTexture copy = paintable.GetCopy();
        Renderer rend = paintable.GetRenderer();

        paintMaterial.SetInt(debugUVID, 1);

        command.SetRenderTarget(mask);
        command.SetRenderTarget(copy);
        command.DrawRenderer(rend, paintMaterial, 0);

        Graphics.ExecuteCommandBuffer(command);
        command.Clear();
    }

    public void Paint(Paintable paintable, Vector3 pos, float radius = 10f, float hardness = 0.5f, float strength = 0.5f, Color color = default(Color))
    {
        RenderTexture mask = paintable.GetMask();
        RenderTexture copy = paintable.GetCopy();
        Renderer rend = paintable.GetRenderer();

        paintMaterial.SetInt(debugUVID, 0);
        paintMaterial.SetVector(positionID, pos);
        paintMaterial.SetColor(colorID, color);
        paintMaterial.SetFloat(hardnessID, hardness);
        paintMaterial.SetFloat(strengthID, strength);
        paintMaterial.SetFloat(radiusID, radius);
        paintMaterial.SetTexture(textureID, copy);

        //paintMaterial.SetInt(debugUVID, 1);

        command.SetRenderTarget(mask);
        command.DrawRenderer(rend, paintMaterial, 0);
        command.SetRenderTarget(copy);
        command.Blit(mask, copy);

        Graphics.ExecuteCommandBuffer(command);
        command.Clear();
    }
}

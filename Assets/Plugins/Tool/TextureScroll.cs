using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public float scrollSpeedU = 1f;  // U方向的移动速度
    public float scrollSpeedV = 1f;  // V方向的移动速度

    private Renderer render;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void Update()
    {
        float offsetU = Time.time * scrollSpeedU;
        float offsetV = Time.time * scrollSpeedV;

        Vector2 offset = new Vector2(offsetU, offsetV);
        render.material.SetTextureOffset("_MainTex", offset);
    }
}
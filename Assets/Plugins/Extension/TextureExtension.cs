using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class TextureExtension
    {
        /// <summary>
        /// RenderTexture×ªTexture2D
        /// </summary>
        /// <param name="renderT"></param>
        /// <returns></returns>
        public static Texture2D RenderTextureToTexture2D(RenderTexture renderT)
        {
            var texture2d = new Texture2D(renderT.width, renderT.height, TextureFormat.ARGB32, false);
            RenderTexture.active = renderT;
            texture2d.ReadPixels(new UnityEngine.Rect(0, 0, renderT.width, renderT.height), 0, 0);
            texture2d.Apply();
            return texture2d;
        }

        /// <summary>
        /// ±éÀúÍ¼Æ¬µÄÏñËØ
        /// </summary>
        /// <param name="tex2d"></param>
        /// <param name="function"></param>
        public static void ForeachPixel(this Texture2D tex2d, Action<Color> function)
        {
            for(var i = 0; i < tex2d.width; i++)
            {
                for(var j = 0; j < tex2d.height; j++)
                {
                    var color = tex2d.GetPixel(i, j);
                    function?.Invoke(color);
                }
            }
        }
    }
}

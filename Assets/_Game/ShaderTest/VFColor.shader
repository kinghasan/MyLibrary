Shader "Test/VFColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OffTex ("OffTexture", 2D) = "white" {}
        _OffColor ("OffColor", Color) =  (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag2
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag2 (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag2
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _OffTex;
            float4 _MainTex_ST;
            float4 _OffColor;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.x += 5;
                v.vertex.y += 5;
                v.vertex.z += 5;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag2 (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_OffTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                if(i.uv.x > 0.5f && i.uv.y > 0.5f)
                {
                    col *= _OffColor;
                }
                return col;
            }
            ENDCG
        }

        Pass
        {
            Tags{"LightMode" = "ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag2
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _OffTex;
            float4 _MainTex_ST;
            float4 _OffColor;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.y += 10;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag2 (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_OffTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                if(i.uv.x > 0.5f && i.uv.y > 0.5f)
                {
                    col *= _OffColor;
                }
                return col;
            }
            ENDCG
        }
    }
}

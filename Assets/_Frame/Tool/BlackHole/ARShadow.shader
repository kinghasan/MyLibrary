 
 
Shader "Custom/WaterFlow"
{
	Properties{
		_MainTex("MainTex",2D) = "white"{}//�����ͼ
		_Color("Color Tint",Color) = (1,1,1,1)//����������ɫ
		_Specular("Specular",Color) =(1,1,1,1)//���Ƹ߹ⷴ����ɫ
		_Gloss("Gloss",Range(1,100))=10//���Ƹ߹������С
		_Magnitude("Magnitude",Float) = 0.1//���Ʋ���Ƶ��
		_Frequency("Frequency",Float) = 0.5//���Ʋ������ȣ��ο����Ҳ���Ƶ�ʷ��������
		_Speed("Speed", Float) = 0.01//���������ٶ�
		_AlphaScale("Alpha Scale",Range(0,1)) = 0.65//͸���Ȼ���е�͸����ϵ��
        _Radius ("Radius", Range(0.0, 10.0)) = 2.0
        _CirclePosition ("Circle Position", Vector) = (0, 0, 0, 0)
	}
 
		SubShader{	
 
		Pass{
		//ָ��ǰ����Ⱦģʽ
		Tags{"LightMode" = "ForwardBase" "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True" }
 
		ZWrite On//�ر���ȶ�д
		Blend SrcAlpha OneMinusSrcAlpha//�������ģʽ
		Cull Off//�ر��޳�����
 
		CGPROGRAM
 
		#pragma vertex vert
		#pragma fragment frag
        #pragma multi_compile_fwdbase//��֤��Shader��ʹ�õĹ���˥���ȹ��ձ������Ա���ȷ��ֵ
 
		#include "UnityCG.cginc"
        #include "Lighting.cginc"
        #include "AutoLight.cginc"//���Unity�������ļ�������������Ӱ���õĺ�
        
		//����Properties�еı���
		sampler2D _MainTex;
		float4 _MainTex_ST;//��������ź�ƫ��ֵ,TRANSFORM_TEX�����
		fixed4 _Color;
		fixed4 _Specular;
		float _Gloss;
		float _Magnitude;
		float _Frequency;
		float _Speed;
		float _AlphaScale;
        float _Radius;
        float4 _CirclePosition;
 
		struct a2v {
			float4 vertex:POSITION;
			float2 texcoord:TEXCOORD0;
			float3 normal:NORMAL;
		};
 
		struct v2f {
			float4 pos:SV_POSITION;
			float2 uv:TEXCOORD0;//ʹ�õ�һ����ֵ�Ĵ���
			float3 worldNormal:TEXCOORD1;//ʹ�õڶ�����ֵ�Ĵ���
			float3 worldPos:TEXCOORD2;
			SHADOW_COORDS(3)//����һ�����ڶ���Ӱ�������������,��ʾʹ�õ�4����ֵ�Ĵ���
		};
 
		v2f vert(a2v v) {
			v2f o;
 
			o.worldNormal = UnityObjectToWorldNormal(v.normal);//����ռ��¶��㷨��
			o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
 
			//float4 offset = float4(0, 0, 0, 0);//����ƫ��
			//offset.y = sin(_Frequency *_Time.y+ v.vertex.x+ v.vertex.y+ v.vertex.z)*_Magnitude;//����Y������ʱ��ƫ��
			o.pos = UnityObjectToClipPos(v.vertex);//�����ģ�Ϳռ䵽�ü��ռ�
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);//����UV����
			//o.uv += float2(0, _Time.y*_Speed);//��������ˮƽ�����ϵ��ƶ�
 
			TRANSFER_SHADOW(o);//����������Ӱ��������
			return o;
		}
 
		fixed4 frag(v2f i) :SV_Target{
 
 
            float distanceToCircle = distance(_CirclePosition.xyz, i.worldPos);
            if (distanceToCircle < _Radius) {
				return fixed4(0,0,0,0);//�ı�͸��ͨ����ֵ
            } else {
			fixed3 worldNormal = normalize(i.worldNormal);//����ռ��¶��㷨��
			fixed3 worldLight = UnityWorldSpaceLightDir(i.worldPos);//����ռ��¶��㴦�������
			fixed4 texColor = tex2D(_MainTex, i.uv);
			//if(i.uv_MainTex.y<-1){
			//	texColor.rgb = (0.5,0.5,0.5);
			//}
			fixed3 albedo = texColor.rgb*_Color.rgb;//���������ȡ��������ɫ
			fixed3 diffuse = _LightColor0.rgb * albedo * (dot(worldLight, worldNormal)*0.5 + 0.5);//��������ģ�ͼ���������
 
			fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;//������
 
			//����߹ⷴ�䣬Blinn-Phongģ��
			fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));//�۲췽��
			fixed3 halfDir = normalize(worldLight + viewDir);
			fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(halfDir, worldNormal)), _Gloss);
			
			UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);//��ȡ����˥������Ӱ

				return fixed4(albedo * atten,texColor.a*_AlphaScale);
				//return fixed4(ambient + (diffuse + specular) * atten,texColor.a*_AlphaScale);
            }
		}
 
		ENDCG
		    }
		 }
	FallBack "VertexLit"//��ò�Ҫ����Fallback
}
 
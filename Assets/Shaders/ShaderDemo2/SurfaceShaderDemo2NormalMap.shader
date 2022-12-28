Shader "Akash/SurfaceShaderDemo2NormalMap" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Brightness("Brightness",Range(0,2))= 1
		_NormalMap("Normal", 2D) = "white"{}
		_NormalAmount("Bump Amount", Range(0,5))= 1
	}
	
	SubShader
	{
		Tags {"RenderType" = "Opaque"}
		LOD 200
		
		CGPROGRAM

		#pragma surface surf Lambert
		#pragma target 3.0

		sampler2D _MainTex;
		fixed3 _Color;
		sampler2D _Normal;
		half _Brightness;
		half _NormalAmount;

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_NormalMap;
		};

		void surf (Input i,inout SurfaceOutput o)
		{
			o.Albedo = tex2D(_MainTex,i.uv_MainTex) * _Color;
			o.Normal = UnpackNormal(tex2D(_Normal,i.uv_NormalMap));
			o.Normal *=	half3(_NormalAmount,_NormalAmount,1); 
		}
		ENDCG
	}
}
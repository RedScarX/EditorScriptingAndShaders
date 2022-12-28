Shader "Akash/SurfaceShaderDemo1"
{
    Properties
    {
        _Color("Colour",Color) = (1,1,1,1)
        _MainTex("MainTexture",2D) = "white" {}
        _Range("Range",range(0,5)) = .5
        _CubeMap("Cube Map",CUBE) = "" {}
        _FLoat("Example float",float) = 1
        _Vector("Example vector",vector) = (1,1,1)
        
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Lambert
        #pragma target 3.0

        sampler2D _MainTex;
        fixed _Range;
        fixed4 _Color;
        samplerCUBE _CubeMap;
        float _Float;
        float3 _Vector;

        struct Input 
        {
            float2 uv_MainTex;
            float3 worldRefl;
            
        };

        void surf(Input i,inout SurfaceOutput o)
        {
            o.Albedo = (tex2D(_MainTex,i.uv_MainTex)) * _Color;
            o.Emission = texCUBE(_CubeMap,i.worldRefl);
        }
        ENDCG
    }
}

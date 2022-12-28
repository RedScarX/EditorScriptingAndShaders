Shader "Unlit/ShaderLerpDemo2"
{
    Properties 
    {
        _ColorA("Color A", color) = (1,1,1,1)
        _ColorB("Color B",color) = (0,0,0,1)
    }
    Subshader
    {
        Tags {"RenderType" = "Opaque"}
        LOD 100
        
        Pass 
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _ColorA;
            fixed4 _ColorB;

            fixed4 frag (v2f_img i) : SV_Target
            {
                float time = sin(_Time.y + 1) /2;
                float3 color = lerp(_ColorA,_ColorB,time);
                return fixed4(color,1);
            }
            ENDCG
        }
    }
}

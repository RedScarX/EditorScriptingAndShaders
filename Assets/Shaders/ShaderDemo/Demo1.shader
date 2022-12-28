Shader "Akash/Demo1"
{
    Properties
    {
    }
    SubShader 
    {
        
        Tags {"RenderType" = "Opaque"}
        LOD 100
        
        Pass 
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            fixed4 frag (v2f_img i) : SV_Target
            {
                float4 color = float4(1,1,sin(_Time.y+1)/2,1);
                return fixed4(color);
            }
            ENDCG 
        }
            
    }
}
Shader "Custom/Brightness Effect" {
 Properties {
     _MainTex ("Base (RGB)", 2D) = "white" {}
     _Brightness ("Brightness", float) = 1
     _Contrast ("Contrast", float) = 1
 }
 
 SubShader {
     Pass {
         ZTest Always Cull Off ZWrite Off
                 
         CGPROGRAM
         #pragma vertex vert_img
         #pragma fragment frag
         #include "UnityCG.cginc"
 
         uniform sampler2D _MainTex;
         uniform float _Brightness;
         uniform float _Contrast;
 
         fixed4 frag (v2f_img i) : SV_Target
         {
             fixed4 output = tex2D(_MainTex, i.uv);
             output = output * _Brightness;
             output = (output - 0.5) * _Contrast + 0.5;
             return output;
         }
         ENDCG
     }
 }
 
 Fallback off
 }
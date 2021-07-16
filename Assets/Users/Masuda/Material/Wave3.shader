Shader "Unlit/Wave3"
{
    Properties
    {
        _MainTex ("Select Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture",2D) = "white"{}
        _Ratio ("ratio", Range(0, 1)) = 0.1
    }
    
    SubShader
    {
        Tags
        { 
            "Queue"="AlphaTest"
            "IgnoreProjector"="True"
            "RenderType"="TransparentCutout"
            "PreviewType"="Sphere"
            "CanUseSpriteAtlas"="True"
        }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };
 
            struct v2f
            {
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            sampler2D _MaskTex;
            float4 _MainTex_ST;
            float _Ratio;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv1 = TRANSFORM_TEX(v.uv1, _MainTex);
                o.uv2 = v.uv2;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mask = tex2D(_MaskTex,i.uv2);
                clip(mask.a - 0.5);
                fixed4 col = tex2D(_MainTex,i.uv1);
                return col * mask;

                float t = _Ratio + sin(_Time.w + i.uv1.x * 8) / 40;                

                if (i.uv1.y < t)
                {
                    return fixed4(.6, .6, .6, 0.2);
                    
                }

                return tex2D(_MainTex, i.uv1);
                //return step(radius, r);
            }
            ENDCG
        }
    }
}
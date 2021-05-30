Shader "Custom/Unlit/Wave"
{
    Properties
    {

        _MainTex ("Select Texture", 2D) = "black" {}
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
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Ratio;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed radius = 0.5;
                fixed r = distance(i.uv, fixed2(0.5,0.5));
                float t = _Ratio + sin(_Time.w + i.uv.x * 8) / 40;                

                if (i.uv.y < t)
                {
                    return fixed4(.6, .6, .6, 0.2);
                    
                }

                return smoothstep(radius, radius+0.02, r);
                //return tex2D(_MainTex, i.uv);
                //return step(radius, r);
            }
            ENDCG
        }
    }
}

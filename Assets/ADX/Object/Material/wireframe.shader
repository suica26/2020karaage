// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/wireframe"
{
    Properties
    {
        _Color ("Color", Color) = (0.5, 0, 0.5, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend SrcAlpha One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                return o;
            }

            half4 frag (v2f i) : COLOR
            {
                return _Color;
            }
            ENDCG
        }
    }
FallBack Off
}

Shader "Custom/Crack"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Normal", 2D) = "bump" {}
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0.0
        _CrackTex ("Crack (Grayscale)", 2D) = "white" { }
        _Progress ("Crack Progress", Range(0, 1)) = 1.0
        _CrackHeight ("Crack Height", Range(0, 0.1)) = 0.05
    }

    SubShader
    {
        Tags { "Queue" = "AlphaTest" "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _CrackTex;
        float4 _CrackTex_TexelSize;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_CrackTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Progress;
        float _CrackHeight;

        #define PROGRESS_EXPONENT 2.0
        #define CAVITY_EXPONENT 2.0

        inline float getAlpha(float2 uv, float2 edge)
        {
            return 1.0 - saturate(tex2D(_CrackTex, uv).r + smoothstep(edge.x, edge.y, uv.x));
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float2 edge = pow(_Progress, float2(PROGRESS_EXPONENT, 1.0 / PROGRESS_EXPONENT));
            float alpha = getAlpha(IN.uv_CrackTex, edge);
            clip(alpha - 0.00001);
            float cavity = 1.0 - pow(alpha, CAVITY_EXPONENT);
            float alphaYP = getAlpha(IN.uv_CrackTex + float2(0.0, _CrackTex_TexelSize.y), edge);
            float alphaXP = getAlpha(IN.uv_CrackTex + float2(_CrackTex_TexelSize.x, 0.0), edge);
            float alphaYN = getAlpha(IN.uv_CrackTex + float2(0.0, -_CrackTex_TexelSize.y), edge);
            float alphaXN = getAlpha(IN.uv_CrackTex + float2(-_CrackTex_TexelSize.x, 0.0), edge);
            float3 tx = float3(_CrackTex_TexelSize.x * 2.0, 0.0, (alphaXN - alphaXP) * _CrackHeight);
            float3 ty = float3(0.0, _CrackTex_TexelSize.y * 2.0, (alphaYN - alphaYP) * _CrackHeight);
            float3 normal = normalize(normalize(cross(tx, ty)) + UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)));
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb * cavity;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            o.Normal = normal;
            o.Occlusion = cavity;
        }
        ENDCG

    }
    FallBack "Diffuse"
}
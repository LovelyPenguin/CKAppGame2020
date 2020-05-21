Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Outline_Bold("OutLineBold", Float) = 0.015
    }
    SubShader
    {
        Tags { "RenderType"="CutOff" "Queue" = "CutOff"}
        LOD 100

        Pass
        {
            cull front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float _Outline_Bold;

            v2f vert (appdata v)
            {
                v2f o;

                float3 fNormalized_Normal = normalize(v.normal);
                float3 fOutline_Position = v.vertex;
                //float3 fOutline_Position = v.vertex + fNormalized_Normal * (_Outline_Bold);
                // ㄴ주석 해제시 카툰렌더링 적용됨

                o.vertex = UnityObjectToClipPos(fOutline_Position);

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return 0.0f;
            }
                ENDCG
        }
            cull back
            CGPROGRAM

            #pragma surface surf _Custom noambient

            sampler2D _MainTex;

            struct Input
            {
                float2 uv_MainTex;
            };

            
            void surf(Input IN, inout SurfaceOutput o)
            {
                float4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }

            float4 Lighting_Custom(SurfaceOutput s, float3 lightDir, float3 viewDir, float attan)
            {
                float4 fFinalColor;
                fFinalColor.rgb = s.Albedo;
                fFinalColor.a = s.Alpha;
                return fFinalColor;
            }

            ENDCG
    }
}

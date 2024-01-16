Shader "Unlit/StaminaShader"
{
    Properties
    {
        _MainTex ("Texture",2D) = "White" {}
        _CircleTex("Texture",2D) = "White" {}
        _Stamina ("Stamina", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            sampler2D _CircleTex;
            float4 _CircleTex_ST;

            sampler2D _MainTex;
            float4 _MainTex_ST;


            float _Stamina;

            struct Meshdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            Interpolators vert (Meshdata v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(Interpolators i) : SV_Target
            {
                float4 col = tex2D(_CircleTex, i.uv);

                return col;
            }
            ENDCG
        }
    }
}

Shader "Custom/SeeThroughShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlayerPosition ("Player Position", Vector) = (0,0,0,0)
        _Radius ("Radius", float) = 5.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _PlayerPosition;
            float _Radius;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate distance between pixel and player
                float distance = length(i.worldPos - _PlayerPosition.xyz);
                
                // Smooth transparency based on distance
                float transparency = smoothstep(_Radius * 0.8, _Radius, distance);

                // Sample texture and apply transparency
                fixed4 col = tex2D(_MainTex, i.uv);
                col.a *= transparency;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/VertexLit"
}

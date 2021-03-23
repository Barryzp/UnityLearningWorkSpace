Shader "Barry_Shaders/LightningBasicDiffuseShader"
{
    Properties
    {
        _Texture ("Texture", 2D) = "white" {}
        _EmissiveColor ("Emissive Color",Color) = (1,1,1,1)
        _AmbientColor("Ambient Color",Color) = (1,1,1,1)
        _MySliderValue("Slider Value",Range(0,3)) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf BasicDiffuse

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
        };

        float4 _EmissiveColor;
        float4 _AmbientColor;
        float _MySliderValue;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        // 光照模型
        inline float4 LightingBasicDiffuse(SurfaceOutput s,fixed3 lightDir,fixed atten){
            float difLight = dot(s.Normal,lightDir);
            float hLambert = difLight * 0.5 + 0.5;

            float4 col=(0,0,0,0);
            // _LightColor0 这个应该是光源
            col.rgb = s.Albedo * _LightColor0.rgb * (hLambert * atten * 2);
            col.a = s.Alpha;
            return col;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            float4 c;
            c = pow((_EmissiveColor + _AmbientColor),_MySliderValue);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

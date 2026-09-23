Shader "Custom/URP/HorizonWater"
{
    Properties
    {
        _WaterColor ("Water Color", Color) = (0.02, 0.65, 0.8, 1)
        _HorizonColor ("Horizon Color", Color) = (0.05, 0.78, 0.95, 1)

        _HorizonDistance ("Horizon Distance", Float) = 40
        _HorizonSoftness ("Horizon Softness", Float) = 30

        _WaveStrength ("Wave Strength", Float) = 0.05
        _WaveSpeed ("Wave Speed", Float) = 0.5
        _WaveScale ("Wave Scale", Float) = 2

        _Smoothness ("Smoothness", Range(0,1)) = 0.8
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Geometry"
        }

        Pass
        {
            Name "ForwardLit"

            Tags
            {
                "LightMode"="UniversalForward"
            }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            CBUFFER_START(UnityPerMaterial)

                float4 _WaterColor;
                float4 _HorizonColor;

                float _HorizonDistance;
                float _HorizonSoftness;

                float _WaveStrength;
                float _WaveSpeed;
                float _WaveScale;

                float _Smoothness;

            CBUFFER_END


            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                VertexPositionInputs positionInputs =
                    GetVertexPositionInputs(IN.positionOS.xyz);

                VertexNormalInputs normalInputs =
                    GetVertexNormalInputs(IN.normalOS);

                OUT.positionHCS = positionInputs.positionCS;
                OUT.positionWS = positionInputs.positionWS;
                OUT.normalWS = normalInputs.normalWS;
                OUT.uv = IN.uv;

                return OUT;
            }


            half4 frag(Varyings IN) : SV_Target
            {
                // ----------------------------------------------------
                // 1. Расстояние от камеры
                // ----------------------------------------------------

                float distanceToCamera =
                    distance(IN.positionWS, _WorldSpaceCameraPos);


                // ----------------------------------------------------
                // 2. Плавный переход Water -> Horizon
                // ----------------------------------------------------

                float horizon =
                    smoothstep(
                        _HorizonDistance,
                        _HorizonDistance + _HorizonSoftness,
                        distanceToCamera
                    );


                // ----------------------------------------------------
                // 3. Небольшая рябь
                // ----------------------------------------------------

                float wave =
                    sin(
                        IN.positionWS.x * _WaveScale +
                        _Time.y * _WaveSpeed
                    )
                    *
                    cos(
                        IN.positionWS.z * _WaveScale * 0.7 +
                        _Time.y * _WaveSpeed * 0.8
                    );

                wave *= _WaveStrength;


                // ----------------------------------------------------
                // 4. Немного меняем цвет воды
                // ----------------------------------------------------

                float3 waterColor =
                    _WaterColor.rgb + wave;


                // ----------------------------------------------------
                // 5. Смешиваем воду с цветом горизонта
                // ----------------------------------------------------

                float3 finalColor =
                    lerp(
                        waterColor,
                        _HorizonColor.rgb,
                        horizon
                    );


                // ----------------------------------------------------
                // 6. Освещение URP
                // ----------------------------------------------------

                InputData lightingInput = (InputData)0;

                lightingInput.positionWS = IN.positionWS;
                lightingInput.normalWS = normalize(IN.normalWS);
                lightingInput.viewDirectionWS =
                    normalize(_WorldSpaceCameraPos - IN.positionWS);

                lightingInput.shadowCoord =
                    TransformWorldToShadowCoord(IN.positionWS);

                SurfaceData surfaceData = (SurfaceData)0;

                surfaceData.albedo = finalColor;
                surfaceData.metallic = 0;
                surfaceData.specular = 0.2;
                surfaceData.smoothness = _Smoothness;
                surfaceData.normalTS = float3(0, 0, 1);
                surfaceData.occlusion = 1;
                surfaceData.emission = 0;
                surfaceData.alpha = 1;

                half4 color = UniversalFragmentPBR(
                    lightingInput,
                    surfaceData
                );

                return color;
            }

            ENDHLSL
        }
    }
}
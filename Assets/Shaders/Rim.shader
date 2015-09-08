  Shader "Custom/Rim" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
      _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
      _Detail ("Detail", 2D) = "gray" {}
      [MaterialToggle] _isGrayscale("is Grayscale", Float) = 0
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float3 viewDir;
          float4 screenPos;
      };
      sampler2D _MainTex;
      sampler2D _BumpMap;
      sampler2D _Detail;
      float4 _RimColor;
      float _RimPower;
      float _isGrayscale;
      void surf (Input IN, inout SurfaceOutput o) {
          float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
          screenUV *= float2(8,6);
          
          if(_isGrayscale == 0){
          	o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          } else {
          	float3 c = tex2D (_MainTex, IN.uv_MainTex).rgb;
          	o.Albedo = (c.r + c.g + c.b)/3;
          }
          
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _RimColor.rgb * pow (rim, _RimPower);
          o.Albedo *= tex2D (_Detail, screenUV).rgb * 2;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }
Shader "Custom/Silhouette" {
Properties  {
	_MainTex ("RGBA Texture Image", 2D) = "white" {}
	_Cutoff ("Alpha Cutoff", Float) = 0.5
}

SubShader { 
	Pass {
		ZTest Always 
		ZWrite Off
	 
		CGPROGRAM
		#pragma fragment frag
		#pragma vertex vert alpha
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex; 
		uniform float _Cutoff;
		float4 _MainTex_ST;

		struct v2f {
			float4 pos : SV_POSITION;
			half2 uv : TEXCOORD0;
		};

		struct appdata_t {
			float4 vertex : POSITION;
			float4 texcoord : TEXCOORD0;
		};

		v2f vert (appdata_t v) {
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			o.uv = TRANSFORM_TEX( v.texcoord, _MainTex );
			return o;
		}
	  
		half4 frag (v2f i) : COLOR {
			float4 textureColor = tex2D(_MainTex, i.uv.xy);
			if (textureColor.a < _Cutoff) {
				discard;
			}
			return textureColor;
		}
		
		ENDCG 
	}
}

FallBack "Diffuse"
}

Shader "Custom/Silhouette" {
Properties  {
	_MainTex ("RGBA Texture Image", 2D) = "white" {}
	_Cutoff ("Alpha Cutoff", Float) = 0.5
}

SubShader { 
	Pass {
		ZTest Greater
	 
		CGPROGRAM
		#pragma fragment frag
		#pragma vertex vert alpha
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex; 
		uniform float _Cutoff;

		struct v2f {
			float4 pos : SV_POSITION;
			float4 tex : TEXCOORD0;
		};

		struct appdata_t {
			float4 vertex : POSITION;
			float4 texcoord : TEXCOORD0;
		};

		v2f vert (appdata_t v) {
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			o.tex = v.texcoord;
			return o;
		}
	  
		half4 frag (v2f i) : COLOR {
			float4 textureColor = tex2D(_MainTex, i.tex.xy);
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

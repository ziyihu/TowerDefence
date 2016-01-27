Shader "Custom/SpriteUV" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SpriteRowCount ("RowCounts",float) = 0
		_SpriteColumnCount ("ColumnCounts",float) = 0
		_Speed ("AnimationSpeed",Range(0.01,10)) = 4
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass{
		LOD 200
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
		 uniform float _SpriteRowCount;
	     uniform float _SpriteColumnCount;
	     uniform float _Speed;
	     
	     struct VertexOutput
	     {
	         float4 pos:SV_POSITION;
	         float2 uv:TEXCOORD0;
	     };
		VertexOutput vert(appdata_base input)
	     {
	         VertexOutput o;
	         o.pos = mul(UNITY_MATRIX_MVP,input.vertex);
	         o.uv = input.texcoord.xy;
	         return o;
	     }
		float4 frag(VertexOutput input):COLOR
	     {
	         float totalSpriteCount = _SpriteRowCount * _SpriteColumnCount;
	         float rowAvgPercent = 1 / _SpriteColumnCount;
	         float columnAvgPercent = 1 / _SpriteRowCount;        
	         float SpriteIndex = fmod(_Time.y * _Speed,totalSpriteCount);        
	         SpriteIndex = floor(SpriteIndex);        
	         float columnIndex = fmod(SpriteIndex,_SpriteColumnCount);
	         float rowIndex = SpriteIndex / _SpriteColumnCount;
	         rowIndex = _SpriteRowCount - 1 - floor(rowIndex);
	         float2 spriteUV = input.uv;
	         spriteUV.x = (spriteUV.x + columnIndex) * rowAvgPercent;
	         spriteUV.y = (spriteUV.y + rowIndex) * columnAvgPercent;            
	         float4 col = tex2D(_MainTex,spriteUV);
	         return col;
	     }
		ENDCG
		}
	} 
	FallBack "Diffuse"
}

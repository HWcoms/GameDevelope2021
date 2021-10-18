// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DawnShader/HairShader"
{
	Properties
	{
		_NoiseTilling("Noise Tilling", Float) = 24
		_NoiseTexture("Noise Texture", 2D) = "white" {}
		_HairAlpha("Hair Alpha", 2D) = "white" {}
		_RoughnessRoot("Roughness Root", Range( 0 , 5)) = 0.06
		_RoughnessTip("Roughness Tip", Range( 0 , 5)) = 0.04
		_MipBias("MipBias", Float) = -1
		_RootColor("Root Color", Color) = (0.3113208,0.184846,0.1072001,0)
		_TipColor("Tip Color", Color) = (0.1226415,0.06746172,0.03297437,0)
		_MetallicBoost("Metallic Boost", Range( 0 , 5)) = 0.7
		_TangentA("TangentA", Color) = (0,0.1543091,0.5568628,0.003921569)
		_TangentB("TangentB", Color) = (0,0.7781801,1,0.003921569)
		_OpacityBoost("Opacity Boost", Range( 0 , 4)) = 1
		_DitherBoost("Dither Boost", Range( 0 , 4)) = 1
		_Brighness("Brighness", Float) = 0.2
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Overlay+0" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPosition;
		};

		uniform float4 _TangentA;
		uniform float4 _TangentB;
		uniform sampler2D _HairAlpha;
		SamplerState sampler_HairAlpha;
		uniform float4 _HairAlpha_ST;
		uniform float _MipBias;
		uniform float _Brighness;
		uniform float4 _RootColor;
		uniform float4 _TipColor;
		uniform sampler2D _NoiseTexture;
		SamplerState sampler_NoiseTexture;
		uniform float _NoiseTilling;
		uniform float _MetallicBoost;
		uniform float _RoughnessTip;
		uniform float _RoughnessRoot;
		uniform float _DitherBoost;
		uniform float _OpacityBoost;
		uniform float _Cutoff = 0.5;


		inline float Dither4x4Bayer( int x, int y )
		{
			const float dither[ 16 ] = {
				 1,  9,  3, 11,
				13,  5, 15,  7,
				 4, 12,  2, 10,
				16,  8, 14,  6 };
			int r = y * 4 + x;
			return dither[r] / 16; // same # of instructions as pre-dividing due to compiler magic
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float4 ase_screenPos = ComputeScreenPos( UnityObjectToClipPos( v.vertex ) );
			o.screenPosition = ase_screenPos;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_HairAlpha = i.uv_texcoord * _HairAlpha_ST.xy + _HairAlpha_ST.zw;
			float4 tex2DNode2 = tex2Dbias( _HairAlpha, float4( uv_HairAlpha, 0, _MipBias) );
			float4 lerpResult64 = lerp( _TangentA , _TangentB , tex2DNode2.r);
			float4 normalizeResult65 = normalize( lerpResult64 );
			o.Normal = normalizeResult65.rgb;
			float4 lerpResult82 = lerp( _RootColor , _TipColor , tex2DNode2.r);
			o.Albedo = ( _Brighness * ( lerpResult82 * tex2D( _NoiseTexture, ( i.uv_texcoord * _NoiseTilling ) ).r ) ).rgb;
			o.Metallic = ( _MetallicBoost * tex2DNode2.r );
			float lerpResult73 = lerp( _RoughnessTip , _RoughnessRoot , tex2DNode2.r);
			o.Smoothness = lerpResult73;
			o.Alpha = 1;
			float4 ase_screenPos = i.screenPosition;
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 clipScreen92 = ase_screenPosNorm.xy * _ScreenParams.xy;
			float dither92 = Dither4x4Bayer( fmod(clipScreen92.x, 4), fmod(clipScreen92.y, 4) );
			dither92 = step( dither92, ( tex2DNode2.a * _DitherBoost ) );
			clip( ( (0.0 + (dither92 - 0.0) * (1.0 - 0.0) / (1.0 - 0.0)) + (0.0 + (( _OpacityBoost * tex2DNode2.a ) - 0.0) * (1.0 - 0.0) / (1.0 - 0.0)) ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
2560;0;2560;1059;1139.148;102.6946;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;74;-1771.874,-451.9984;Inherit;False;1184.027;869.6328;Base Color;12;85;84;83;82;81;79;78;77;76;75;2;55;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;86;-1844.317,475.8812;Inherit;False;1146.538;473.4188;Opacity;8;95;94;93;92;91;90;89;87;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1727.103,206.5775;Float;False;Property;_MipBias;MipBias;5;0;Create;True;0;0;False;0;False;-1;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;76;-1721.874,-398.6082;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;75;-1693.323,-258.1593;Float;False;Property;_NoiseTilling;Noise Tilling;0;0;Create;True;0;0;False;0;False;24;35;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-1784.088,722.9722;Float;False;Property;_DitherBoost;Dither Boost;12;0;Create;True;0;0;False;0;False;1;2.72;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1718.209,-21.48597;Inherit;True;Property;_HairAlpha;Hair Alpha;2;0;Create;True;0;0;False;0;False;-1;347bee86d8ff03845bfdb6e6cac93870;347bee86d8ff03845bfdb6e6cac93870;True;0;False;white;Auto;False;Object;-1;MipBias;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;56;-521.9481,-475.5573;Inherit;False;1005.005;800.842;Normal;4;65;64;60;59;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-1496.874,-346.6082;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;89;-1480.982,608.1351;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;79;-1310.097,-189.415;Float;False;Property;_RootColor;Root Color;6;0;Create;True;0;0;False;0;False;0.3113208,0.184846,0.1072001,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;78;-1313.337,-1.718262;Float;False;Property;_TipColor;Tip Color;7;0;Create;True;0;0;False;0;False;0.1226415,0.06746172,0.03297437,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;90;-1783.138,803.327;Float;False;Property;_OpacityBoost;Opacity Boost;11;0;Create;True;0;0;False;0;False;1;3.7;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;60;-155.1383,-423.2082;Float;False;Property;_TangentA;TangentA;9;0;Create;True;0;0;False;0;False;0,0.1543091,0.5568628,0.003921569;0,0.1543091,0.5568628,0.003921569;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;59;-154.1383,-249.2076;Float;False;Property;_TangentB;TangentB;10;0;Create;True;0;0;False;0;False;0,0.7781801,1,0.003921569;1,0.7653513,0,0.003921569;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;81;-1319.135,-398.7985;Inherit;True;Property;_NoiseTexture;Noise Texture;1;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;68;-627.4835,522.3861;Inherit;False;800.4378;294.6938;Roughness;3;73;71;69;;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;82;-1034.462,-101.4217;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-1452.562,769.683;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DitheringNode;92;-1320.614,563.7701;Inherit;False;0;True;3;0;FLOAT;0;False;1;SAMPLER2D;;False;2;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;93;-1076.867,747.2991;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-288.0162,605.4874;Float;False;Property;_RoughnessTip;Roughness Tip;4;0;Create;True;0;0;False;0;False;0.04;0.22;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;-932.0945,-300.8712;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;64;135.8624,-307.2082;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;94;-1076.13,581.5602;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;70;-259.4961,401.6363;Float;False;Property;_MetallicBoost;Metallic Boost;8;0;Create;True;0;0;False;0;False;0.7;0.5;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;69;-286.0447,706.0798;Float;False;Property;_RoughnessRoot;Roughness Root;3;0;Create;True;0;0;False;0;False;0.06;0.11;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-996.5434,-388.1333;Float;False;Property;_Brighness;Brighness;13;0;Create;True;0;0;False;0;False;0.2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-767.2622,-357.6505;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;73;-11.04499,607.08;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;41.57341,412.4997;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;95;-851.7798,614.4042;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;65;123.3082,-167.4089;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;626.2911,259.8262;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;DawnShader/HairShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Overlay;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;2;False;-1;0;False;-1;0;5;False;-1;7;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;14;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;35;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;2;55;0
WireConnection;77;0;76;0
WireConnection;77;1;75;0
WireConnection;89;0;2;4
WireConnection;89;1;87;0
WireConnection;81;1;77;0
WireConnection;82;0;79;0
WireConnection;82;1;78;0
WireConnection;82;2;2;1
WireConnection;91;0;90;0
WireConnection;91;1;2;4
WireConnection;92;0;89;0
WireConnection;93;0;91;0
WireConnection;83;0;82;0
WireConnection;83;1;81;1
WireConnection;64;0;60;0
WireConnection;64;1;59;0
WireConnection;64;2;2;1
WireConnection;94;0;92;0
WireConnection;85;0;84;0
WireConnection;85;1;83;0
WireConnection;73;0;71;0
WireConnection;73;1;69;0
WireConnection;73;2;2;1
WireConnection;72;0;70;0
WireConnection;72;1;2;1
WireConnection;95;0;94;0
WireConnection;95;1;93;0
WireConnection;65;0;64;0
WireConnection;0;0;85;0
WireConnection;0;1;65;0
WireConnection;0;3;72;0
WireConnection;0;4;73;0
WireConnection;0;10;95;0
ASEEND*/
//CHKSM=3AF9FB92C3EE57E715134996A0DCFB48B5A7467D
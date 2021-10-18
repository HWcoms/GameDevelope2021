// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DawnShader/ClothingShader2Sides"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[Toggle(_ONOFFOPACITY_ON)] _OnOffOpacity("On/Off Opacity", Float) = 0
		_DitherBoosts("Dither Boosts", Range( 0 , 20)) = 9.5
		_OpacityBoots("Opacity Boots", Range( 0 , 4)) = 0.5
		_Dirt("Dirt", 2D) = "white" {}
		_DirtBoots("Dirt Boots", Range( 0 , 2)) = 0
		_AO("AO", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Mask("Mask", 2D) = "white" {}
		_Metallic("Metallic", 2D) = "white" {}
		_MetallicBoots01("Metallic Boots 01", Range( 0 , 4)) = 0
		_MetallicBoots02("Metallic Boots 02", Range( 0 , 4)) = 1
		_MetallicBoots03("Metallic Boots 03", Range( 0 , 4)) = 0
		_MetallicBoots04("Metallic Boots 04", Range( 0 , 4)) = 1
		_Roughness("Roughness", 2D) = "white" {}
		_RoughnessBoost01("Roughness Boost 01", Range( 0 , 4)) = 1
		_RoughnessBoost02("Roughness Boost 02", Range( 0 , 4)) = 1
		_RoughnessBoost03("Roughness Boost 03", Range( 0 , 4)) = 1
		_RoughnessBoost04("Roughness Boost 04", Range( 0 , 4)) = 1
		_BC("Base Color", 2D) = "white" {}
		_BlendColor01("Blend Color 01", Color) = (1,1,1,0)
		_BlendColorPower01("Blend Color Power 01", Range( 0 , 1)) = 0
		_BlendColor02("Blend Color 02", Color) = (1,1,1,0)
		_BlendColorPower02("Blend Color Power 02", Range( 0 , 1)) = 0
		_BlendColor03("Blend Color 03", Color) = (1,1,1,0)
		_BlendColorPower03("Blend Color Power 03", Range( 0 , 1)) = 0
		_BlendColor04("Blend Color 04", Color) = (1,1,1,0)
		_BlendColorPower04("Blend Color Power 04", Range( 0 , 1)) = 0
		[Toggle(_BLENDTEXTUREMODE_ON)] _BlendTextureMode("Blend Texture Mode", Float) = 0
		[Toggle(_BLENDTEXTURE01_ON)] _BlendTexture01("Blend Texture 01", Float) = 0
		[Toggle(_BLENDTEXTURE02_ON)] _BlendTexture02("Blend Texture 02", Float) = 0
		[Toggle(_BLENDTEXTURE03_ON)] _BlendTexture03("Blend Texture 03", Float) = 0
		[Toggle(_BLENDTEXTURE04_ON)] _BlendTexture04("Blend Texture 04", Float) = 0
		_Texture01("Texture 01", 2D) = "white" {}
		_EmissiveMask1("Emissive Mask", 2D) = "white" {}
		_Texture02("Texture 02", 2D) = "white" {}
		_Texture03("Texture 03", 2D) = "white" {}
		_EmissiveColor2("Emissive Color 01", Color) = (0,0,0,0)
		_Texture04("Texture 04", 2D) = "white" {}
		_EmissiveBoots2("Emissive Boots 1", Float) = 0
		_EmissiveColor3("Emissive Color 02", Color) = (0,0,0,0)
		_EmissiveBoots3("Emissive Boots 2", Float) = 0
		_EmissiveColor4("Emissive Color 03", Color) = (0,0,0,0)
		_EmissiveBoots4("Emissive Boots 3", Float) = 0
		_EmissiveColor5("Emissive Color 04", Color) = (0,0,0,0)
		_EmissiveBoots5("Emissive Boots 4", Float) = 0
		_EmissivePannerMap1("Emissive Panner Map", 2D) = "white" {}
		_EmissivePannerTilling1("Emissive Panner Tilling", Float) = 1
		_PannerProperty1("Panner Property", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature _BLENDTEXTUREMODE_ON
		#pragma shader_feature _BLENDTEXTURE01_ON
		#pragma shader_feature _BLENDTEXTURE02_ON
		#pragma shader_feature _BLENDTEXTURE03_ON
		#pragma shader_feature _BLENDTEXTURE04_ON
		#pragma shader_feature _ONOFFOPACITY_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			half ASEVFace : VFACE;
			float4 screenPosition;
		};

		uniform sampler2D _Normal;
		SamplerState sampler_Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _BC;
		uniform float4 _BC_ST;
		uniform float4 _BlendColor01;
		uniform sampler2D _Mask;
		SamplerState sampler_Mask;
		uniform float4 _Mask_ST;
		uniform float4 _BlendColor02;
		uniform float4 _BlendColor03;
		uniform float4 _BlendColor04;
		uniform float _BlendColorPower01;
		uniform float _BlendColorPower02;
		uniform float _BlendColorPower03;
		uniform float _BlendColorPower04;
		uniform sampler2D _Texture01;
		uniform float4 _Texture01_ST;
		uniform sampler2D _Texture02;
		uniform float4 _Texture02_ST;
		uniform sampler2D _Texture03;
		uniform float4 _Texture03_ST;
		uniform sampler2D _Texture04;
		uniform float4 _Texture04_ST;
		uniform sampler2D _Dirt;
		uniform float4 _Dirt_ST;
		uniform float _DirtBoots;
		SamplerState sampler_Dirt;
		uniform sampler2D _EmissiveMask1;
		SamplerState sampler_EmissiveMask1;
		uniform float4 _EmissiveMask1_ST;
		uniform sampler2D _EmissivePannerMap1;
		uniform float2 _PannerProperty1;
		uniform float _EmissivePannerTilling1;
		uniform float _EmissiveBoots2;
		uniform float4 _EmissiveColor2;
		uniform float _EmissiveBoots3;
		uniform float4 _EmissiveColor3;
		uniform float _EmissiveBoots4;
		uniform float4 _EmissiveColor4;
		uniform float _EmissiveBoots5;
		uniform float4 _EmissiveColor5;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform float _MetallicBoots01;
		uniform float _MetallicBoots02;
		uniform float _MetallicBoots03;
		uniform float _MetallicBoots04;
		uniform sampler2D _Roughness;
		uniform float4 _Roughness_ST;
		uniform float _RoughnessBoost01;
		uniform float _RoughnessBoost02;
		uniform float _RoughnessBoost03;
		uniform float _RoughnessBoost04;
		uniform sampler2D _AO;
		uniform float4 _AO_ST;
		SamplerState sampler_BC;
		uniform float _DitherBoosts;
		uniform float _OpacityBoots;
		uniform float _Cutoff = 0.5;


		inline float Dither8x8Bayer( int x, int y )
		{
			const float dither[ 64 ] = {
				 1, 49, 13, 61,  4, 52, 16, 64,
				33, 17, 45, 29, 36, 20, 48, 32,
				 9, 57,  5, 53, 12, 60,  8, 56,
				41, 25, 37, 21, 44, 28, 40, 24,
				 3, 51, 15, 63,  2, 50, 14, 62,
				35, 19, 47, 31, 34, 18, 46, 30,
				11, 59,  7, 55, 10, 58,  6, 54,
				43, 27, 39, 23, 42, 26, 38, 22};
			int r = y * 8 + x;
			return dither[r] / 64; // same # of instructions as pre-dividing due to compiler magic
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float4 ase_screenPos = ComputeScreenPos( UnityObjectToClipPos( v.vertex ) );
			o.screenPosition = ase_screenPos;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 tex2DNode75 = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float4 appendResult175 = (float4(tex2DNode75.r , tex2DNode75.g , ( tex2DNode75.b * i.ASEVFace ) , 0.0));
			o.Normal = appendResult175.xyz;
			float2 uv_BC = i.uv_texcoord * _BC_ST.xy + _BC_ST.zw;
			float4 tex2DNode1 = tex2D( _BC, uv_BC );
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode2 = tex2D( _Mask, uv_Mask );
			float4 lerpResult28 = lerp( tex2DNode1 , _BlendColor01 , tex2DNode2.r);
			float4 lerpResult29 = lerp( tex2DNode1 , _BlendColor02 , tex2DNode2.g);
			float4 lerpResult30 = lerp( tex2DNode1 , _BlendColor03 , tex2DNode2.b);
			float4 lerpResult31 = lerp( tex2DNode1 , _BlendColor04 , tex2DNode2.a);
			float4 lerpResult147 = lerp( tex2DNode1 , ( lerpResult28 * lerpResult29 * lerpResult30 * lerpResult31 ) , ( ( _BlendColorPower01 * tex2DNode2.r ) + ( _BlendColorPower02 * tex2DNode2.g ) + ( _BlendColorPower03 * tex2DNode2.b ) + ( _BlendColorPower04 * tex2DNode2.a ) ));
			float2 uv_Texture01 = i.uv_texcoord * _Texture01_ST.xy + _Texture01_ST.zw;
			float4 lerpResult55 = lerp( float4( 0,0,0,0 ) , tex2D( _Texture01, uv_Texture01 ) , tex2DNode2.r);
			#ifdef _BLENDTEXTURE01_ON
				float4 staticSwitch60 = lerpResult55;
			#else
				float4 staticSwitch60 = tex2DNode1;
			#endif
			float2 uv_Texture02 = i.uv_texcoord * _Texture02_ST.xy + _Texture02_ST.zw;
			float4 lerpResult56 = lerp( float4( 0,0,0,0 ) , tex2D( _Texture02, uv_Texture02 ) , tex2DNode2.g);
			#ifdef _BLENDTEXTURE02_ON
				float4 staticSwitch61 = lerpResult56;
			#else
				float4 staticSwitch61 = tex2DNode1;
			#endif
			float2 uv_Texture03 = i.uv_texcoord * _Texture03_ST.xy + _Texture03_ST.zw;
			float4 lerpResult57 = lerp( float4( 0,0,0,0 ) , tex2D( _Texture03, uv_Texture03 ) , tex2DNode2.b);
			#ifdef _BLENDTEXTURE03_ON
				float4 staticSwitch62 = lerpResult57;
			#else
				float4 staticSwitch62 = tex2DNode1;
			#endif
			float2 uv_Texture04 = i.uv_texcoord * _Texture04_ST.xy + _Texture04_ST.zw;
			float4 lerpResult58 = lerp( float4( 0,0,0,0 ) , tex2D( _Texture04, uv_Texture04 ) , tex2DNode2.a);
			#ifdef _BLENDTEXTURE04_ON
				float4 staticSwitch63 = lerpResult58;
			#else
				float4 staticSwitch63 = tex2DNode1;
			#endif
			float temp_output_27_0 = ( tex2DNode2.r + tex2DNode2.g + tex2DNode2.b + tex2DNode2.a );
			float4 lerpResult71 = lerp( tex2DNode1 , ( ( staticSwitch60 * tex2DNode2.r ) + ( staticSwitch61 * tex2DNode2.g ) + ( staticSwitch62 * tex2DNode2.b ) + ( staticSwitch63 * tex2DNode2.a ) ) , temp_output_27_0);
			#ifdef _BLENDTEXTUREMODE_ON
				float4 staticSwitch72 = lerpResult71;
			#else
				float4 staticSwitch72 = lerpResult147;
			#endif
			float2 uv_Dirt = i.uv_texcoord * _Dirt_ST.xy + _Dirt_ST.zw;
			float4 tex2DNode80 = tex2D( _Dirt, uv_Dirt );
			float4 lerpResult87 = lerp( staticSwitch72 , ( tex2DNode80 * _DirtBoots ) , ( _DirtBoots * tex2DNode80.a ));
			o.Albedo = lerpResult87.rgb;
			float2 uv_EmissiveMask1 = i.uv_texcoord * _EmissiveMask1_ST.xy + _EmissiveMask1_ST.zw;
			float4 tex2DNode185 = tex2D( _EmissiveMask1, uv_EmissiveMask1 );
			float2 temp_cast_2 = (_EmissivePannerTilling1).xx;
			float2 uv_TexCoord180 = i.uv_texcoord * temp_cast_2;
			float2 panner182 = ( _Time.y * _PannerProperty1 + uv_TexCoord180);
			float4 tex2DNode183 = tex2D( _EmissivePannerMap1, panner182 );
			o.Emission = ( ( tex2DNode185.r * tex2DNode183 * _EmissiveBoots2 * _EmissiveColor2 ) + ( tex2DNode185.g * tex2DNode183 * _EmissiveBoots3 * _EmissiveColor3 ) + ( tex2DNode185.b * tex2DNode183 * _EmissiveBoots4 * _EmissiveColor4 ) + ( tex2DNode185.a * tex2DNode183 * _EmissiveBoots5 * _EmissiveColor5 ) ).rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			float4 tex2DNode76 = tex2D( _Metallic, uv_Metallic );
			float4 lerpResult128 = lerp( tex2DNode76 , ( ( ( _MetallicBoots01 * tex2DNode2.r ) + ( _MetallicBoots02 * tex2DNode2.g ) + ( _MetallicBoots03 * tex2DNode2.b ) + ( _MetallicBoots04 * tex2DNode2.a ) ) * tex2DNode76 ) , temp_output_27_0);
			o.Metallic = lerpResult128.r;
			float2 uv_Roughness = i.uv_texcoord * _Roughness_ST.xy + _Roughness_ST.zw;
			float4 temp_output_174_0 = ( 1.0 - tex2D( _Roughness, uv_Roughness ) );
			float4 lerpResult119 = lerp( temp_output_174_0 , ( ( ( _RoughnessBoost01 * tex2DNode2.r ) + ( _RoughnessBoost02 * tex2DNode2.g ) + ( _RoughnessBoost03 * tex2DNode2.b ) + ( _RoughnessBoost04 * tex2DNode2.a ) ) * temp_output_174_0 ) , temp_output_27_0);
			float4 temp_cast_5 = (( 1.0 * _DirtBoots )).xxxx;
			float4 lerpResult88 = lerp( lerpResult119 , temp_cast_5 , ( _DirtBoots * tex2DNode80.a ));
			o.Smoothness = lerpResult88.r;
			float2 uv_AO = i.uv_texcoord * _AO_ST.xy + _AO_ST.zw;
			o.Occlusion = tex2D( _AO, uv_AO ).r;
			o.Alpha = 1;
			float4 ase_screenPos = i.screenPosition;
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 clipScreen155 = ase_screenPosNorm.xy * _ScreenParams.xy;
			float dither155 = Dither8x8Bayer( fmod(clipScreen155.x, 8), fmod(clipScreen155.y, 8) );
			dither155 = step( dither155, ( tex2DNode1.a * _DitherBoosts ) );
			float4 temp_cast_8 = (( dither155 + ( _OpacityBoots * tex2DNode1.a ) )).xxxx;
			float4 color160 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
			#ifdef _ONOFFOPACITY_ON
				float4 staticSwitch159 = color160;
			#else
				float4 staticSwitch159 = temp_cast_8;
			#endif
			clip( staticSwitch159.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
2560;0;2560;1059;2363.688;2853.302;1.699859;True;False
Node;AmplifyShaderEditor.SamplerNode;43;-2881.104,4.529496;Inherit;True;Property;_Texture02;Texture 02;35;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-3082.411,-1163.275;Inherit;True;Property;_Mask;Mask;8;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;54;-2882.484,549.7479;Inherit;True;Property;_Texture04;Texture 04;38;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;39;-2885.036,-281.1397;Inherit;True;Property;_Texture01;Texture 01;33;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;50;-2882.945,273.4547;Inherit;True;Property;_Texture03;Texture 03;36;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;57;-2468.646,256.5266;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;56;-2486.201,-16.73898;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;55;-2491.789,-300.0495;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;58;-2457.471,532.6592;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-3072.166,-963.0521;Inherit;True;Property;_BC;Base Color;19;0;Create;False;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;60;-2213.897,-327.3228;Float;False;Property;_BlendTexture01;Blend Texture 01;29;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;178;-3456.482,-2485.464;Inherit;False;Property;_EmissivePannerTilling1;Emissive Panner Tilling;47;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;168;-3477.319,-1476.052;Inherit;False;Property;_BlendColorPower04;Blend Color Power 04;27;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-2538.948,-1332.005;Float;False;Property;_BlendColor02;Blend Color 02;22;0;Create;True;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;93;-336.6754,-462.5357;Float;False;Property;_RoughnessBoost03;Roughness Boost 03;17;0;Create;True;0;0;False;0;False;1;0;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-3047.917,-1337.109;Float;False;Property;_BlendColor01;Blend Color 01;20;0;Create;True;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;15;-1803.023,-1314.167;Float;False;Property;_BlendColor04;Blend Color 04;26;0;Create;True;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-2163.126,-1318.067;Float;False;Property;_BlendColor03;Blend Color 03;24;0;Create;True;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;91;-331.6754,-586.5353;Float;False;Property;_RoughnessBoost02;Roughness Boost 02;16;0;Create;True;0;0;False;0;False;1;0;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;62;-2210.348,-111.9511;Float;False;Property;_BlendTexture03;Blend Texture 03;31;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-334.4108,-327.243;Float;False;Property;_RoughnessBoost04;Roughness Boost 04;18;0;Create;True;0;0;False;0;False;1;0;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-333.8505,-722.3608;Float;False;Property;_RoughnessBoost01;Roughness Boost 01;15;0;Create;True;0;0;False;0;False;1;0;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;61;-2210.348,-219.3009;Float;False;Property;_BlendTexture02;Blend Texture 02;30;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;167;-3477.069,-1789.539;Inherit;False;Property;_BlendColorPower02;Blend Color Power 02;23;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;63;-2211.201,-1.129412;Float;False;Property;_BlendTexture04;Blend Texture 04;32;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;165;-3472.041,-1637.896;Inherit;False;Property;_BlendColorPower03;Blend Color Power 03;25;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;166;-3477.249,-1921.981;Inherit;False;Property;_BlendColorPower01;Blend Color Power 01;21;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;169;-3162.787,-1641.192;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;101;514.1066,-708.1597;Float;False;Property;_MetallicBoots01;Metallic Boots 01;10;0;Create;True;0;0;False;0;False;0;0;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-43.93866,-588.1492;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-1854.932,-396.8809;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-1853.199,-282.3062;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;180;-3156.045,-2487.633;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;171;-3141.024,-1923.661;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;28;-2721.928,-1555.666;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;181;-3124.045,-2359.633;Float;False;Property;_PannerProperty1;Panner Property;48;0;Create;True;0;0;False;0;False;0,0;0.1,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TimeNode;179;-3140.045,-2199.634;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;30;-2341.91,-1557.232;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;-30.93866,-333.1487;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;29;-2526.072,-1556.6;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;107;513.5462,-313.0414;Float;False;Property;_MetallicBoots04;Metallic Boots 04;13;0;Create;True;0;0;False;0;False;1;0;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;77;-455.246,-161.8578;Inherit;True;Property;_Roughness;Roughness;14;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-1851.464,-174.6741;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;104;511.2816,-448.3343;Float;False;Property;_MetallicBoots03;Metallic Boots 03;12;0;Create;True;0;0;False;0;False;0;0;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;111;-51.93866,-720.1497;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;170;-3141.358,-1779.751;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-1853.199,-516.6637;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;31;-2132.354,-1556.063;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;154;171.5241,-967.9235;Float;False;Property;_DitherBoosts;Dither Boosts;2;0;Create;True;0;0;False;0;False;9.5;0;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;102;516.2817,-572.3339;Float;False;Property;_MetallicBoots02;Metallic Boots 02;11;0;Create;True;0;0;False;0;False;1;0;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;-36.93866,-461.1491;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;172;-3167.066,-1479.349;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;182;-2836.045,-2327.633;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;122;818.5206,-448.0989;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;174;-124.5013,-135.8322;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;123;824.5206,-320.0984;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;173;-2783.125,-1950.607;Inherit;True;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;152;-2431.542,-1796.561;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;158;507.6574,-1181.098;Float;False;Property;_OpacityBoots;Opacity Boots;3;0;Create;True;0;0;False;0;False;0.5;0;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;118;261.8468,-542.6664;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;121;811.5206,-575.0991;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;153;491.5241,-951.9235;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;70;-1467.807,-348.2731;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;120;803.5206,-707.0998;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-2065.265,-1139.706;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;183;-2564.045,-2327.633;Inherit;True;Property;_EmissivePannerMap1;Emissive Panner Map;46;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;192;-2990.273,-2869.703;Float;False;Property;_EmissiveColor4;Emissive Color 03;42;0;Create;True;0;0;False;0;False;0,0,0,0;0.4009434,0.8080564,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;186;-2996.488,-3034.164;Float;False;Property;_EmissiveColor3;Emissive Color 02;40;0;Create;True;0;0;False;0;False;0,0,0,0;0,0.6797385,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;187;-3003.852,-3203.163;Float;False;Property;_EmissiveColor2;Emissive Color 01;37;0;Create;True;0;0;False;0;False;0,0,0,0;0.4009434,0.8080564,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;188;-2705.221,-3304.388;Float;False;Property;_EmissiveBoots4;Emissive Boots 3;43;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;189;-2980.179,-2699.339;Float;False;Property;_EmissiveColor5;Emissive Color 04;44;0;Create;True;0;0;False;0;False;0,0,0,0;0,0.6797385,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;190;-2702.807,-3468.092;Float;False;Property;_EmissiveBoots2;Emissive Boots 1;39;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;191;-2704.953,-3383.426;Float;False;Property;_EmissiveBoots3;Emissive Boots 2;41;0;Create;True;0;0;False;0;False;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;184;-2706.026,-3229.109;Float;False;Property;_EmissiveBoots5;Emissive Boots 4;45;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;185;-2710.975,-2587.278;Inherit;True;Property;_EmissiveMask1;Emissive Mask;34;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FaceVariableNode;176;-116.2063,-2132.472;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;71;-1243.507,-392.5125;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;85;-358.5976,-1291.623;Float;False;Constant;_Float0;Float 0;22;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;86;-484.8184,-1625.895;Float;False;Property;_DirtBoots;Dirt Boots;5;0;Create;True;0;0;False;0;False;0;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;147;-2088.647,-1779.873;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;157;786.5793,-1134.064;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;75;-253.3056,-2323.307;Inherit;True;Property;_Normal;Normal;7;0;Create;True;0;0;False;0;False;-1;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;127;1117.306,-529.6164;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;76;468.94,-159.856;Inherit;True;Property;_Metallic;Metallic;9;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DitheringNode;155;667.5239,-1015.924;Inherit;False;1;False;3;0;FLOAT;0;False;1;SAMPLER2D;;False;2;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;80;-505.1128,-1515.332;Inherit;True;Property;_Dirt;Dirt;4;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;117;130.0613,-216.1485;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;156;969.2486,-1033.432;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;177;99.79378,-2180.671;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;193;-2065.493,-2869.224;Inherit;False;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;196;-2061.502,-2692.637;Inherit;False;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;194;-2074.023,-3047.961;Inherit;False;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;195;-2084.514,-3216.748;Inherit;False;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;160;524.181,-1515.492;Float;False;Constant;_ZeroMask;Zero Mask;34;0;Create;True;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;119;139.2943,-101.9112;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;72;-765.7151,-832.885;Float;False;Property;_BlendTextureMode;Blend Texture Mode;28;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-92.35077,-1376.695;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;985.5206,-203.0984;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-92.35083,-1627.187;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;-92.35077,-1247.51;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;-95.50151,-1501.153;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1581.31,-1308.37;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-2318.233,-1325.208;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;159;1015.175,-1315.079;Float;False;Property;_OnOffOpacity;On/Off Opacity;1;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;128;994.7536,-88.86114;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;88;287.6019,-1367.028;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;87;296.463,-1594.75;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;74;-251.6975,-1947.125;Inherit;True;Property;_AO;AO;6;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;175;402.4938,-2295.372;Inherit;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-2725.142,-1324.813;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1941.412,-1312.271;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;197;-1696.823,-2895.163;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1593.379,-1531.525;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;DawnShader/ClothingShader2Sides;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;57;1;50;0
WireConnection;57;2;2;3
WireConnection;56;1;43;0
WireConnection;56;2;2;2
WireConnection;55;1;39;0
WireConnection;55;2;2;1
WireConnection;58;1;54;0
WireConnection;58;2;2;4
WireConnection;60;1;1;0
WireConnection;60;0;55;0
WireConnection;62;1;1;0
WireConnection;62;0;57;0
WireConnection;61;1;1;0
WireConnection;61;0;56;0
WireConnection;63;1;1;0
WireConnection;63;0;58;0
WireConnection;169;0;165;0
WireConnection;169;1;2;3
WireConnection;112;0;91;0
WireConnection;112;1;2;2
WireConnection;65;0;61;0
WireConnection;65;1;2;2
WireConnection;66;0;62;0
WireConnection;66;1;2;3
WireConnection;180;0;178;0
WireConnection;171;0;166;0
WireConnection;171;1;2;1
WireConnection;28;0;1;0
WireConnection;28;1;8;0
WireConnection;28;2;2;1
WireConnection;30;0;1;0
WireConnection;30;1;13;0
WireConnection;30;2;2;3
WireConnection;114;0;95;0
WireConnection;114;1;2;4
WireConnection;29;0;1;0
WireConnection;29;1;10;0
WireConnection;29;2;2;2
WireConnection;67;0;63;0
WireConnection;67;1;2;4
WireConnection;111;0;79;0
WireConnection;111;1;2;1
WireConnection;170;0;167;0
WireConnection;170;1;2;2
WireConnection;64;0;60;0
WireConnection;64;1;2;1
WireConnection;31;0;1;0
WireConnection;31;1;15;0
WireConnection;31;2;2;4
WireConnection;113;0;93;0
WireConnection;113;1;2;3
WireConnection;172;0;168;0
WireConnection;172;1;2;4
WireConnection;182;0;180;0
WireConnection;182;2;181;0
WireConnection;182;1;179;2
WireConnection;122;0;104;0
WireConnection;122;1;2;3
WireConnection;174;0;77;0
WireConnection;123;0;107;0
WireConnection;123;1;2;4
WireConnection;173;0;171;0
WireConnection;173;1;170;0
WireConnection;173;2;169;0
WireConnection;173;3;172;0
WireConnection;152;0;28;0
WireConnection;152;1;29;0
WireConnection;152;2;30;0
WireConnection;152;3;31;0
WireConnection;118;0;111;0
WireConnection;118;1;112;0
WireConnection;118;2;113;0
WireConnection;118;3;114;0
WireConnection;121;0;102;0
WireConnection;121;1;2;2
WireConnection;153;0;1;4
WireConnection;153;1;154;0
WireConnection;70;0;64;0
WireConnection;70;1;65;0
WireConnection;70;2;66;0
WireConnection;70;3;67;0
WireConnection;120;0;101;0
WireConnection;120;1;2;1
WireConnection;27;0;2;1
WireConnection;27;1;2;2
WireConnection;27;2;2;3
WireConnection;27;3;2;4
WireConnection;183;1;182;0
WireConnection;71;0;1;0
WireConnection;71;1;70;0
WireConnection;71;2;27;0
WireConnection;147;0;1;0
WireConnection;147;1;152;0
WireConnection;147;2;173;0
WireConnection;157;0;158;0
WireConnection;157;1;1;4
WireConnection;127;0;120;0
WireConnection;127;1;121;0
WireConnection;127;2;122;0
WireConnection;127;3;123;0
WireConnection;155;0;153;0
WireConnection;117;0;118;0
WireConnection;117;1;174;0
WireConnection;156;0;155;0
WireConnection;156;1;157;0
WireConnection;177;0;75;3
WireConnection;177;1;176;0
WireConnection;193;0;185;3
WireConnection;193;1;183;0
WireConnection;193;2;188;0
WireConnection;193;3;192;0
WireConnection;196;0;185;4
WireConnection;196;1;183;0
WireConnection;196;2;184;0
WireConnection;196;3;189;0
WireConnection;194;0;185;2
WireConnection;194;1;183;0
WireConnection;194;2;191;0
WireConnection;194;3;186;0
WireConnection;195;0;185;1
WireConnection;195;1;183;0
WireConnection;195;2;190;0
WireConnection;195;3;187;0
WireConnection;119;0;174;0
WireConnection;119;1;117;0
WireConnection;119;2;27;0
WireConnection;72;1;147;0
WireConnection;72;0;71;0
WireConnection;82;0;85;0
WireConnection;82;1;86;0
WireConnection;126;0;127;0
WireConnection;126;1;76;0
WireConnection;84;0;80;0
WireConnection;84;1;86;0
WireConnection;81;0;86;0
WireConnection;81;1;80;4
WireConnection;83;0;86;0
WireConnection;83;1;80;4
WireConnection;14;0;15;0
WireConnection;14;1;2;4
WireConnection;11;0;10;0
WireConnection;11;1;2;2
WireConnection;159;1;156;0
WireConnection;159;0;160;0
WireConnection;128;0;76;0
WireConnection;128;1;126;0
WireConnection;128;2;27;0
WireConnection;88;0;119;0
WireConnection;88;1;82;0
WireConnection;88;2;81;0
WireConnection;87;0;72;0
WireConnection;87;1;84;0
WireConnection;87;2;83;0
WireConnection;175;0;75;1
WireConnection;175;1;75;2
WireConnection;175;2;177;0
WireConnection;3;0;8;0
WireConnection;3;1;2;1
WireConnection;12;0;13;0
WireConnection;12;1;2;3
WireConnection;197;0;195;0
WireConnection;197;1;194;0
WireConnection;197;2;193;0
WireConnection;197;3;196;0
WireConnection;0;0;87;0
WireConnection;0;1;175;0
WireConnection;0;2;197;0
WireConnection;0;3;128;0
WireConnection;0;4;88;0
WireConnection;0;5;74;0
WireConnection;0;10;159;0
ASEEND*/
//CHKSM=A8CBF30276504E33778D3C9291F9A967565C206A
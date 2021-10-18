// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DawnShader/EyeslashShader"
{
	Properties
	{
		_TextNoise("TextNoise", 2D) = "white" {}
		_NoiseTilling("Noise Tilling", Float) = 24
		_Root("Root", 2D) = "white" {}
		_BaseColor("Base Color", 2D) = "white" {}
		[Toggle(_USEBASECOLOR_ON)] _UseBaseColor("UseBaseColor", Float) = 0
		[Toggle(_ISDYED_ON)] _isDyed("isDyed", Float) = 0
		_DyedColor("Dyed Color", Color) = (1,0,0,0)
		_DyeMask("DyeMask", 2D) = "white" {}
		_Alpha("Alpha", 2D) = "white" {}
		_ID("ID", 2D) = "white" {}
		_TipColor("Tip Color", Color) = (0.1226415,0.06746172,0.03297437,0)
		_RootColor("Root Color", Color) = (0.3113208,0.184846,0.1072001,0)
		_OpacityBoots("Opacity Boots", Range( 0 , 4)) = 1
		_Brightness("Brightness", Float) = 0.2
		_RoughnessRoot("Roughness Root", Range( 0 , 5)) = 0.25
		_RoughnessTip("Roughness Tip", Range( 0 , 5)) = 0.25
		_MetallicBoost("Metallic Boost", Range( 0 , 5)) = 0.7
		_MipBias("MipBias", Float) = -1
		_TangentA("TangentA", Color) = (0,0.2367066,0.8679245,0.003921569)
		_TangentB("TangentB", Color) = (0.4374777,0.6237929,0.8207547,0.003921569)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		AlphaToMask On
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma shader_feature _USEBASECOLOR_ON
		#pragma shader_feature _ISDYED_ON
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _TangentA;
		uniform float4 _TangentB;
		uniform sampler2D _ID;
		SamplerState sampler_ID;
		uniform float4 _ID_ST;
		uniform float _MipBias;
		uniform float _Brightness;
		uniform float4 _RootColor;
		uniform float4 _TipColor;
		uniform sampler2D _Root;
		SamplerState sampler_Root;
		uniform float4 _Root_ST;
		uniform sampler2D _TextNoise;
		SamplerState sampler_TextNoise;
		uniform float _NoiseTilling;
		uniform float4 _DyedColor;
		uniform sampler2D _DyeMask;
		SamplerState sampler_DyeMask;
		uniform float4 _DyeMask_ST;
		uniform sampler2D _BaseColor;
		uniform float4 _BaseColor_ST;
		uniform float _MetallicBoost;
		uniform float _RoughnessTip;
		uniform float _RoughnessRoot;
		uniform float _OpacityBoots;
		uniform sampler2D _Alpha;
		SamplerState sampler_Alpha;
		uniform float4 _Alpha_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_ID = i.uv_texcoord * _ID_ST.xy + _ID_ST.zw;
			float4 tex2DNode50 = tex2Dbias( _ID, float4( uv_ID, 0, _MipBias) );
			float4 lerpResult106 = lerp( _TangentA , _TangentB , tex2DNode50.r);
			float4 normalizeResult108 = normalize( lerpResult106 );
			o.Normal = normalizeResult108.rgb;
			float2 uv_Root = i.uv_texcoord * _Root_ST.xy + _Root_ST.zw;
			float4 tex2DNode48 = tex2D( _Root, uv_Root );
			float4 lerpResult83 = lerp( _RootColor , _TipColor , tex2DNode48.r);
			float4 tex2DNode85 = tex2D( _TextNoise, ( i.uv_texcoord * _NoiseTilling ) );
			float2 uv_DyeMask = i.uv_texcoord * _DyeMask_ST.xy + _DyeMask_ST.zw;
			float4 lerpResult140 = lerp( lerpResult83 , _DyedColor , tex2D( _DyeMask, uv_DyeMask ).r);
			#ifdef _ISDYED_ON
				float4 staticSwitch139 = lerpResult140;
			#else
				float4 staticSwitch139 = ( lerpResult83 * tex2DNode85.r );
			#endif
			float2 uv_BaseColor = i.uv_texcoord * _BaseColor_ST.xy + _BaseColor_ST.zw;
			#ifdef _USEBASECOLOR_ON
				float4 staticSwitch148 = ( staticSwitch139 * tex2D( _BaseColor, uv_BaseColor ) );
			#else
				float4 staticSwitch148 = staticSwitch139;
			#endif
			o.Albedo = ( _Brightness * staticSwitch148 ).rgb;
			o.Metallic = ( _MetallicBoost * tex2DNode50.r );
			float lerpResult100 = lerp( 0.0 , _RoughnessTip , tex2DNode48.r);
			float lerpResult160 = lerp( 0.0 , ( 1.0 - _RoughnessRoot ) , tex2DNode48.r);
			o.Smoothness = ( lerpResult100 + lerpResult160 );
			float2 uv_Alpha = i.uv_texcoord * _Alpha_ST.xy + _Alpha_ST.zw;
			float4 tex2DNode49 = tex2Dbias( _Alpha, float4( uv_Alpha, 0, _MipBias) );
			float temp_output_114_0 = ( _OpacityBoots * tex2DNode49.r );
			o.Alpha = temp_output_114_0;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			AlphaToMask Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
2560;0;2560;1059;1439.761;-188.5917;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;137;-2842.43,-517.2213;Inherit;False;2349.419;886.9131;Base Color;17;91;148;90;147;139;138;140;89;83;85;146;145;88;82;81;86;87;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-2762.365,-314.2816;Float;False;Property;_NoiseTilling;Noise Tilling;1;0;Create;True;0;0;False;0;False;24;24;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;86;-2790.916,-454.7307;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;-2565.916,-402.7306;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;82;-2378.186,-57.84063;Float;False;Property;_TipColor;Tip Color;10;0;Create;True;0;0;False;0;False;0.1226415,0.06746172,0.03297437,0;0.2924528,0.1421646,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;48;-2373.341,775.1918;Inherit;True;Property;_Root;Root;2;0;Create;True;0;0;False;0;False;-1;None;347bee86d8ff03845bfdb6e6cac93870;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;81;-2374.946,-245.5373;Float;False;Property;_RootColor;Root Color;11;0;Create;True;0;0;False;0;False;0.3113208,0.184846,0.1072001,0;0.1886792,0.09171906,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;83;-2099.311,-157.544;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;85;-2394.254,-456.121;Inherit;True;Property;_TextNoise;TextNoise;0;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;145;-2022.293,192.6334;Inherit;False;Property;_DyedColor;Dyed Color;6;0;Create;True;0;0;False;0;False;1,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;146;-2404.141,155.7803;Inherit;True;Property;_DyeMask;DyeMask;7;0;Create;True;0;0;False;0;False;-1;None;347bee86d8ff03845bfdb6e6cac93870;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;89;-1996.943,-356.9937;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;140;-1767.81,-167.9768;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;102;-1103.515,665.3661;Inherit;False;800.4378;294.6938;Roughness;4;100;99;98;160;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;103;-1698.661,1312.42;Float;False;Property;_MipBias;MipBias;18;0;Create;True;0;0;False;0;False;-1;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;133;-1453.308,994.0795;Inherit;False;1157.29;895.7731;Normal;5;106;105;108;50;104;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;99;-1072.076,873.0599;Float;False;Property;_RoughnessRoot;Roughness Root;15;0;Create;True;0;0;False;0;False;0.25;0.76;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;138;-1614.441,49.03595;Inherit;True;Property;_BaseColor;Base Color;3;0;Create;True;0;0;False;0;False;-1;None;347bee86d8ff03845bfdb6e6cac93870;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;139;-1487.709,-380.7859;Inherit;False;Property;_isDyed;isDyed;5;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;104;-1094.41,1060.273;Float;False;Property;_TangentA;TangentA;19;0;Create;True;0;0;False;0;False;0,0.2367066,0.8679245,0.003921569;0,0.1543091,0.5568628,0.003921569;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;50;-1411.219,1057.923;Inherit;True;Property;_ID;ID;9;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipBias;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;134;-1446.053,2023.185;Inherit;False;1146.538;473.4188;Opacity;8;111;49;115;112;114;110;116;157;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;105;-1093.41,1234.273;Float;False;Property;_TangentB;TangentB;20;0;Create;True;0;0;False;0;False;0.4374777,0.6237929,0.8207547,0.003921569;1,0.7653513,0,0.003921569;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;147;-1307.445,-98.61004;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;161;-768.7407,870.7279;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;98;-1095.048,709.4676;Float;False;Property;_RoughnessTip;Roughness Tip;16;0;Create;True;0;0;False;0;False;0.25;0.12;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;135;-735.5278,544.6165;Float;False;Property;_MetallicBoost;Metallic Boost;17;0;Create;True;0;0;False;0;False;0.7;0.94;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;90;-910.2245,-268.1305;Float;False;Property;_Brightness;Brightness;14;0;Create;True;0;0;False;0;False;0.2;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;160;-587.7407,812.7279;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;115;-1391.874,2357.63;Float;False;Property;_OpacityBoots;Opacity Boots;12;0;Create;True;0;0;False;0;False;1;1.09;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;106;-803.4091,1176.273;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;100;-791.0766,707.0601;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;49;-1396.053,2073.184;Inherit;True;Property;_Alpha;Alpha;8;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipBias;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;148;-966.4904,-160.4921;Inherit;False;Property;_UseBaseColor;UseBaseColor;4;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-1047.718,2154.438;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DitheringNode;110;-872.3495,2112.073;Inherit;False;0;True;3;0;FLOAT;0;False;1;SAMPLER2D;;False;2;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;157;-580.0565,2104.844;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;162;-435.7407,744.7279;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;108;-621.9633,1173.071;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;111;-1385.824,2270.275;Float;False;Property;_DitherBoost;Dither Boost;13;0;Create;True;0;0;False;0;False;1;1.25;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;136;-434.4582,555.4799;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;116;-505.8944,2231.434;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-680.9434,-237.6477;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;158;-206.0624,723.3875;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;-1042.298,2310.986;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;159;-219.0624,646.3875;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;127;-13.49403,405.2581;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;DawnShader/EyeslashShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;1;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;False;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;2;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;True;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;88;0;86;0
WireConnection;88;1;87;0
WireConnection;83;0;81;0
WireConnection;83;1;82;0
WireConnection;83;2;48;1
WireConnection;85;1;88;0
WireConnection;89;0;83;0
WireConnection;89;1;85;1
WireConnection;140;0;83;0
WireConnection;140;1;145;0
WireConnection;140;2;146;1
WireConnection;139;1;89;0
WireConnection;139;0;140;0
WireConnection;50;2;103;0
WireConnection;147;0;139;0
WireConnection;147;1;138;0
WireConnection;161;0;99;0
WireConnection;160;1;161;0
WireConnection;160;2;48;1
WireConnection;106;0;104;0
WireConnection;106;1;105;0
WireConnection;106;2;50;1
WireConnection;100;1;98;0
WireConnection;100;2;48;1
WireConnection;49;2;103;0
WireConnection;148;1;139;0
WireConnection;148;0;147;0
WireConnection;112;0;49;1
WireConnection;112;1;111;0
WireConnection;110;0;112;0
WireConnection;110;2;85;1
WireConnection;157;0;110;0
WireConnection;162;0;100;0
WireConnection;162;1;160;0
WireConnection;108;0;106;0
WireConnection;136;0;135;0
WireConnection;136;1;50;1
WireConnection;116;0;110;0
WireConnection;116;1;114;0
WireConnection;91;0;90;0
WireConnection;91;1;148;0
WireConnection;114;0;115;0
WireConnection;114;1;49;1
WireConnection;127;0;91;0
WireConnection;127;1;108;0
WireConnection;127;3;136;0
WireConnection;127;4;162;0
WireConnection;127;9;114;0
ASEEND*/
//CHKSM=DB8C76FDFC6ED18A626E33338B74A97958ED734D
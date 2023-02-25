Shader "ShardShaders/WaterReflection"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistortionTex ("Water Distortion Texture", 2D) = "white" {}
        _DistortionStr("Distortion Strength", Range(0,1)) = 0.025
        _WaveSpeed("Wave Speed", Range(0,3)) = 0.5
        _WaveDensity("Wave Density", Range(0,10)) = 4
        
        _GeneralTint("General Tint", Color) = (1,1,1,1)
        _DistanceTintColorStart ("Distance Tint Color Start", Color) = (1,1,1,1)
        _DistanceTintColorEnd ("Distance Tint Color End", Color) = (1,1,1,1)
        
		[MaterialToggle] _EnablePixelation("Enable Pixelation", Float) = 0
		[MaterialToggle] _EnableGeneralTint("Enable General Tint", Float) = 0
		[MaterialToggle] _EnableDistanceTint("Enable Distance Tint", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }


        Pass
        {

            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            
            sampler2D _DistortionTex;
            float4 _DistortionTex_ST;
            float _DistortionStr;
            float _WaveSpeed;
            float _WaveDensity;
            
            float _EnablePixelation;
            float _EnableGeneralTint;
            float _EnableDistanceTint;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }
            
			fixed4 _GeneralTint;
			
			fixed4 _DistanceTintColorStart;
			fixed4 _DistanceTintColorEnd;
			
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the distortion texture
                fixed2 distortedUV = i.uv * _WaveDensity + _WaveSpeed * _Time.y;
                
                // Combine distortion texture and UV
				fixed4 distortionTex = tex2D(_DistortionTex, distortedUV);
				
				// Create regular and pixelated distortion
				fixed2 regularDistortionUV = i.uv * _MainTex_ST.xy + distortionTex.rg * _DistortionStr*i.uv.y;
                fixed2 pixelDistortionUV = i.uv * _MainTex_ST.xy + round((distortionTex.rg * _DistortionStr*i.uv.y)*_MainTex_TexelSize.zw)*_MainTex_TexelSize.xy;
				
                // Using either regular distortion or pixelated distortion based on the _EnablePixelation variable
                fixed2 distortion = lerp(regularDistortionUV,pixelDistortionUV,_EnablePixelation);
                           
                // Create a gradient texture to tint the water surface     
                fixed4 distanceTintGradient = lerp(_DistanceTintColorStart,_DistanceTintColorEnd,i.uv.y);
                fixed4 distanceTint = lerp(fixed4(1,1,1,0),distanceTintGradient,_EnableDistanceTint);
                
                // Create a general tint texture
                fixed4 generalTint = lerp(fixed4(1,1,1,0),_GeneralTint,_EnableGeneralTint);
                
                // Create the final texture to be returned
                fixed4 distortedMain = tex2D(_MainTex, distortion);
                
                // Apply the distance tint and the general tint
                fixed4 output = distortedMain * distanceTint * generalTint;
                
                // Return the final outcome
                return output;
            }
            ENDCG
        }
    }
}

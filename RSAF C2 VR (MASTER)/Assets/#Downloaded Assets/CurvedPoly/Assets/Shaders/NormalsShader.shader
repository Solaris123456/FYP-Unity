//This is a simple Unity Surface Shader (https://docs.unity3d.com/Manual/SL-SurfaceShaderExamples.html)
//we use normals here to assign colors to surfaces. 
//There are 2 normals schemas: a one-sided and a bi-sided ones.
//You can smoothly switch between them with the Parameter BisidedNormal
Shader "CurvedPoly/NormalsShader" {
	Properties { 
		//This parameter will smoothly choose the normals color schema to be used
		_BisidedNormal ("Bisided Normals ", Range(0,1)) = 0.0
		//Same as Metallic property in Unity Standard Shader
		_Metallic ("Metallic", Range(0,1)) = 0.0
		//Same as Smoothness property in Unity Standard Shader
		_Glossiness ("Smoothness", Range(0,1)) = 0.0
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
	
		//Most of the code here is taken from Unity Examples and documentation 
		//Find everything you need here:(https://docs.unity3d.com/Manual/SL-Reference.html)
		
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
 
		struct Input {
			float2 uv_MainTex;
		};
		
        sampler2D _MainTex;
		half _Glossiness;
		half _Metallic;
		half _BisidedNormal; 

		void surf (Input IN, inout SurfaceOutputStandard o) {   
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			//We map each normal component component to one of the color component here.
			//A color value is evaluated with a formulat which smoothly blends 
			//between the real value of the component or the 'abs' value of the component.
			//So: when _BisidedNormal==1, negative coordinates of the normals will
			//map to positive color components.
			//Mapping Normal.x to red component
			float r = abs(o.Normal.x)*(_BisidedNormal)+(o.Normal.x)*(1.0-_BisidedNormal);
			//Mapping Normal.y to green component
			float g = abs(o.Normal.y)*(_BisidedNormal)+(o.Normal.y)*(1.0-_BisidedNormal);
			//Mapping Normal.z to blue component
			float b = abs(o.Normal.z)*(_BisidedNormal)+(o.Normal.z)*(1.0-_BisidedNormal);
			//We assign the (r,g,b) color to the final Albedo Color here
			o.Albedo = float3(r,g,b)*c;  
			//Here instead we directly pass the metallic and smoothness parameters to 
			//Unity Pipeline
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

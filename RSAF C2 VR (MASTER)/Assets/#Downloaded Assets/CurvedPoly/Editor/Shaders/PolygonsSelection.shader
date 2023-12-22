/*Pretty Simple Color+Transparency Shader used to display Polygons Selections.*/
Shader "Custom/CurvedPolyEditor/PolygonsSelection" {
    Properties{
      _Color("Color", Color) = (1.0, 1.0, 1.0, 1.0) 
    }
    SubShader{
        Tags { "Queue" = "Transparent"  "RenderType" = "Transparent"}
        Lighting Off
        Blend SrcAlpha OneMinusSrcAlpha 
        ZWrite On
        Cull Off


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _Color;
            float _Dither;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;

                // https://www.opengl.org/discussion_boards/showthread.php/166719-Clean-Wireframe-Over-Solid-Mesh
                o.pos = float4(UnityObjectToViewPos(v.vertex.xyz), 1);
                o.pos.z *= 0.99;
                o.pos = mul(UNITY_MATRIX_P, o.pos);

                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                //i.pos.xy = floor(i.pos.xy * 1) * .5;
                //float checker = -frac(i.pos.x + i.pos.y);
                //clip(lerp(1, checker, _Dither));

                return _Color;
            }

            ENDCG
        }
 
        /*CGPROGRAM
        #pragma surface surf Lambert
 
        fixed4 _Color;
  
        struct Input {
          float2 uv_MainTex;
        };
 
        void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = float3(0,0,0);
          o.Emission = _Color.rgb;
          o.Alpha = _Color.a; 
        }
        ENDCG*/
    } 
    FallBack "Diffuse"
}
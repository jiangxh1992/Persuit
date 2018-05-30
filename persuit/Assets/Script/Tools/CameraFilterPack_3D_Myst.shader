Shader "CameraFilterPack/3D_Myst" 
{
Properties 
{
_MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader 
{
Pass
{
Cull Off ZWrite Off ZTest Always
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma target 3.0
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform sampler2D _MainTex2;
uniform float _TimeX;
uniform float2 _MainTex_TexelSize;

struct appdata_t
{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
float2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
float4 color    : COLOR;
float4 projPos : TEXCOORD1; 
};   

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}

float4 frag(v2f i) : COLOR
{
float2 uv = i.texcoord.xy;
#if SHADER_API_D3D9
if (_MainTex_TexelSize.y < 0)
uv.y = 1-uv.y;
#endif
float4 txt = tex2D(_MainTex,uv);

//float yOff = _WorldSpaceCameraPos.y;
//uv.y+=yOff*0.2;
uv.x+=_Time*2;
float4 txt2 = tex2D(_MainTex2,frac(uv));

uv.x+=_Time*2;
uv/=1.5;
float4 txt3 = tex2D(_MainTex2,frac(uv));

uv/=2.5;
float4 txt4 = tex2D(_MainTex2,frac(uv));

txt2.rgb=lerp(txt2.r,txt3.r,txt4.b);
txt2 = txt2.r+txt3.g+txt4.r;
txt2 = txt2*0.33;

txt = lerp(txt,txt+txt2,0.8);
return txt;
}

ENDCG
}

}
}
#ifndef PW_DETAILINDIRECT_INCLUDED
#define PW_DETAILINDIRECT_INCLUDED

sampler2D unity_DitherMask;

struct DetailData {
    float4x4 obj2World;
    float4x4 world2Obj;
};

float _detailData;
float4 _detailBehaviorData;
float4 _PW_DetailGlobals;

float FastDistance2D(float3 v1, float3 v2)
{
    float dx = abs(v1.x - v2.x);
    float dz = abs(v1.z - v2.z);
    return 0.394 * (dx + dz) + 0.554 * max(dx, dz);
}

void ColorVariationMix_float(in float4 colorA, in float4 colorB, in float3 texColor, out float3 colorOut)
{
    float greyscaled = dot(texColor.rgb, half3(0.333, 0.333, 0.333));
    float4 tintCol = lerp(colorA, colorB, _detailData.x);
    texColor.rgb = lerp(texColor.rgb, greyscaled.xxx, tintCol.a);
    colorOut = texColor.rgb * tintCol.rgb;
    
}

void DistanceFade_float(in float dist, out float distFadeOut)
{
  _detailBehaviorData.xy *= _PW_DetailGlobals.w;
  distFadeOut = 1-saturate(max(dist - _detailBehaviorData.x, 0.001f) / _detailBehaviorData.y);
}

void DitherCrossFade(float2 vpos,float fade)
{
    vpos /= 4; // the dither mask texture is 4x4
    float mask = tex2D(unity_DitherMask, vpos).a;
    clip(fade * fade - 0.001 - mask * mask); // needs to be improved
}

#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
#if defined(SHADER_API_GLCORE) || defined(SHADER_API_D3D11) || defined(SHADER_API_GLES3) || defined(SHADER_API_METAL) || defined(SHADER_API_VULKAN) || defined(SHADER_API_PSSL) || defined(SHADER_API_XBOXONE)
    StructuredBuffer<DetailData> detailBuffer;
#endif	
#endif


void setup()
{

   #define unity_ObjectToWorld unity_ObjectToWorld
   #define unity_WorldToObject unity_WorldToObject

    #ifndef	_VRI_DEBUG
        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            unity_ObjectToWorld = detailBuffer[unity_InstanceID].obj2World;
            unity_WorldToObject = detailBuffer[unity_InstanceID].world2Obj;
    
            _detailData = unity_ObjectToWorld._41; // extra grass data packed into matrix then zeroed
            unity_ObjectToWorld._41 = 0;
            
        #endif
    #endif
}

void DetailIndirect_float(float3 inPos, out float3 outPos)
{
    outPos = inPos;
}


#endif

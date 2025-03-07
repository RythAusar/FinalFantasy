sampler uImage0 : register(s0);
sampler uImage1 : register(s1); // Automatically Images/Misc/Perlin via Force Shader testing option
sampler uImage2 : register(s2); // Automatically Images/Misc/noise via Force Shader testing option
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float4x4 World;
float4x4 View;
floar4x4 Projection;

float4 AmbientColor = float4(1, 1, 1, 1);
float AmbientIntensity = 0.1;

struct VertexShaderInput
{
    float4 Position : POSITION0;
};

struct VertexShaderOutput{
float4 Position : POSITION0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
    
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    return AmbientColor * AmbientIntensity;
}

technique Ambient
{
    pass Pass1
    {
        VertexShader = compile ps_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();

    }
}
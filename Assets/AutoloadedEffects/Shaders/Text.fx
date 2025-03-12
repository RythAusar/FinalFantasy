sampler uImage0 : register(s0);
sampler uImage1 : register(s1);

float globalTime;
float3 mainColor;
float3 secondaryColor;

float4 PulseShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
    float wave = frac(globalTime + (coords.y));
    color.rgb *= (wave * mainColor) + ((1 - wave) * secondaryColor);
	return color * sampleColor * 2;

	//return color * tex2D(uImage0, coords).a;
}

float4 MyShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    // Sample the base texture color from uImage0 at the given coordinates
    float4 color = tex2D(uImage0, coords);
    
    // Calculate the distance from the center (assumed at (0.5, 0.5) in normalized coordinates)
    float2 center = float2(1, 1);
    float dist = distance(coords, center);
    
    // Create a pulsating wave effect:
    // - globalTime speeds up the oscillation.
    // - Multiplying the distance produces rings that radiate from the center.
    // - The sine function creates a smooth oscillation, and we remap it from [-1, 1] to [0, 1].
    float pulse = sin(globalTime * 7.0 - dist * 20.0) * 0.5 + 0.5;
    
    // Blend between secondaryColor and mainColor based on the pulse value.
    float3 blendedColor = lerp(secondaryColor, mainColor, pulse);
    // Modulate the texture's color by the blended color
    color.rgb *= blendedColor;
    
    // Combine with the incoming sampleColor and boost brightness
    return color * sampleColor * 5.0;
}


float4 ModifiedShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    //float wave = frac(globalTime + (coords.x * 10)); //sets a float number from 0 to 1
    float wave = sin(globalTime * 7.0 + coords.x * 10) * 0.5 + 0.5; //The sine function creates a smooth oscillation, and we remap it from [-1, 1] to [0, 1].
    float3 blendedColor = lerp(mainColor, secondaryColor, wave);
    color.rgb *= blendedColor;
    //color.rgb *= (wave * mainColor) + (wave + 0.5) * secondaryColor;
    return color * sampleColor * 2;
}

float4 DiagonalShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
	float wave = frac(globalTime / 2 + coords.y / 2 + coords.x / 2);//sets a float number from 0 to 1
    color.rgb *= (wave * mainColor) + ((1 - wave) * secondaryColor);
	

    return color * sampleColor * 2;

	//return color * tex2D(uImage0, coords).a;
}

float4 CircleShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float dist = sqrt(pow((coords.x - 0.5), 2) + pow((coords.y - 0.5), 2));
    
	float wave = frac(globalTime + dist);
    color.rgb *= (wave * mainColor) + ((1 - wave) * secondaryColor);
	

    return color * sampleColor * 2;

	//return color * tex2D(uImage0, coords).a;
}

technique Technique1
{
    pass MyShader
    {
        PixelShader = compile ps_2_0 MyShader();
    }

    pass ModifiedShader
    {
        PixelShader = compile ps_2_0 ModifiedShader();
    }

    pass AutoloadPass
    {
        PixelShader = compile ps_2_0 PulseShader();
    }
	pass PulseUpwards
	{
		PixelShader = compile ps_2_0 PulseShader();
	}

    pass PulseDiagonal
    {
        PixelShader = compile ps_2_0 DiagonalShader();
    }

    pass PulseCircle
    {
        PixelShader = compile ps_2_0 CircleShader();
    }
}
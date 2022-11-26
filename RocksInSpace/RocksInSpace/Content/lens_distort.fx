#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float2 computeUV( float2 uv, float k, float kcube ){
    
    float2 t = uv - .5;
    float r2 = t.x * t.x + t.y * t.y;
	float f = 0.;
    
    if( kcube == 0.0){
        f = 1. + r2 * k;
    }else{
        f = 1. + r2 * ( k + kcube * sqrt( r2 ) );
    }
    
    float2 nUv = f * t + .5;
    //nUv.y = 1. - nUv.y;
 
    return nUv;
    
}

struct VertexShaderOutput
{
	float4 Color : COLOR0;
	float2 texCoord : TEXCOORD0;
};

sampler2D texSampler;

float strength = 0.5;
float offset = 0.1;

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 uv = input.texCoord;
    float kcube = 0.0;

	float2 redUV = computeUV( uv, strength + offset, kcube );
	if (redUV.x < 0. || redUV.x > 1. || redUV.y < 0. || redUV.y > 1.) { discard; }
	float2 greenUV = computeUV( uv, strength, kcube );
	if (redUV.x < 0. || redUV.x > 1. || redUV.y < 0. || redUV.y > 1.) { discard; }
	float2 blueUV = computeUV( uv, strength - offset, kcube );
	if (redUV.x < 0. || redUV.x > 1. || redUV.y < 0. || redUV.y > 1.) { discard; }

    float red = tex2D( texSampler, redUV ).r; 
    float green = tex2D( texSampler, greenUV ).g; 
    float blue = tex2D( texSampler, blueUV ).b; 

	return float4(red, green, blue, 1.);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};
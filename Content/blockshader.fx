#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

float time;
bool isMagic;

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 col = tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color;
	
    if (isMagic)
    {
        //if (time % 2 < 1)
        //{
        //    col.r *= time % 2;
        //}
        //else
        //{
        //    col.r *= 2 - time % 2;
        //}
        
        
        col.r = 1;
        col.g = col.r/2;
        col.b = 1 - abs((floor(time % 1 * 20) - floor(input.TextureCoordinates.x * 20))) / 20;
        
        
        //col.rgb = time;
        //if (input.TextureCoordinates == time % 1)
        //{
        //    col.r = 1;
        //}
    }
    else
    {
        col.r *= floor(input.TextureCoordinates.x * 10) / 10;
        col.g *= floor(input.TextureCoordinates.y * 10) / 10;
        col.b *= floor(input.TextureCoordinates.x * 5) / 5;    
    }
    
    return col;
	
	
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};
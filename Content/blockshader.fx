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

float R;
float G;
float B;

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


        
    col.r = R;
    col.g = G * floor(input.TextureCoordinates.y * 5) / 5;
    col.b = B;
        
    if (isMagic == true)
    {
        float t = time % 2;

        if (t < 1.0)
        {
            col.b = 1 - abs((floor(time % 1 * 20) - floor(input.TextureCoordinates.y * 10))) / 20;
            col.b = 1 - abs((floor(time % 1 * 20) - floor(input.TextureCoordinates.x * 20))) / 20;
        }
        else
        {
            col.b = abs((floor(time % 1 * 20) - floor(input.TextureCoordinates.y * 10))) / 20;
            col.b = abs((floor(time % 1 * 20) - floor(input.TextureCoordinates.x * 20))) / 20;
        }
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
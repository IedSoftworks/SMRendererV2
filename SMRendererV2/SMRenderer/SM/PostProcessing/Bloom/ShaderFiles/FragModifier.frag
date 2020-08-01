#version 330

struct BloomOption {
	int Type;
	sampler2D Texture;
	vec4 Color;
};

in vec2 vTexture;

out vec4 color;
out vec4 bloom;

uniform BloomOption BloomOptions;
uniform vec3 AmbientLight;

vec3 LightOutput;

void main() {
	bloom = vec4(0,0,0,1);

	switch(BloomOptions.Type) {
		// if no bloom nothing happends
		case 1: // color-output as bloom
			bloom = color;
			break;
		case 2: // Bloom color
			bloom = BloomOptions.Color;
			break;
		case 3: // Bloom Texture
			bloom = texture(BloomOptions.Texture, vTexture);
			break;
		case 4: // Lighting output
			bloom.xyz = LightOutput;
			break;
		case 5: // LightOutput with ambient
			bloom.xyz = AmbientLight + LightOutput;
			break;
	}
}
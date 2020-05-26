#version 330

struct Material {
	vec4 ObjectColor;
	int UseTexture;
};

in vec2 vTexture;

out vec4 color;
out vec4 bloom;

uniform Material material;
uniform sampler2D Texture;

float colThreshold = 0.1;

void main() {
	color = material.ObjectColor;
	if(material.UseTexture == 1)
		color *= texture(Texture, vTexture);


	bloom = color;
}
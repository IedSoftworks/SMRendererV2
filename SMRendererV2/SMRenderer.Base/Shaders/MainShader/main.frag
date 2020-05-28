#version 330

struct Material {
	vec4 ObjectColor;
	int UseTexture;
};

in vec2 vTexture;
in vec4 vColor;

out vec4 color;
out vec4 bloom;

uniform Material material;
uniform sampler2D Texture;

vec4 CalulateLight();

void main() {
	color = material.ObjectColor * vColor;
	if(material.UseTexture == 1)
		color *= texture(Texture, vTexture);

	vec4 light = CalulateLight();

	bloom = color;
}
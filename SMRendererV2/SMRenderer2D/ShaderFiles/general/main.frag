#version 330

struct Material {
	vec4 ObjectColor;
};

in vec2 vTexture;

out vec4 color;
out vec4 bloom;

uniform Material material;
uniform sampler2D Texture;

float colThreshold = 0.1;

void main() {
	vec4 tex = texture(Texture, vTexture);

	color = tex * material.ObjectColor;

	bloom = vec4(1,1,1,1);
}
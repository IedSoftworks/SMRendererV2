#version 330

in vec2 vTexture;

out vec4 color;

uniform sampler2D Texture;
uniform vec4 ObjectColor;

float colThreshold = 0.1;

void main() {
	vec4 tex = texture(Texture, vTexture);

	color = tex * ObjectColor;
}
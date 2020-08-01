#version 330

in vec2 vTexture;

out vec4 color;

uniform sampler2D BloomTexture;
uniform sampler2D SceneTexture;

uniform float Percentage;

void main() {
	color = vec4(texture(SceneTexture, vTexture).rgb + texture(BloomTexture, vTexture).rgb, 1);
}
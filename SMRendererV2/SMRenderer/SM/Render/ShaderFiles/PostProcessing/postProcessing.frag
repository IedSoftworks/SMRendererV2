#version 330

in vec2 vFragPos;
in vec2 vTexture;

out vec4 finalColor;

uniform sampler2D Scene;

vec4 originalScene;

void main() {
	originalScene = finalColor = texture(Scene, vTexture);
}
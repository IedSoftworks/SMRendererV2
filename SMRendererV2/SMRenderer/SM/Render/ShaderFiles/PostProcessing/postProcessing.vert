#version 330

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexture;


out vec2 vTexture;
out vec2 vFragPos;

uniform mat4 MVP;

void main() {
	vTexture = aTexture;
	gl_Position = MVP * vec4(aPosition,0);
}
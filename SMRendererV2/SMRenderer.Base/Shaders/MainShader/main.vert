#version 330

in vec3 aPosition;
in vec2 aTexture;
in vec3 aNormal;

out vec2 vTexture;
out vec3 vNormal;
out vec3 vPosition;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main() {
	vTexture = aTexture;
	vNormal = aNormal;
	vPosition = vec3(model * vec4(aPosition,1));

	gl_Position = projection * view * model * vec4(aPosition, 1.0);
}
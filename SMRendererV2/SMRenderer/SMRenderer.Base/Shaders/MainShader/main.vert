#version 330

in vec3 aPosition;
in vec2 aTexture;
in vec3 aNormal;
in vec4 aColor;

out vec2 vTexture;
out vec3 vNormal;
out vec3 vPosition;
out vec4 vColor;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

uniform int HasColors;

void main() {
	vTexture = aTexture;
	vNormal = aNormal;
	vPosition = vec3(model * vec4(aPosition,1));

	if (HasColors < 1) vColor = vec4(1,1,1,1);
	else vColor = aColor;

	gl_Position = projection * view * model * vec4(aPosition, 1.0);
}
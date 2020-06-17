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

uniform vec3[2048] Motions; 
uniform float Time;
uniform float Fade;

void main() {
	vTexture = aTexture;
	vNormal = aNormal;

	vec3 pos = aPosition;
	pos += Motions[gl_InstanceID] * Time;
	vPosition = (model * vec4(pos,1)).xyz;

	if (HasColors < 1) vColor = vec4(1,1,1,1);
	else vColor = aColor;
	vColor.w *= Fade;

	gl_Position = projection * view * model * vec4(pos, 1.0);
}
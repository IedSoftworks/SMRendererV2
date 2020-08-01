#version 330
#define MAX_PARTICLES //#particleCount

in vec3 aPosition;
in vec2 aTexture;
in vec3 aNormal;
in vec4 aColor;
in vec3 aTangent;

out vec2 vTexture;
out vec3 vNormal;
out vec3 vPosition;
out vec4 vColor;
out mat3 vTBN;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

uniform int HasColors;

uniform mat4[MAX_PARTICLES] Matrices;
uniform float Fade;

void main() {
	mat4 modelMatrix = model * Matrices[gl_InstanceID];

	vTexture = aTexture;
	vNormal = mat3(transpose(inverse(modelMatrix)))  * aNormal;

	vPosition = (modelMatrix * vec4(aPosition,1)).xyz;

	if (HasColors < 1) vColor = vec4(1,1,1,1);
	else vColor = aColor;
	vColor.w *= Fade;

	gl_Position = projection * view * vec4(vPosition, 1.0);
	
	vec3 T = normalize(vec3(modelMatrix * vec4(aTangent, 0.0)));
	vec3 N = normalize(vec3(modelMatrix * vec4(aNormal, 0.0)));
	vec3 B = cross(N, T);

	vTBN = mat3(T,B,N);
}
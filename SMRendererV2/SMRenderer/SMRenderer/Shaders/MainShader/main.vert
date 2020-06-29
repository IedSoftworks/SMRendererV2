#version 330
#define MAX_CALLPARAMETERS //#maximalCallParameters

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexture;
layout(location = 2) in vec3 aNormal;
layout(location = 3) in vec4 aColor;

out vec2 vTexture;
out vec3 vNormal;
out vec3 vPosition;
out vec4 vColor;

uniform mat4 projection;
uniform mat4 view;
uniform mat4[MAX_CALLPARAMETERS] model;
uniform mat4 normal;

uniform int HasColors;

uniform vec2[MAX_CALLPARAMETERS] TexOffset;
uniform vec2[MAX_CALLPARAMETERS] TexSize;

void main() {
	vTexture = aTexture * TexSize[gl_InstanceID] + TexOffset[gl_InstanceID];

	vNormal = vec3(normal * vec4(aNormal, 1));
	vPosition = vec3(model[gl_InstanceID] * vec4(aPosition,1));

	if (HasColors < 1) vColor = vec4(1,1,1,1);
	else vColor = aColor;

	gl_Position = projection * view * model[gl_InstanceID] * vec4(aPosition, 1.0);
}
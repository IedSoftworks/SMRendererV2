#version 330
#define MAX_CALLPARAMETERS //#maximalCallParameters

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexture;
layout(location = 2) in vec3 aNormal;
layout(location = 3) in vec4 aColor;
layout(location = 4) in vec3 aTangent;

out vec2 vTexture;
out vec3 vNormal;
out vec3 vPosition;
out vec4 vColor;
out mat3 vTBN;

uniform mat4 projection;
uniform mat4 view;
uniform mat4[MAX_CALLPARAMETERS] model;

uniform bool HasColors;

uniform mat4 masterMatrix;
uniform bool HasMasterMatrix;

uniform vec2[MAX_CALLPARAMETERS] TexOffset;
uniform vec2[MAX_CALLPARAMETERS] TexSize;

void main() {
	mat4 modelMatrix;
	if (HasMasterMatrix) 
		modelMatrix = masterMatrix * model[gl_InstanceID];
	else modelMatrix = model[gl_InstanceID];

	vTexture = aTexture * TexSize[gl_InstanceID] + TexOffset[gl_InstanceID];

	vNormal = mat3(transpose(inverse(modelMatrix)))  * aNormal;
	vPosition = vec3(modelMatrix * vec4(aPosition,1));

	if (!HasColors) vColor = vec4(1,1,1,1);
	else vColor = aColor;

	gl_Position = projection * view * vec4(vPosition, 1.0);

	vec3 T = normalize(vec3(modelMatrix * vec4(aTangent, 0.0)));
	vec3 N = normalize(vec3(modelMatrix * vec4(aNormal, 0.0)));
	vec3 B = cross(N, T);

	vTBN = mat3(T,B,N);
}
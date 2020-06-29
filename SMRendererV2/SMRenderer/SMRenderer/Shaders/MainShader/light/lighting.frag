#version 330
#define MAX_LIGHTS //#maxLights

struct Light {
	int Type;
	vec3 Position;
	vec3 Direction;
	vec3 Size;
	vec3 Color;
};

in vec3 vNormal;
in vec3 vPosition;

uniform vec3 AmbientLight;
uniform Light[MAX_LIGHTS] Lights;
uniform int UsedLights;

out vec4 color;

vec3 CalcSun(Light light) {
	return vec3(0);
}

vec3 CalcSpotlight(Light light) {
	return vec3(0);
}

vec3 CalcPoint(Light light) {
	return vec3(0);
	/*vec3 norm = normalize(vNormal);
	vec3 lightDir = normalize(light.Position - vPosition);

	float diff = max(dot(norm, lightDir), 0f);
	return diff * light.Color;*/
}

void CalcLight() {
	vec3 result = AmbientLight;

	for(int i = 0; i < UsedLights; i++) {
		switch(Lights[i].Type) {
			case 0:
				result += CalcPoint(Lights[i]);
				break;
			case 1:
				result += CalcSpotlight(Lights[i]);
				break;
			case 2:
				result += CalcSun(Lights[i]);
				break;
		}
	}

	color *= result;
}
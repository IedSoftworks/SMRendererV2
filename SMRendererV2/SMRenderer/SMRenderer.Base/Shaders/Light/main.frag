#version 330

in vec3 vNormal;
in vec3 vPosition;

struct Phong {
	vec3 Direction;
};

struct Light {
	int Type;
	vec4 Color;
	vec3 Position;
	Phong Phong;
};

struct LightOptions {
	vec4 ambientLight;
	Light lights;
};

uniform LightOptions light;

vec4 CalcPhong(vec3 lightPos, vec4 lightColor, Phong phong);

vec4 CalulateLight() {
	vec4 result = light.ambientLight;

	if (light.lights.Type == 0) {
		result += CalcPhong(light.lights.Position, light.lights.Color, light.lights.Phong);
	}

	return result;
}
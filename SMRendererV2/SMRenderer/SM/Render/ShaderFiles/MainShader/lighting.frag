#version 330
#define MAX_LIGHTS //#maxLights

struct PointLight {
	vec3 Position;

	float Constant;
	float Linear;
	float Quadratic;
};

struct SpotlightLight {
	vec3 Position;
	vec3 Direction;

	float OuterCutoff;
	float InnerCutoff;
};

struct SunLight {
	vec3 Direction;
};

struct Light {
	int Type;

	vec3 Diffuse;
	vec3 Specular;

	PointLight Point;
	SpotlightLight Spotlight;
	SunLight Sun;
};

struct Material {
	sampler2D SpecularTexture;
	sampler2D NormalMap;

	vec3 Specular;

	bool UseSpecularTexture;
	bool UseNormalMap;
	bool UseLight;
	
	float Shininess;
};

in vec3 vNormal;
in vec2 vTexture;
in vec3 vPosition;
in mat3 vTBN;

out vec4 color;

uniform float AmbientLight;
uniform Light[MAX_LIGHTS] Lights;
uniform int UsedLights;
uniform vec3 ViewPosition;

uniform Material material;

vec3 LightOutput;
vec3 Normal;
vec3 viewDir;

vec3 CalcSun(Light light) {
	vec3 lightDir = normalize(-light.Sun.Direction);

	float diff = max(dot(Normal, lightDir), 0);

	vec3 halfwayDir = normalize(lightDir + viewDir);

	float spec = pow(max(dot(Normal, halfwayDir), 0.0), material.Shininess);

	vec3 specTex = material.UseSpecularTexture ? vec3(texture(material.SpecularTexture, vTexture)) : vec3(1);

	return diff * light.Diffuse + (spec * specTex) * light.Specular;
}

vec3 CalcSpotlight(Light light) {
	SpotlightLight spot = light.Spotlight;

	vec3 lightDir = normalize(spot.Position - vPosition);

	float theta = dot(lightDir, normalize(-spot.Direction));
	float epsilon = spot.InnerCutoff - spot.OuterCutoff;
	float intensity = clamp((theta - spot.OuterCutoff) / epsilon, 0.0, 1.0);

	float diff = max(dot(Normal, lightDir), 0);

	vec3 reflectDir = reflect(-lightDir, Normal);

	float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.Shininess);

	vec3 specTex = material.UseSpecularTexture ? vec3(texture(material.SpecularTexture, vTexture)) : vec3(1);

	return diff * light.Diffuse * intensity + (spec * specTex) * light.Specular * intensity;
}

vec3 CalcPoint(Light light) {
	PointLight point = light.Point;

	vec3 lightDir = normalize(point.Position - vPosition);

	float diff = max(dot(Normal, lightDir), 0);

	vec3 reflectDir = reflect(-lightDir, Normal);

	float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.Shininess);

	vec3 specTex = material.UseSpecularTexture ? vec3(texture(material.SpecularTexture, vTexture)) : vec3(1);

	float distance = length(point.Position - vPosition);
	float attenuation = 1.0 / (point.Constant + point.Linear * distance + point.Quadratic * (distance * distance));

	return diff * light.Diffuse * attenuation + (spec * specTex) * light.Specular * attenuation;
}


vec3 CalcLight() {
	vec3 result = vec3(0);

	
	viewDir = normalize(ViewPosition - vPosition);

	if (material.UseNormalMap) {
		Normal = normalize(vTBN * (texture(material.NormalMap, vTexture).rgb * 2.0 - 1.0));
	} else Normal = normalize(vNormal);

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

	return result;
}

void CalculateLighting() {
	if (material.UseLight) {
		LightOutput = CalcLight();
		color.xyz *= AmbientLight;
		color.xyz += LightOutput;
	}
}
#version 150
struct Material {
	sampler2D DiffuseTexture;
	sampler2D SpecularTexture;
	sampler2D NormalMap;

	vec4 Diffuse;
	vec3 Specular;

	bool UseDiffuseTexture;
	bool UseSpecularTexture;
	bool UseNormalMap;
	bool UseLight;
	
	float Shininess;

	//#MaterialExtention
};

in vec2 vTexture;
in vec3 vNormal;
in vec3 vPosition;
in vec4 vColor;

out vec4 color;

uniform Material material;

void main() {
	color = material.Diffuse * vColor;
	if(material.UseDiffuseTexture)
		color *= texture(material.DiffuseTexture, vTexture);
}
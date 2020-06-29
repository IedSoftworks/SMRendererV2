#version 330
struct Material {
	vec4 ObjectColor;
	int UseTexture;
};
//#Structs

in vec3 vPosition;
in vec2 vTexture;
in vec3 vNormal;
in vec4 vColor;
//#In

out vec4 color;
//#Out

uniform Material material;
uniform sampler2D DiffuseTexture;
//#Uniform

vec4 Texture;
//#Variables

void CalcLight();

void main() {
	color = material.ObjectColor * vColor;
	if(material.UseTexture == 1)
		color *= Texture = texture(DiffuseTexture, vTexture);
		
	CalcLight();
	//#Main
}
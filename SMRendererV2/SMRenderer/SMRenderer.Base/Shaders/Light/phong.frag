#version 330

in vec3 vPosition;
in vec3 vNormal;

struct Phong {
	vec3 Direction;
};

vec4 CalcPhong(vec3 lightPos, vec4 lightColor, Phong phong) {
	vec3 norm = normalize(vNormal);
	vec3 lightDir = normalize(lightPos - vPosition);

	float diff = max(dot(norm, phong.Direction), 0);
	return diff * lightColor;
}
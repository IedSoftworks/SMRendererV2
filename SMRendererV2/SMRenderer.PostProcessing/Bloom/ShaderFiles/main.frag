#version 330

in vec2 vTexture;

out vec4 color;

uniform sampler2D BloomTex;
uniform sampler2D Scene;

uniform int Merge;
uniform int Horizontal;

float weight[9] = float[] (.4, .2,.1,.075,.05,.01,.0075,.005,.0001);
vec2 tex_offset = vec2(1,1);

float bloomSizeFactor = 0.00075;
float multiplier = 2;

void main() {

	vec3 result = texture(BloomTex, vTexture).rgb;

	if (Horizontal == 1) {
		for(int i = 1; i < 9; i++) {
			result += texture(BloomTex, vTexture + vec2(tex_offset.x * (float(i) * (bloomSizeFactor * float(i))), 0.0)).rgb * (weight[i] * multiplier);
			result += texture(BloomTex, vTexture - vec2(tex_offset.x * (float(i) * (bloomSizeFactor * float(i))), 0.0)).rgb * (weight[i] * multiplier);
		}
		color.x = result.x;
		color.y = result.y;
		color.z = result.z;	
		color.w = 1;
	}
	else {
		for(int i = 1; i < 9; i++) {
			result += texture(BloomTex, vTexture + vec2(0.0, tex_offset.y * (float(i) * (bloomSizeFactor * float(i))))).rgb * (weight[i] * multiplier);
			result += texture(BloomTex, vTexture - vec2(0.0, tex_offset.y * (float(i) * (bloomSizeFactor * float(i))))).rgb * (weight[i] * multiplier);
		}
		if(Merge == 1) {
			result += texture(Scene, vTexture).rgb;
		}
		color.x = result.x;
		color.y = result.y;
		color.z = result.z;
		color.w = 1;
	}
}
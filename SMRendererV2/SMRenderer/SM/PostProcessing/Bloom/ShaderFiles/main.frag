#version 330

in vec2 vTexture;

out vec4 color;

uniform sampler2D BloomTex;

uniform int Horizontal;

uniform float weight[16];
uniform int weightCount;
uniform vec2 tex_offset;

uniform float bloomSizeFactor;
uniform float multiplier;

void main() {

	vec3 result = texture(BloomTex, vTexture).rgb;

	if (Horizontal == 1) {
		for(int i = 1; i < weightCount; i++) {
			result += texture(BloomTex, vTexture + vec2(tex_offset.x * (float(i) * (bloomSizeFactor * float(i))), 0.0)).rgb * (weight[i] * multiplier);
			result += texture(BloomTex, vTexture - vec2(tex_offset.x * (float(i) * (bloomSizeFactor * float(i))), 0.0)).rgb * (weight[i] * multiplier);
		}
	}
	else {
		for(int i = 1; i < weightCount; i++) {
			result += texture(BloomTex, vTexture + vec2(0.0, tex_offset.y * (float(i) * (bloomSizeFactor * float(i))))).rgb * (weight[i] * multiplier);
			result += texture(BloomTex, vTexture - vec2(0.0, tex_offset.y * (float(i) * (bloomSizeFactor * float(i))))).rgb * (weight[i] * multiplier);
		}
	}
	color = vec4(result, 1);
}
shader_type canvas_item;

uniform float _frameDuration = 0.1; // seconds each frame
uniform sampler2D _frameSequence;
uniform sampler2DArray _frames : filter_nearest;

float fmod(float a, float b) { // float modulo
	float c = a / b;
	return b * (c - floor(c));
} // end fmod()

void fragment() {
	int frameCount = textureSize(_frames, 0).z; // number of distinct frame textures
	int seqCount = textureSize(_frameSequence, 0).x; // number of frames in loop

	float loopLength = float(seqCount) * _frameDuration; // duration of loop in seconds
	int seqInd = int(fmod(TIME, loopLength) / _frameDuration); // surrent position in the sequence
	float frameInd = texelFetch(_frameSequence, ivec2(seqInd, 0), 0).r * float(frameCount); // get frame index to show. was cramped into 0 - 1 size, so needs to be expand to match frame count

	COLOR = texture(_frames, vec3(UV.xy, round(frameInd)));
} // end fragment()
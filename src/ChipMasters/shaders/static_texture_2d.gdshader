shader_type canvas_item;

uniform sampler2D _texture : filter_nearest;

void fragment() {
	COLOR = texture(_texture, UV);
} // end fragment()
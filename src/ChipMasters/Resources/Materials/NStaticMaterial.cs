using Godot;

namespace ChipMasters.Resources.Materials
{
    /// <summary>
    /// <see cref="Material"/> that renders an animated texture through a sequence of frames.
    /// </summary>
    public partial class NStaticMaterial : ShaderMaterial
    {
        private const string SHADER_PATH = "res://shaders/static_texture_2d.gdshader";
        private const string TEXTURE_UNIFORM = "_texture";

        private static readonly Shader SHADER_2D = GD.Load<Shader>(SHADER_PATH);



        /// <inheritdoc/>
        public NStaticMaterial(Texture2D texture)
        {
            Shader = SHADER_2D;

            SetShaderParameter(TEXTURE_UNIFORM, texture.AssertNotNull());
        } // end ctor
    } // end class
} // end namespace
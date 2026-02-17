using Godot;
using Godot.Collections;
using GSR.Utilic.Generic;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.Resources.Materials
{
    /// <summary>
    /// <see cref="Material"/> that renders an animated texture through a sequence of frames.
    /// </summary>
    public partial class NAnimatedMaterial : ShaderMaterial
    {
        private const string SHADER_2D_PATH = "res://shaders/animated_texture_2d.gdshader";
        private const string SHADER_3D_PATH = "res://shaders/animated_texture_3d.gdshader";
        private const string FRAME_DURATION_UNIFORM = "_frameDuration";
        private const string FRAME_SEQUENCE_UNIFORM = "_frameSequence";
        private const string FRAMES_UNIFORM = "_frames";

        private static readonly Shader SHADER_2D = GD.Load<Shader>(SHADER_2D_PATH);
        private static readonly Shader SHADER_3D = GD.Load<Shader>(SHADER_3D_PATH);



        /// <inheritdoc/>
        public NAnimatedMaterial(IEnumerable<Image> frames, VAnimData animData, EMode mode)
        {
            Texture2DArray packedFrames = new();
            packedFrames.CreateFromImages(new Array<Image>(frames.AssertNotNull().Select(x => x.AssertNotNull())));

            Image packedAnimData = Image.CreateEmpty(animData.FrameSequence.Count, 1, false, Image.Format.Rgba8);
            animData.FrameSequence.ForEvery((x, i) => packedAnimData.SetPixel(i, 0, new(((float)x) / ((float)packedFrames.GetLayers()), 0, 0))); // pack animData into an image, necessary to pass into shader(arrays must always be of fixed length).

            Shader = mode switch
            {
                EMode._2D => SHADER_2D,
                EMode._3D => SHADER_3D,
                _ => throw new System.InvalidOperationException("Mode note defined"),
            };

            SetShaderParameter(FRAME_DURATION_UNIFORM, animData.FrameDuration);
            SetShaderParameter(FRAME_SEQUENCE_UNIFORM, ImageTexture.CreateFromImage(packedAnimData));
            SetShaderParameter(FRAMES_UNIFORM, packedFrames);
        } // end ctor



        /// <summary>
        /// What the animated material's meant to be used for.
        /// </summary>
        public enum EMode
        {
            /// <summary>
            /// Material's meant for a <see cref="CanvasItem"/>.
            /// </summary>
            _2D,
            /// <summary>
            /// Material is meant for a 3d Mesh.
            /// </summary>
            _3D,
        } // end Mode()
    } // end class
} // end namespace
using Godot;

namespace ChipMasters.Resources.Materials
{
    /// <summary>
    /// Class that holds the <see cref="Material"/>s for a given texture.
    /// </summary>
    public interface IMaterialData
    {
        /// <summary>
        /// <see cref="Material"/> for rendering material on 2d canvases.
        /// </summary>
        Material Material2D { get; }

        /// <summary>
        /// <see cref="Material"/> for rendering texture on 3d geometry.
        /// </summary>
        Material Material3D { get; }
    } // end interface
} // end namespace
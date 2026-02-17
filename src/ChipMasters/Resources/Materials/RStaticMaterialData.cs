using Godot;
using System;

namespace ChipMasters.Resources.Materials
{
    /// <summary>
    /// <see cref="IMaterialData"/> for a static texture.
    /// </summary>
    public sealed class RStaticMaterialData : IMaterialData
    {
        /// <inheritdoc/>
        public Material Material2D => _material2D.Value;
        private readonly Lazy<Material> _material2D;

        /// <inheritdoc/>
        public Material Material3D => _material3D.Value;
        private readonly Lazy<Material> _material3D;



        /// <inheritdoc/>
        public RStaticMaterialData(Texture2D texture)
        {
            texture.AssertNotNull();
            _material2D = new(() => new NStaticMaterial(texture));
            _material3D = new(() =>
                new StandardMaterial3D()
                {
                    AlbedoTexture = texture,
                    Transparency = BaseMaterial3D.TransparencyEnum.Alpha,
                    TextureFilter = BaseMaterial3D.TextureFilterEnum.Nearest
                });
        } // end ctor

    } // end class
} // end namespace
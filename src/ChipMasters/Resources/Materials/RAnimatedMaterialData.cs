using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Resources.Materials
{
    /// <summary>
    /// <see cref="IMaterialData"/> for an animated texture.
    /// </summary>
    public sealed class RAnimatedMaterialData : IMaterialData
    {
        /// <inheritdoc/>
        public Material Material2D => _material2D.Value;
        private readonly Lazy<Material> _material2D;

        /// <inheritdoc/>
        public Material Material3D => _material3D.Value;
        private readonly Lazy<Material> _material3D;



        /// <inheritdoc/>
        public RAnimatedMaterialData(IEnumerable<Image> frames, VAnimData animData)
        {
            IImmutableList<Image> f = frames.AssertNotNull().Select(x => x.AssertNotNull()).ToImmutableList();

            _material2D = new(() => new NAnimatedMaterial(f, animData, NAnimatedMaterial.EMode._2D));
            _material3D = new(() => new NAnimatedMaterial(f, animData, NAnimatedMaterial.EMode._3D));
        } // end ctor

    } // end class
} // end namespace
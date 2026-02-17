using Godot;

namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// <see cref="Godot.AnimatedSprite2D"/> that implements <see cref="IAnimatedSprite2D"/> interface.
    /// </summary>
    public partial class NAnimatedSprite2D : AnimatedSprite2D, IAnimatedSprite2D
    {
        /// <inheritdoc/>
        public void Play(string? _name/*needed to distinguish the overloads*/ = null, float customSpeed = 1, bool fromEnd = false) => Play(name: (StringName)_name!, customSpeed, fromEnd);
    } // end class
} // end namespace
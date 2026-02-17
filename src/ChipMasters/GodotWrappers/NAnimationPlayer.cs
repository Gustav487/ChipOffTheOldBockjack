using Godot;

namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// <see cref="Godot.AnimationPlayer"/> that implements <see cref="IAnimationPlayer"/> interface.
    /// </summary>
    public partial class NAnimationPlayer : AnimationPlayer, IAnimationPlayer
    {
        /// <inheritdoc/>
        public void Play(string? _name = null, double customBlend = -1, float customSpeed = 1, bool fromEnd = false)
            => Play(name: (StringName)_name!, customBlend, customSpeed, fromEnd);

        /// <inheritdoc/>
        public void PlayBackwards(string? _name = null, double customBlend = -1)
            => Play(name: (StringName)_name!, customBlend);

    } // end class
} // end namespace
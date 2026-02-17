namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.AnimationPlayer"/>, see also ChipMasters.GodotWrappers.<see cref="NAnimationPlayer"/>.
    /// </summary>
    public interface IAnimationPlayer : IAnimationMixer
    {
        /// <summary>
        /// Refer to <see cref="Godot.AnimationPlayer.Play(Godot.StringName, double, float, bool)"/>.
        /// </summary>
        void Play(string? name = null, double customBlend = -1d, float customSpeed = 1, bool fromEnd = false);

        /// <summary>
        /// Refer to <see cref="Godot.AnimationPlayer.PlayBackwards(Godot.StringName, double)"/>.
        /// </summary>
        void PlayBackwards(string? name = null, double customBlend = -1d);

        /// <summary>
        /// Refer to <see cref="Godot.AnimationPlayer.Stop(bool)"/>.
        /// </summary>
        void Stop(bool keepState = false);
    } // end interface
} // end namespace
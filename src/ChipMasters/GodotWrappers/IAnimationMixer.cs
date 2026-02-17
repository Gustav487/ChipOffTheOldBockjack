namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.AnimationMixer"/>
    /// </summary>
    // , see also ChipMasters.GodotWrappers.<see cref="NAnimationMixer"/>.
    public interface IAnimationMixer
    {
        /// <summary>
        /// Refer to <see cref="Godot.AnimationMixer.Advance(double)"/>.
        /// </summary>
        void Advance(double delta);
    } // end interface
} // end namespace
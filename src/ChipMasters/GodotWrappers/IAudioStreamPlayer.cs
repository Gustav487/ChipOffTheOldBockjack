namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.AudioStreamPlayer"/>, see also ChipMasters.GodotWrappers.<see cref="NAudioStreamPlayer"/>.
    /// </summary>
    public interface IAudioStreamPlayer : INode
    {
        /// <summary>
        /// Refer to <see cref="Godot.AudioStreamPlayer.Playing"/>.
        /// </summary>
        bool Playing { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.AudioStreamPlayer.Play(float)"/>.
        /// </summary>
        void Play(float fromPosition = 0f);

        /// <summary>
        /// Refer to <see cref="Godot.AudioStreamPlayer.Stop"/>.
        /// </summary>
        void Stop();
    } // end interface
} // end namespace
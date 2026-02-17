using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Audio
{
    /// <summary>
    /// Marks a scene with a music stream to be played automatically when the scene is loaded.
    /// Attach this script to any node in a scene (like a marker), and assign the music stream.
    /// </summary>
    public partial class NMusicChangeDirective : NNode
    {
        /// <summary>
        /// The music stream that should play when this scene is active.
        /// </summary>
        public AudioStream MusicToPlay => _musicToPlay;

        [Export] private AudioStream _musicToPlay = null!;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _musicToPlay.AssertNotNull();
        } // end _Ready()

    } // end class
} // end namespace 
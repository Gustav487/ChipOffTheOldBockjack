using Godot;

namespace ChipMasters.Audio
{
    /// <summary>
    /// Global audio manager responsible for switching music based on scene state.
    /// Automatically switches tracks when a directive node enters or exits the tree.
    /// </summary>
    public partial class NAudioManager : Node
    {
        /// <summary>
        /// Audio player responsible for background music.
        /// </summary>
        [Export] private AudioStreamPlayer _musicPlayer = null!;

        /// <summary>
        /// Currently playing music stream.
        /// </summary>
        private AudioStream? _currentMusic;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _musicPlayer.AssertNotNull();
            GetTree().NodeAdded += TryApplyDirective;
        } // end _Ready();

        /// <summary>
        /// Apply a new directive when entering a scene or sub-scene.
        /// Only changes music if the directive is unique and has a different stream.
        /// </summary>
        /// <param name="node">A node that was added to the tree that may be a music change directive..</param>
        public void TryApplyDirective(Node node)
        {
            if (node is not NMusicChangeDirective directive)
                return;

            AudioStream mtp = directive.MusicToPlay;
            if (_currentMusic == mtp)
                return;

            PlayStream(mtp);
        } // end ApplyDirective()



        /// <summary>
        /// Internal method to play a new stream if it differs from the current.
        /// </summary>
        /// <param name="stream">The audio stream to play.</param>
        private void PlayStream(AudioStream stream)
        {
            if (_currentMusic == stream)
                return;

            _musicPlayer.Stream = stream;
            _musicPlayer.Play();
            _currentMusic = stream;
        } // end PlayStream()
    } // end class
} // end namespace
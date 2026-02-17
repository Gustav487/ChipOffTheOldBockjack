using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Audio
{
    /// <summary>
    /// <see cref="NButton"/> for playing a sounds when pressed
    /// </summary>
#warning N-R split
    public sealed partial class NAudioButton : NButton
    {
        [Export] private AudioStream _clickSound = null!;



        /// <inheritdoc/>
        public override void _Ready()
        {
            _clickSound.AssertNotNull();
            Pressed += OnButtonPressed;

        } // end _Ready()

        private void OnButtonPressed()
        {
            var player = new AudioStreamPlayer();
            player.Stream = _clickSound;
            player.Bus = "SFX";
            AddChild(player);
            player.Finished += () => player.QueueFree();
            player.Play();
        } // end OnButtonPressed()
    } // end calss
} // end namespace 
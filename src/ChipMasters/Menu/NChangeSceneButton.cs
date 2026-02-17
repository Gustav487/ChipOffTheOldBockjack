using ChipMasters.GodotWrappers;
using Godot;
using System.Threading.Tasks;

namespace ChipMasters.Menu
{
    /// <summary>
    /// Godot <see cref="Button"/> that changes the active scene when pressed.
    /// </summary>
    public partial class NChangeSceneButton : NButton
    {
        [Export] private string? _scenePath;

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            Pressed += OnPressedDelayed;
        } // end _Ready()

        private async void OnPressedDelayed()
        {
            // Wait just enough time for the sound to play (0.1s = 100ms)
            await Task.Delay(100);
            if (!string.IsNullOrEmpty(_scenePath))
            {
                SGodotUtil.SceneChangeToFile(_scenePath, GetTree()).Invoke();
            }
        }
    } // end class
} // end namespace

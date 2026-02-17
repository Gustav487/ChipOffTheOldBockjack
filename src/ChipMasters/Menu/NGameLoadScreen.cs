using ChipMasters.GodotWrappers;
using ChipMasters.IO;
using Godot;
using System;

namespace ChipMasters.Menu
{
    /// <summary>
    /// Godot <see cref="Node"/> based game load screen logic.
    /// </summary>
    public partial class NGameLoadScreen : NNode
    {
        [Export] private PackedScene? _nextScene;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _nextScene.AssertNotNull();
        } // end _Ready()

        /// <inheritdoc/>
        public override void _Process(double delta)
        {
            base._Process(delta);
            if (NApplicationDataManager.INSTANCE.IsLoaded)
            {
                Error e = GetTree().ChangeSceneToPacked(_nextScene);
                if (e != Error.Ok)
                    throw new InvalidOperationException($"Load screen scene change failed with error: {e}");
                SetProcess(false);
            }
        } // end _Process()

    } // end class
} // end namespace

using ChipMasters.GodotWrappers;
using Godot;
using System;
using System.Collections.Generic;

namespace ChipMasters.Audio
{
    /// <summary>
    /// <see cref="NNode"/> that configures all buttons in the project that play a sound when pressed.
    /// </summary>
    public partial class NButtonAudioHandler : NNode
    {
        private readonly AudioStreamPlayer _clickPlayer = new AudioStreamPlayer();
        private readonly HashSet<BaseButton> _hookedButtons = new();



        /// <inheritdoc/>
        public override void _Ready()
        {
            _clickPlayer.Stream = GD.Load<AudioStream>("res://assets/audio/button_click.wav");
            _clickPlayer.Bus = "SFX";
            AddChild(_clickPlayer);

            // Listen to future nodes being added to the tree
            GetTree().NodeAdded += OnNodeAdded;

            // Scan current tree for existing buttons
            foreach (var node in GetTree().GetNodesInGroup("i_buttons"))
                OnNodeAdded(node);
        } // end _Ready()

        private void OnNodeAdded(Node node)
        {
            var excludedNames = new[] { "SubmitButton", "HitButton", "StandButton" };

            BaseButton? button = FindButtonRecursive(node);
            if (button is null)
                return;
            if (Array.Exists(excludedNames, name => button.Name == name))
                return;


            if (_hookedButtons.Contains(button))
                return;

            button.Pressed += OnAnyButtonPressed;
            _hookedButtons.Add(button);
        } // end OnNodeAdded()

        private void OnAnyButtonPressed()
        {

            if (_clickPlayer.IsPlaying())
                _clickPlayer.Stop(); // Stop before restarting

            _clickPlayer.Play();
        } // end OnAnyButtonPressed()

        // Utility to search deeply for a Button in any hierarchy
        private BaseButton? FindButtonRecursive(Node node)
        {
            if (node is BaseButton b)
                return b;

            foreach (Node child in node.GetChildren())
            {
                BaseButton? result = FindButtonRecursive(child);
                if (result != null)
                    return result;
            }

            return null;
        } // end FindButtonRecursive()
    } // end class
} // end namespace 
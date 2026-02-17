using System;

namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.AnimatedSprite2D"/>, see also ChipMasters.GodotWrappers.<see cref="NAnimatedSprite2D"/>.
    /// </summary>
    public interface IAnimatedSprite2D : INode2D
    {
        /// <summary>
        /// Refer to <see cref="Godot.AnimatedSprite2D.AnimationFinished"/>.
        /// </summary>
        event Action AnimationFinished;

        /// <summary>
        /// Refer to <see cref="Godot.AnimatedSprite2D.IsPlaying"/>.
        /// </summary>
        bool IsPlaying();

        /// <summary>
        /// Refer to <see cref="Godot.AnimatedSprite2D.Play"/>. Type slightly adjust for C#.
        /// </summary>
        void Play(string? name = null!, float customSpeed = 1, bool fromEnd = false);
    } // end interface
} // end namespace
using System;

namespace ChipMasters.Menu.Displays.Assets.Animations
{
    /// <summary>
    /// Contract for an object that play animations.
    /// </summary>
    public interface IAnimator
    {
        /// <summary>
        /// Is the animation currently playing.
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Event raised when an animation starts playing.
        /// </summary>
        event Action? OnPlaying;

        /// <summary>
        /// Event raised when an animation stops playing.
        /// </summary>
        event Action? OnStopped;

    } // end interface
} // end namespace
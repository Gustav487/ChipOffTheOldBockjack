using System;

namespace ChipMasters.Menu.Displays.Animations
{
    /// <summary>
    /// Contract for an object representing an instance of animation.
    /// </summary>
    public interface IAnimation
    {
        /// <summary>
        /// Concluded animation singelton.
        /// </summary>
        public static readonly IAnimation EMPTY = new REmptyAnimation();



        /// <summary>
        /// Is the animation done playing.
        /// </summary>
        bool IsFinished { get; }
        /// <summary>
        /// Fired when the animation ends.
        /// </summary>
        event Action? OnFinished;



        private class REmptyAnimation : IAnimation
        {
            public bool IsFinished => true;
#pragma warning disable CS0067
            public event Action? OnFinished;
#pragma warning restore CS0067
        } // end inner class REmptyAnimation
    } // end interface
} // end namespace
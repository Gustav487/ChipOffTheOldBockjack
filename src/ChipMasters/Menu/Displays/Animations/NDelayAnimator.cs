using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Displays.Animations
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IAnimator{T}"/> that waits for a set period before concluding.
    /// </summary>
    public sealed partial class NDelayAnimator : NNode, IAnimator<object>
    {
        [Export] private int _milliDelay;



        /// <inheritdoc/>
        public IAnimation Animate(object instance) => new RDelayAnimation(_milliDelay);
    } // end class
} // end namespace
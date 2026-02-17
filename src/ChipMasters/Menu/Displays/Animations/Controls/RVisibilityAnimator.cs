using ChipMasters.GodotWrappers;

namespace ChipMasters.Menu.Displays.Animations.Controls
{
    /// <summary>
    /// <see cref="IAnimator{T}"/> for changing a <see cref="IControl"/>'s visibilty
    /// </summary>
    public sealed class RVisibilityAnimator : IAnimator<IControl>
    {
        private readonly bool _setTo;



        /// <inheritdoc/>
        public RVisibilityAnimator(bool setTo)
        {
            _setTo = setTo;
        } // end ctor



        /// <inheritdoc/>
        public IAnimation Animate(IControl instance)
        {
            instance.Visible = _setTo;
            return IAnimation.EMPTY;
        } // end Animate()
    } // end class
} // end namespace
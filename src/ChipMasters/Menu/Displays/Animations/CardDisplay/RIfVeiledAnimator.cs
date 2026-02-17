using ChipMasters.Cards;

namespace ChipMasters.Menu.Displays.Animations.CardDisplay
{
    /// <summary>
    /// <see cref="IAnimator{T}"/> for triggering another <see cref="IAnimator{T}"/> if a <see cref="ICard"/> <see cref="IDisplay{T}"/>'s <see cref="ICard"/> is <see cref="ICard.Veiled"/>.
    /// </summary>
    public sealed class RIfVeiledAnimator : IAnimator<IDisplay<ICard>>
    {
        private readonly IAnimator<IDisplay<ICard>> _animator;
        private readonly bool _negate;



        /// <inheritdoc/>
        public RIfVeiledAnimator(IAnimator<IDisplay<ICard>> animator, bool negate)
        {
            _animator = animator.AssertNotNull();
            _negate = negate;
        } // end ctor



        /// <inheritdoc/>
        public IAnimation Animate(IDisplay<ICard> instance)
        {

            if (_negate ^ instance.Display!.Veiled)
                return _animator.Animate(instance);
            else
                return IAnimation.EMPTY;
        } // end Animate()
    } // end class
} // end namespace
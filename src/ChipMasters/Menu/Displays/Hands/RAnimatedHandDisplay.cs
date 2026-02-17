using ChipMasters.Cards;
using ChipMasters.Games.Hands;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Animations;
using ChipMasters.Menu.Displays.Arrangers;
using ChipMasters.Menu.Displays.Cards;
using ChipMasters.Menu.Displays.Hand;
using ChipMasters.Util;
using System;

namespace ChipMasters.Menu.SubDisplays
{
    /// <summary>
    /// <see cref="IHandDisplay"/> implementation for laying cards out in arectangular area face down, and then revealing them all at once.
    /// </summary>
    public class RAnimatedHandDisplay : RHandDisplay
    {
        private readonly IAnimator<IControlCardDisplay> _revealAnimator;




        /// <inheritdoc/>
        public RAnimatedHandDisplay(IPool<IControlCardDisplay> cardDisplayPool,
            IArranger<IControl> arranger,
            Action<INode> addChild, Action<INode> removeChild,
            IAnimator<IControlCardDisplay> revealAnimator)
            : base(cardDisplayPool, arranger, addChild, removeChild)
        {
            _revealAnimator = revealAnimator.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        protected override void HandAdded(IHand hand)
        {
            base.HandAdded(hand);
            foreach (IControlCardDisplay cd in _activeDisplay)
                _revealAnimator.Animate(cd);

            hand.OnCardAdded += RevealLast;
        } // end HandAdded()

        /// <inheritdoc/>
        protected override void HandRemoved(IHand hand)
        {
            base.HandRemoved(hand);
            hand.OnCardAdded -= RevealLast;
        } // end HandRemove()



        private void RevealLast(ICard _) => _revealAnimator.Animate(_activeDisplay[^1]);

    } // end class
} // end namespace

using ChipMasters.Games.Hands;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Animations;
using ChipMasters.Menu.Displays.Arrangers;
using ChipMasters.Menu.Displays.Cards;
using ChipMasters.Menu.Displays.Hand;
using ChipMasters.Util;
using Godot;

namespace ChipMasters.Menu.SubDisplays
{
    /// <summary>
    /// Godot <see cref="Node"/> <see cref="IHandDisplay"/>.
    /// </summary>
    public partial class NAnimatedHandDisplay : NNode, IHandDisplay
    {
        [Export] private Node _cardDisplayPool = null!;
        [Export] private Node _arranger = null!;
        [Export] private Node _revealAnimator = null!;



        /// <inheritdoc/>
        public IHand? Hand { get => Inner.Hand; set => Inner.Hand = value; }



        private IHandDisplay Inner => _inner ?? throw new RNotReadyException();
        private IHandDisplay _inner = null!;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inner = new RAnimatedHandDisplay(
                (IPool<IControlCardDisplay>)_cardDisplayPool.AssertNotNull(),
                (IArranger<IControl>)_arranger.AssertNotNull(),
                (x) => AddChild((Node)x), (x) => RemoveChild((Node)x),

                (IAnimator<IControlCardDisplay>)_revealAnimator.AssertNotNull());
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_inner is not null)
                Inner.Dispose();
        } // end Dispose()
    } // end class
} // end namespace

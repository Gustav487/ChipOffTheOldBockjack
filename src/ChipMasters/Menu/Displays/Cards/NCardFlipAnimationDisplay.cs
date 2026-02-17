using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Menu.Displays.Cards
{
    /// <summary>
    /// <see cref="ICard"/> <see cref="ANDisplay{T}"/> that wraps a <see cref="RCardFlipAnimationDisplay"/>.
    /// </summary>
    public partial class NCardFlipAnimationDisplay : ANDisplay<ICard>
    {
        [Export] private Node _animationPlayer = null!;

        /// <inheritdoc/>
        protected override IDisplay<ICard> ConstructInner()
            => new RCardFlipAnimationDisplay(RUser.INSTANCE.AssetSelection, (IAnimationPlayer)_animationPlayer.AssertNotNull());
    } // end class
} // end namespace

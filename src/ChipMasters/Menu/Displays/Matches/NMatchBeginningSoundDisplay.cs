using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Displays.Matches
{
    /// <summary>
    /// <see cref="IMatch"/> <see cref="ANDisplay{T}"/> that wraps a <see cref="RMatchBeginningSoundDisplay"/>.
    /// </summary>
    public partial class NMatchBeginningSoundDisplay : ANDisplay<IMatch>
    {
        [Export] private Node _cardShuffle = null!;
        [Export] private Node _cardDeal = null!;

        /// <inheritdoc/>
        protected override IDisplay<IMatch> ConstructInner()
            => new RMatchBeginningSoundDisplay((IAudioStreamPlayer)_cardShuffle.AssertNotNull(), (IAudioStreamPlayer)_cardDeal.AssertNotNull());
    } // end class
} // end namespace

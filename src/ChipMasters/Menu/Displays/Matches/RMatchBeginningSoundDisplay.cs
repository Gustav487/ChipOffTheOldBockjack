using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;

namespace ChipMasters.Menu.Displays.Matches
{
    /// <summary>
    /// Implements logic for playing the card sound effects.
    /// </summary>
    public sealed class RMatchBeginningSoundDisplay : IDisplay<IMatch>
    {
        /// <inheritdoc/>
        public IMatch? Display
        {
            get => _displaying;
            set
            {
                if (_displaying == value)
                    return;

                _displaying = value;
                if (_displaying is not null && !_displaying.IsConcluded)
                {
                    _cardShuffle.Play();
                    _cardDeal.Play();
                }
            }
        } // end Display
        private IMatch? _displaying;

        private readonly IAudioStreamPlayer _cardShuffle;
        private readonly IAudioStreamPlayer _cardDeal;



        /// <inheritdoc/>
        public RMatchBeginningSoundDisplay(IAudioStreamPlayer cardsShuffle, IAudioStreamPlayer cardsDeal)
        {
            _cardShuffle = cardsShuffle.AssertNotNull();
            _cardDeal = cardsDeal.AssertNotNull();
        } // end ctor

        /// <inheritdoc/>
        public void Dispose() { } // end Dispose()
    } // end class
} // end namespace
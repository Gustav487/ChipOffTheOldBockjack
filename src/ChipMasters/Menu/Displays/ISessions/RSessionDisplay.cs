using ChipMasters.Games.Matches;
using ChipMasters.Games.Sessions;

namespace ChipMasters.Menu.Displays.ISessions
{
    /// <summary>
    /// Simple <see cref="IDisplay{T}"/> <see cref="ISession"/> implementation.
    /// </summary>
    public sealed class RSessionDisplay : IDisplay<ISession>
    {
        /// <inheritdoc/>
        public ISession? Display
        {
            get => _displaying;
            set
            {
                if (_displaying == value)
                    return;

                Detach();
                _displaying = value;
                Attach();
                RefreshDisplay();
            }
        } // end Session
        private ISession? _displaying;

        private readonly IDisplay<IMatch> _matchDisplay;



        /// <inheritdoc/>
        public RSessionDisplay(IDisplay<IMatch> matchDisplay)
        {
            _matchDisplay = matchDisplay.AssertNotNull();
        } // end ctor

        /// <inheritdoc/>
        public void Dispose() => Detach();



        private void RefreshDisplay()
        {
            _matchDisplay.Display = _displaying?.Match;
        } // end RefreshDisplay()

        private void Attach()
        {
            if (_displaying is null)
                return;

            _displaying.OnMatchChanged += RefreshDisplay;
        } // end Attach()

        private void Detach()
        {
            if (_displaying is null)
                return;

            _displaying.OnMatchChanged -= RefreshDisplay;
        } // end Detach()
    } // end class
} // end namespace
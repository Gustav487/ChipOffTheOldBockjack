using ChipMasters.User;
using ChipMasters.Util;
using GSR.Utilic;
using System;
using System.Collections.Generic;

namespace ChipMasters.Menu.Displays.ChipRecords
{
    /// <summary>
    /// UI component that displays the player's chip count history over the last 10 matches.
    /// </summary>
    public sealed class RChipHistoryDisplay : IDisplay<IReadOnlyList<VChipRecord>>
    {
        /// <inheritdoc/>
        public IReadOnlyList<VChipRecord>? Display
        {
            get => _displaying;
            set
            {
                if (_displaying == value)
                    return;

                _displaying = value;
                RefreshDisplay();
            }
        }
        private IReadOnlyList<VChipRecord>? _displaying;

#warning common pool usage logic used in a hand display, candidate for abc
        private readonly List<IDisplay<VChipRecord?>> _activeDisplays = new();
        private readonly IPool<IDisplay<VChipRecord?>> _displayPool;
        private readonly Action<IDisplay<VChipRecord?>> _addDisplay;
        private readonly Action<IDisplay<VChipRecord?>> _removeDisplay;



        /// <inheritdoc/>
        public RChipHistoryDisplay(
            IPool<IDisplay<VChipRecord?>> displayPool,
            Action<IDisplay<VChipRecord?>> addDisplay,
            Action<IDisplay<VChipRecord?>> removeDisplay)
        {
            _displayPool = displayPool.AssertNotNull();
            _addDisplay = addDisplay.AssertNotNull();
            _removeDisplay = removeDisplay.AssertNotNull();
        } // end ctor

        /// <inheritdoc/>
        public void Dispose() { } // end Dispose()


        private void RefreshDisplay()
        {
            ClearDisplay();
            if (_displaying is not null)
                BuildDisplay();
        } // end RefreshDisplay()

        private void ClearDisplay()
        {
            foreach (IDisplay<VChipRecord?> ccrd in _activeDisplays)
            {
                _removeDisplay(ccrd);
                _displayPool.Release(ccrd);
            }
            _activeDisplays.Clear();
        } // end ClearDisplay()

        private void BuildDisplay()
        {
            if (_displaying is null)
                throw new UnexpectedStateException();

            foreach (VChipRecord ccr in _displaying)
            {
                IDisplay<VChipRecord?> ccrd = _displayPool.Get();
                ccrd.Display = ccr;

                _activeDisplays.Add(ccrd);
                _addDisplay(ccrd);
            }
        } // end BuildDisplay()

    } // end class
} // end namespace
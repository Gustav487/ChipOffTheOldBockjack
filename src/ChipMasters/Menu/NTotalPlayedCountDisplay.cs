using ChipMasters.GodotWrappers;
using ChipMasters.User;

namespace ChipMasters.Menu
{
    /// <summary>
    /// Displays the total number of games played (sum of wins, ties, and losses).
    /// </summary>
    public partial class NTotalPlayedCountDisplay : NLabel
    {
        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            UpdateDisplay();
        }

        /// <summary>
        /// Updates the label text with the total games played.
        /// </summary>
        private void UpdateDisplay()
        {
            var metrics = RUser.INSTANCE.Metrics;
            Text = $"Total Games Played: {metrics.WinRatio.Wins + metrics.WinRatio.Ties + metrics.WinRatio.Losses}";
        }
    } // end class
} // end namespace

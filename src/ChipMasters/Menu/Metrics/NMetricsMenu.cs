using ChipMasters.GodotWrappers;
using ChipMasters.Items;
using ChipMasters.Menu.Displays;
using ChipMasters.User;
using Godot;
using System.Collections.Generic;

namespace ChipMasters.Menu.Metrics
{
    /// <summary>
    /// <see cref="NNode"/> for displaying the <see cref="RUser.INSTANCE"/>s metrics to several displays.
    /// </summary>
    public sealed partial class NMetricsMenu : NNode
    {
        [Export] private Node _winRatioDisplay = null!;
        [Export] private Node _chipHistoryDisplay = null!;
        [Export] private Node _matchHistoryDisplay = null!;
        [Export] private Node _inventoryFractionDisplay = null!;
        [Export] private Node _achievementsAchievedFractionDisplay = null!;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            ((IDisplay<VWinRatio?>)_winRatioDisplay.AssertNotNull()).Display = RUser.INSTANCE.Metrics.WinRatio;
            ((IDisplay<IReadOnlyList<VChipRecord>>)_chipHistoryDisplay.AssertNotNull()).Display = RUser.INSTANCE.Metrics.ChipHistory;
            ((IDisplay<IReadOnlyList<VMatchRecord>>)_matchHistoryDisplay.AssertNotNull()).Display = RUser.INSTANCE.Metrics.MatchHistory;
            ((IDisplay<IInventory>)_inventoryFractionDisplay.AssertNotNull()).Display = RUser.INSTANCE.Inventory;
            ((IDisplay<IAchievementTracker>)_achievementsAchievedFractionDisplay.AssertNotNull()).Display = RUser.INSTANCE.AchievementTracker;
        } // end _Ready()

    } // end class
} // end namespace

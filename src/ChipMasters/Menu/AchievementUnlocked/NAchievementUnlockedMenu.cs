using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Menu.AchievementUnlocked
{
    /// <summary>
    /// <see cref="NNode"/> wrapper of <see cref="RAchievementUnlockedMenu"/>.
    /// </summary>
    public sealed partial class NAchievementUnlockedMenu : NNode
    {
        [Export] private Node _achievementDisplay = null!;
        [Export] private int _displayLinger = 1000;

        private RAchievementUnlockedMenu? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _achievementDisplay.Reparent(this);
            RemoveChild(_achievementDisplay);

            _inner = new RAchievementUnlockedMenu(
                RUser.INSTANCE.AchievementTracker, (IDisplay<IAchievement>)_achievementDisplay.AssertNotNull(), _displayLinger,
                () => AddChild(_achievementDisplay), () => RemoveChild(_achievementDisplay));
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _inner?.Dispose();
        } // end Dispose()

    } // end class
} // end namespace

using ChipMasters.GodotWrappers;
using ChipMasters.Registers;
using ChipMasters.User;
using Godot;
using System.Linq;

namespace ChipMasters.Menu
{
    /// <summary>
    /// <see cref="NNode"/> for displaying a list of achievements and the <see cref="RUser.INSTANCE"/>s progress.
    /// </summary>
#warning N-R split needed
    public partial class NAchievementMenu : NNode
    {
        [Export] private VBoxContainer? achievementList;
        [Export] private ScrollContainer? scrollContainer;



        /// <inheritdoc/>
        public override void _Ready()
        {
            DisplayAchievements();
        } // end _Ready()

        private void DisplayAchievements()
        {
            foreach (IAchievement achievement in SAchievementTypes.REGISTER.Values
                .OrderBy((x) => x.WatchedStat)
                .ThenBy((x) => x.Threshold))
            {
                bool isUnlocked = RUser.INSTANCE.AchievementTracker.Achievements.Contains(achievement);
                AddAchievementToUI(achievement, isUnlocked, achievementList);
            }
        } // end DisplayAchievements()

        private void AddAchievementToUI(IAchievement achievement, bool isUnlocked, VBoxContainer? list)
        {
            if (list == null) return;

            HBoxContainer achievementContainer = new HBoxContainer();

            LabelSettings labelSettings = new LabelSettings
            {
                FontSize = 24,
                FontColor = isUnlocked ? new Color(1.0f, 1.0f, 1.0f) : new Color(0.0f, 0.0f, 0.0f)
            };

            Label nameLabel = new Label
            {
                Text = achievement.DisplayName,
                LabelSettings = labelSettings
            };

            Label statusLabel = new Label
            {
                Text = isUnlocked ? "Unlocked" : "Locked",
                LabelSettings = labelSettings
            };

            achievementContainer.AddChild(nameLabel);
            achievementContainer.AddChild(statusLabel);
            list.AddChild(achievementContainer);
        } // end AddAchievementToUI()
    } // end class
} // end namespace

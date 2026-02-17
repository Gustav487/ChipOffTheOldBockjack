using ChipMasters.Menu.Displays;
using ChipMasters.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChipMasters.Menu.AchievementUnlocked
{
    /// <summary>
    /// Unintrusive popup that displays <see cref="IAchievement"/> that've been unlocked by an <see cref="IAchievementTracker"/>.
    /// </summary>
    public sealed class RAchievementUnlockedMenu : IDisposable
    {
        private readonly Queue<IAchievement> _displayQueue = new();

        private readonly IAchievementTracker _achievementTracker;
        private readonly IDisplay<IAchievement> _achievementDisplay;
        private readonly int _displayLinger;
        private readonly Action _show;
        private readonly Action _hide;

        private bool _disposed = false;
        private bool _displaying = false;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="achievementTracker"></param>
        /// <param name="achievementDisplay"></param>
        /// <param name="displayLinger">Milliseconds each achievement will be displayed for.</param>
        /// <param name="show"></param>
        /// <param name="hide"></param>
        public RAchievementUnlockedMenu(
            IAchievementTracker achievementTracker, IDisplay<IAchievement> achievementDisplay, int displayLinger,
            Action show, Action hide)
        {
            _achievementTracker = achievementTracker.AssertNotNull();
            _achievementDisplay = achievementDisplay.AssertNotNull();
            _displayLinger = displayLinger;
            _show = show.AssertNotNull();
            _hide = hide.AssertNotNull();

            _achievementTracker.OnUnlocked += Queue;
        } // end ctor

        /// <inheritdoc/>
        public void Dispose()
        {
            _disposed = true;
            _achievementTracker.OnUnlocked -= Queue;
        } // end Dispose()



        private void Queue(IAchievement a)
        {
            _displayQueue.Enqueue(a);
            if (!_displaying)
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Display();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        } // end Queue()

        private async Task Display()
        {
            _displaying = true;
            _show();
            while (_displayQueue.Count > 0)
            {
                if (_disposed)
                    return;
                _achievementDisplay.Display = _displayQueue.Dequeue();
                await Task.Delay(_displayLinger);
            }
            _hide();
            _displaying = false;
        } // end Display()

    } // end class
} // end namespace
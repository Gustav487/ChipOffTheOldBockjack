using System;
using System.Threading.Tasks;

namespace ChipMasters.Menu.Displays.Animations
{
    /// <summary>
    /// <see cref="IAnimation"/> that concludes after a fixed time.
    /// </summary>
    public sealed class RDelayAnimation : IAnimation
    {
        /// <inheritdoc/>
        public bool IsFinished { get; private set; }

        /// <inheritdoc/>
        public event Action? OnFinished;



        /// <inheritdoc/>
        public RDelayAnimation(int milliDelay)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Finish(milliDelay);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        } // end ctor



        /// <inheritdoc/>
        public async Task Finish(int milliDelay)
        {
            await Task.Delay(milliDelay);
            IsFinished = true;
            OnFinished?.Invoke();
        } // end Finished()

    } // end class
} // end namespace
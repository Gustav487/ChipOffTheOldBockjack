using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using Godot;
using System;

namespace ChipMasters.Menu.Displays.Cards
{
    /// <summary>
    /// <see cref="ICard"/> <see cref="IDisplay{T}"/> for playing a sound on flip.
    /// </summary>
    public sealed class RCardFlipAudioDisplay : IDisplay<ICard>
    {
        /// <inheritdoc/>
        public ICard? Display
        {
            get => _displaying;
            set
            {
                // Unsubscribe from old card
                if (_displaying is not null && _isInsideTree()) // if outside tree it shouldn't be attached
                    _displaying.OnFlipped -= HandleCardFlipped;

                _displaying = value;

                // Subscribe to new card
                if (_displaying is not null && _isInsideTree()) // only attach if inside tree
                    _displaying.OnFlipped += HandleCardFlipped;
            }
        }
        private ICard? _displaying;

        private readonly IAudioStreamPlayer _flipSound;
        private readonly Func<bool> _isInsideTree;



        /// <inheritdoc/>
        public RCardFlipAudioDisplay(IAudioStreamPlayer flipSound, Func<bool> isInsideTree)
        {
            _flipSound = flipSound.AssertNotNull();
            _isInsideTree = isInsideTree.AssertNotNull();
        } // end ctor

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_displaying is not null)
                _displaying.OnFlipped -= HandleCardFlipped;
        } // end Dispose()



        private void HandleCardFlipped() => _flipSound?.Play();



        /// <summary>
        /// Parallel of <see cref="Node._ExitTree"/>.
        /// </summary>
        public void ExitTree()
        {
            if (_displaying is not null)
                _displaying.OnFlipped -= HandleCardFlipped;
        } // end ExitTree()

        /// <summary>
        /// Parallel of <see cref="Node._EnterTree"/>.
        /// </summary>
        public void EnterTree()
        {
            if (_displaying is not null)
                _displaying.OnFlipped += HandleCardFlipped;
        } // end EnterTree()

    } // end class
} // end namespace 
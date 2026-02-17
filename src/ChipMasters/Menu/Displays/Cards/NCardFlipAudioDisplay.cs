using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Displays.Cards
{
    /// <summary>
    /// <see cref="ICard"/> <see cref="ANDisplay{T}"/> that wraps a <see cref="RCardFlipAudioDisplay"/>.
    /// </summary>
    public sealed partial class NCardFlipAudioDisplay : ANDisplay<ICard>
    {
        [Export] private Node _flipSound = null!;

        /// <inheritdoc/>
        protected override IDisplay<ICard> ConstructInner()
        {
            RCardFlipAudioDisplay cd = new(
                (IAudioStreamPlayer)_flipSound.AssertNotNull(),
                IsInsideTree);
            TreeEntered += cd.EnterTree;
            TreeExited += cd.ExitTree;
            return cd;
        } // end ConstructInner()
    } // end class
} // end namespace 
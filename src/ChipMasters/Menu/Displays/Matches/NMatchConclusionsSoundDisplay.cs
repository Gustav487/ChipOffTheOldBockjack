using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Displays.Matches
{
    /// <summary>
    /// <see cref="IMatch"/> <see cref="ANDisplay{T}"/> for playing sounds based on conclusion state.
    /// </summary>
    public partial class NMatchConclusionsSoundDisplay : ANDisplay<IMatch>
    {
        // assigned in Export
        [Export] private NAudioStreamPlayer _win = null!;
        [Export] private NAudioStreamPlayer _tie = null!;
        [Export] private NAudioStreamPlayer _loss = null!;



        /// <inheritdoc/>
        protected override IDisplay<IMatch> ConstructInner()
            => new RMatchConclusionsSoundDisplay(_win, _tie, _loss);
    } // end class
} // end namespace

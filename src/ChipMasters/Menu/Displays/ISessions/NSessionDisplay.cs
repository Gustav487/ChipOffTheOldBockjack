using ChipMasters.Games.Matches;
using ChipMasters.Games.Sessions;
using Godot;

namespace ChipMasters.Menu.Displays.ISessions
{
    /// <summary>
    /// <see cref="ANDisplay{T}"/> for <see cref="ISession"/>s that wraps a <see cref="RSessionDisplay"/>.
    /// </summary>
    public partial class NSessionDisplay : ANDisplay<ISession>
    {
        [Export] private Node _matchDisplay = null!;

        /// <inheritdoc/>
        protected override IDisplay<ISession> ConstructInner() => new RSessionDisplay((IDisplay<IMatch>)_matchDisplay.AssertNotNull());
    } // end class
} // end namespace
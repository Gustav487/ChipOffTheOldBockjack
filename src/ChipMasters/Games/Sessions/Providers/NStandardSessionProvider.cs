using ChipMasters.Games.BetHandlers;
using ChipMasters.Games.Matches.Providers;
using ChipMasters.GodotWrappers;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Games.Sessions.Providers
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="ISessionProvider"/> for <see cref="RStandardSession"/>s.
    /// </summary>
    public sealed partial class NStandardSessionProvider : NNode, ISessionProvider
    {
        [Export] private Node _matchProvider = null!;
        [Export] private Node _betHandler = null!;



        /// <inheritdoc/>
        public ISession Create(int bet)
            => new RStandardSession(
                bet,
                (IMatchProvider)_matchProvider.AssertNotNull(),
                (IBetHandler)_betHandler.AssertNotNull(),
                RUser.INSTANCE.Metrics);
    } // end class
} // end namespace

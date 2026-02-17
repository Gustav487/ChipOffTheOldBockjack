using ChipMasters.Games.BetHandlers;
using ChipMasters.Games.Sessions.Providers;
using ChipMasters.Menu.BetSelections;
using ChipMasters.User;
using System;

namespace ChipMasters.Menu.Interactables.Buttons.Sessions
{
    /// <summary>
    /// Button that generates a new match for a player, prompting them first to select a bet.
    /// </summary>
    public sealed class RCreateSessionButton
    {
        private readonly IUser _player;
        private readonly ISessionProvider _sessionProvider;
        private readonly VBetRange _betRange;
        private readonly IBetSelectionMenu _betSelectionMenu;

        /// <summary>
        /// Event raised when the session was created.
        /// </summary>
        public event Action? OnSessionCreated;



        /// <inheritdoc/>
        public RCreateSessionButton(IUser player, ISessionProvider sessionProvider, VBetRange betRange, IBetSelectionMenu betSelectionMenu)
        {
            _player = player.AssertNotNull();
            _sessionProvider = sessionProvider.AssertNotNull();
            _betRange = betRange;
            _betSelectionMenu = betSelectionMenu.AssertNotNull();
        } // end ctor



        /// <summary>
        /// Press the button, beginning session creation.
        /// </summary>
        public void Press()
        {
            _betSelectionMenu.Range = _betRange;
            _betSelectionMenu.Open(CreateMatch);
        } // end Press()

        private void CreateMatch(int selection)
        {
            _player.Session = _sessionProvider.Create(selection);
            OnSessionCreated?.Invoke();
        } // end CreateMatch()

    } // end class
} // end namespace

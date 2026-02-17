using ChipMasters.Games.BetHandlers;
using ChipMasters.Games.Matches;
using ChipMasters.Games.Sessions.Providers;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.BetSelections;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Menu.Interactables.Buttons.Sessions
{
    /// <summary>
    /// Button to open up a separate scene with a new <see cref="IMatch"/>.
    /// </summary>
    public partial class NCreateSessionButton : NButton
    {
        [Export] private string? _gameScreenPath;
        [Export] private int _minBet;
        [Export] private int _maxBet;
        [Export] private Node? _betSelectionMenu;
        [Export] private Node? _sessionProvider;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            // building cmb
            RCreateSessionButton cmb = new(
                RUser.INSTANCE,
                (ISessionProvider)_sessionProvider.AssertNotNull(),
                new VBetRange(_minBet, _maxBet == -1 ? int.MaxValue : _maxBet),
                (IBetSelectionMenu)_betSelectionMenu.AssertNotNull());

            cmb.OnSessionCreated += SGodotUtil.SceneChangeToFile(_gameScreenPath, GetTree());

            if (RUser.INSTANCE.Session is not null)
                Pressed += () => Warning(cmb);
            else
                Pressed += cmb.Press;
        } // end _Ready()

        /// <summary>
        /// Warn about ongoing match and that it'll be cancelled.
        /// </summary>
        /// <param name="amb"></param>
        public void Warning(RCreateSessionButton amb)
        {
            var ForfeitPrompt = new ConfirmationDialog();

            ForfeitPrompt.Confirmed += amb.Press;
            ForfeitPrompt.DialogText = "Are you sure? This will remove match data and the chips you placed as a bet at the time?.";

            AddChild(ForfeitPrompt);
            ForfeitPrompt.PopupCentered();
            ForfeitPrompt.Show();
        } // end NCreateMatchButton()

    } // end class
} // end namespace

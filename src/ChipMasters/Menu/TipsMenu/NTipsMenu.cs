using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Menu.TipsMenu
{
    /// <summary>
    /// <see cref="NControl"/> based <see cref="ITipsMenu"/> that wraps <see cref="RTipsMenu"/>.
    /// </summary>
    public partial class NTipsMenu : NControl, ITipsMenu
    {
        [Export] private Node _tipsMenu = null!;
        [Export] private Node _tipsToggle = null!;
        [Export] private Node _tipsLabel = null!;
        [Export] private Node _closeButton = null!;



        private ITipsMenu Menu => _menu ?? throw new RNotReadyException(nameof(_menu));
        private ITipsMenu? _menu;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            _menu = new RTipsMenu(RUser.INSTANCE,
                (IControl)_tipsMenu.AssertNotNull(),
                (ICheckBox)_tipsToggle.AssertNotNull(),
                (ILabel)_tipsLabel.AssertNotNull(),
                (IBaseButton)_closeButton.AssertNotNull());

            // Disable tips if not in standard match type
            if (!RUser.INSTANCE.Session!.Match.GetType().Equals(typeof(RMatch)))
                Visible = false;
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Menu.Dispose();
        } // end Dispose()

        /// <inheritdoc/>
        public void ShowTip(bool isMatchConcluded, int playerTotal, int dealerTotal)
            => Menu.ShowTip(isMatchConcluded, playerTotal, dealerTotal);

        /// <inheritdoc/>
        public void Open() => Menu.Open();

        /// <inheritdoc/>
        public void Close() => Menu.Close();
    } // end class
} // end namespace

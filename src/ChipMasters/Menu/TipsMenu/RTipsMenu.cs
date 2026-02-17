using ChipMasters.Cards;
using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;
using ChipMasters.User;
using GSR.Utilic;

namespace ChipMasters.Menu.TipsMenu
{
    /// <summary>
    /// Simple <see cref="ITipsMenu"/> implementation.
    /// </summary>
    public sealed class RTipsMenu : ITipsMenu
    {
        private readonly IUser _user;
        private readonly IControl _tipsMenu;
        private readonly ICheckBox _tipsToggle;
        private readonly ILabel _tipsLabel;
        private readonly IBaseButton _closeButton;

        /// <inheritdoc/>
        public RTipsMenu(IUser user, IControl tipsMenu, ICheckBox tipsToggle, ILabel tipsLabel, IBaseButton closeButton)
        {
            _user = user.AssertNotNull();
            _tipsMenu = tipsMenu.AssertNotNull();
            _tipsToggle = tipsToggle.AssertNotNull();
            _tipsLabel = tipsLabel.AssertNotNull();
            _closeButton = closeButton.AssertNotNull();

            _tipsToggle.Pressed += Open;
            _closeButton.Pressed += Close;

            TryDetachTipEvents();
        } // end ctor

        /// <inheritdoc/>
        public void Dispose()
        {
            TryDetachTipEvents();
        } // end Dispose()




        /// <inheritdoc/>
        public void Open()
        {
            if (_tipsToggle.ButtonPressed)
            {
                TryDetachTipEvents();
                TryAttachTipEvents();

                _tipsMenu.Visible = true;
                ShowTip();
            }
            else
                Close();
        } // end Open()

        /// <inheritdoc/>
        public void Close()
        {
            TryDetachTipEvents();

            _tipsMenu.Visible = false;
            _tipsToggle.ButtonPressed = false;
        } // end Close()

        private void ShowTip(ICard _) => ShowTip();

        private void ShowTip()
        {
            if (_user.Session is null)
                throw new UnexpectedStateException();

            int playerTotal = _user.Session.Match.AppraisePlayerHand(false).TotalValue;
            int dealerTotal = _user.Session.Match.AppraiseDealerHand(false).TotalValue;
            ShowTip(_user.Session.Match.IsConcluded, playerTotal, dealerTotal);
        } // end ShowTip()

        /// <inheritdoc/>
        public void ShowTip(bool isMatchConcluded, int playerTotal, int dealerTotal)
        {
            _tipsLabel.Text = playerTotal switch
            {
                <= 11 => "Press the Hit button to draw another card.", // playerTotal less than or equal to 11
                <= 16 => dealerTotal <= 6
                    ? "This is a tough choice. The best move is to stand." // playerTotal between 12 and 16, dealerTotal less than or equal than 6
                    : "This is a tough choice. The best move is to hit.", // playerTotal between 12 and 16, dealerTotal greater than 7
                <= 20 => "Great hand! The best move is to stand.", // playerTotal between 17 and 20
                21 => isMatchConcluded
                    ? "Congratulations! You got a Blackjack!" // player got blackjack
                    : "You have the best possible hand! Press the Stand button.", // player got 21
                _ => "Oh no, looks like you have bust. Try not to go over 21." // player busted
            };
        } // end ShowTip()

        private void OnMatchChanged()
        {
            TryDetachTipEvents();
            TryAttachTipEvents();
            ShowTip();
        } // end OnMatchChanged()



        private void TryAttachTipEvents()
        {
            if (_user.Session is null)
                return;

            _user.Session.Match.PlayerHand.OnCardAdded += ShowTip;
            _user.Session.OnMatchChanged += OnMatchChanged;
        } // end TryAttachTipEvents()

        private void TryDetachTipEvents()
        {
            if (_user.Session is null)
                return;

            _user.Session.Match.PlayerHand.OnCardAdded -= ShowTip;
            _user.Session.OnMatchChanged -= OnMatchChanged;
        } // end TryDetachTipEvents()
    } // end class
} // end namespace
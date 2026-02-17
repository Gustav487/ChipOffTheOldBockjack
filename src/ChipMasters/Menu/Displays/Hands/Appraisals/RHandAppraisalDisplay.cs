using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;
using ChipMasters.GodotWrappers;

namespace ChipMasters.Menu.Displays.Hand.Appraisal
{
    /// <summary>
    /// <see cref="IHandAppraisalDisplay"/> which displays calculate and total hand value.
    /// </summary>
    public sealed class RHandAppraisalDisplay : AHandDisplay, IHandAppraisalDisplay
    {
        /// <inheritdoc/>
        public IAppraiser? Appraiser
        {
            get => _appraiser;
            set
            {
                if (_appraiser == value)
                    return;

                _appraiser = value;
                RefreshDisplay();
            }
        } // end Appraiser()
        private IAppraiser? _appraiser;

        private readonly ILabel _totalLabel;



        /// <inheritdoc/>
        public RHandAppraisalDisplay(ILabel totalLabel)
        {
            _totalLabel = totalLabel.AssertNotNull();
            RefreshDisplay();
        } // end ctor



        /// <inheritdoc/>
        protected override void RefreshDisplay(ICard _)
        {
            base.RefreshDisplay(_);
            _.OnFlipped += RefreshDisplay;
        } // end RefreshDisplay()

        /// <inheritdoc/>
        protected override void RefreshDisplay()
        {
            if (Hand is null || Appraiser is null)
            {
                _totalLabel.Text = "N/A";
                return;
            }

            VHandAppraisal appraisal = Appraiser.AppraiseHand(Hand, false);
            _totalLabel.Text = appraisal.TotalValue.ToString();
            if (appraisal.State == EHandState.Unknown)
                _totalLabel.Text += "?";
        } // end RefreshDisplay()

        /// <inheritdoc/>
        protected override void HandAdded(IHand hand)
        {
            base.HandAdded(hand);
            foreach (ICard card in hand)
                card.OnFlipped += RefreshDisplay;
        } // end HandAdded()

        /// <inheritdoc/>
        protected override void HandRemoved(IHand hand)
        {
            base.HandRemoved(hand);
            foreach (ICard card in hand)
                card.OnFlipped -= RefreshDisplay;
        } // end HandRemoved()
    } // end class
} // end namespace
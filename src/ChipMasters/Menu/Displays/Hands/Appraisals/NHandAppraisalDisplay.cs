using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Hand.Appraisal;
using Godot;

namespace ChipMasters.Menu.SubDisplays
{
    /// <summary>
    /// Godot <see cref="Node"/> based <see cref="IHandAppraisalDisplay"/> implementation.
    /// </summary>
    public partial class NHandAppraisalDisplay : NNode, IHandAppraisalDisplay
    {
        [Export] private Label? _totalLabel;

        /// <inheritdoc/>
        public IHand? Hand { get => HandAppraisalDisplay.Hand; set => HandAppraisalDisplay.Hand = value; }

        /// <inheritdoc/>
        public IAppraiser? Appraiser { get => HandAppraisalDisplay.Appraiser; set => HandAppraisalDisplay.Appraiser = value; }



        private IHandAppraisalDisplay HandAppraisalDisplay => _handAppraisalDisplay ?? throw new RNotReadyException();
        private IHandAppraisalDisplay? _handAppraisalDisplay;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _handAppraisalDisplay = new RHandAppraisalDisplay((ILabel)_totalLabel.AssertNotNull());
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            HandAppraisalDisplay.Dispose();
        } // end Dispose()

    } // end class
} // end namespace

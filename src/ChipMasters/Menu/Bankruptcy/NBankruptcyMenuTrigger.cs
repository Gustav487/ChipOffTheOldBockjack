using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Assets.Animations.MatchConclusion;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Menu.Bankruptcy
{
    /// <summary>
    /// <see cref="NNode"/> that wraps <see cref="RBankruptcyMenuTrigger"/>
    /// </summary>
    public partial class NBankruptcyMenuTrigger : NNode
    {
        [Export] private Node _animator = null!;
        [Export] private Node _bankruptcyMenu = null!;

        private RBankruptcyMenuTrigger? _trigger;

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _trigger = new RBankruptcyMenuTrigger(RUser.INSTANCE.Wallet, RUser.INSTANCE.Session!, (IMatchConclusionAnimator)_animator.AssertNotNull(), (IBankruptcyMenu)_bankruptcyMenu.AssertNotNull());
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _trigger?.Dispose();
        } // end Dispose()
    } // end class
} // end namespace

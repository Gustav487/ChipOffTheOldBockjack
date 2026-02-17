using ChipMasters.GodotWrappers;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Menu.Displays.Assets.Animations.MatchConclusion
{
    /// <summary>
    /// <see cref="NNode"/> <see cref="RSessionMatchConclusionAnimTrigger"/> like.
    /// </summary>
    public partial class NSessionMatchConclusionAnimTrigger : NNode
    {
        [Export] private Node _animator = null!;

        private RSessionMatchConclusionAnimTrigger? _trigger;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _trigger = new RSessionMatchConclusionAnimTrigger(RUser.INSTANCE.Session.AssertNotNull(), (IMatchConclusionAnimator)_animator.AssertNotNull());
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _trigger?.Dispose();
        } // end Dispose

    } // end class
} // end namespace

using ChipMasters.User;

namespace ChipMasters.Menu.Interactables.Buttons.Sessions
{
    /// <summary>
    /// <see cref="NChangeSceneButton"/> that only appears when there's a session to be resumed.
    /// </summary>
    public partial class NResumeSessionButton : NChangeSceneButton
    {

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            Visible = RUser.INSTANCE.Session is not null; // && !RUser.INSTANCE.Session.IsConcluded;
        } // end _Ready()
    } // end class
} // end namespace

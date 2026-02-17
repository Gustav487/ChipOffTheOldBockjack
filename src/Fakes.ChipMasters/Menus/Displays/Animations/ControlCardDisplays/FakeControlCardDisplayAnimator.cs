using ChipMasters.Menu.Displays.Animations;
using ChipMasters.Menu.Displays.Cards;

namespace Tests.ChipMasters.Menus.Displays.Animators.ControlCardDisplay
{
    public class FakeControlCardDisplayAnimator : IAnimator<IControlCardDisplay>
    {
        public IControlCardDisplay? _Animating_ { get; private set; }

        public bool IsPlaying => throw new NotImplementedException();

        public event Action? OnPlaying;
        public event Action? OnStopped;



        public IAnimation Animate(IControlCardDisplay controlCardDisplay)
        {
            _Animating_ = controlCardDisplay;
            return null!;
        } // end Animate
    } // end class
} // end namespace
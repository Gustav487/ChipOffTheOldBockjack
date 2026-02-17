using ChipMasters.Menu.Displays.Animations;

namespace Fakes.ChipMasters.Menus.Displays.Animations
{
    public sealed class FakeAnimation : IAnimation
    {
        public bool IsFinished { get; private set; }

        public event Action? OnFinished;



        public FakeAnimation(bool concluded = false)
        {
            IsFinished = concluded;
        } // end ctor



        public void Conclude()
        {
            IsFinished = true;
            OnFinished?.Invoke();
        } // end Conclude()

    } // end class
} // end namespace
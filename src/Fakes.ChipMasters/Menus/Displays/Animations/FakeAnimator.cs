using ChipMasters.Menu.Displays.Animations;

namespace Fakes.ChipMasters.Menus.Displays.Animations
{
    public sealed class FakeAnimator<T> : IAnimator<T>
    {

        public IReadOnlyList<FakeAnimation> _RetList_ => _retList;
        private readonly List<FakeAnimation> _retList = new();
        private readonly bool _instConclude;



        public FakeAnimator(bool instConclude = false)
        {
            _instConclude = instConclude;
        } // end ctor



        public IAnimation Animate(T instance)
        {
            FakeAnimation anim = new FakeAnimation(concluded: _instConclude);
            _retList.Add(anim);
            return anim;
        } // end Animation();
    } // end class
} // end namespace
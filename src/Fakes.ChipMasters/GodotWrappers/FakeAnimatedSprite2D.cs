using ChipMasters.GodotWrappers;
using Godot;

namespace Fakes.ChipMasters.GodotWrappers
{
    public class FakeAnimatedSprite2D : IAnimatedSprite2D
    {
        public bool _IsPlaying_ { get; set; }
        public string? _Playing_ { get; private set; } = null;



        public bool Visible { get; set; }
        public Material Material { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action? AnimationFinished;



        public bool IsPlaying() => _IsPlaying_;

        public void Play(string? name = null, float customSpeed = 1, bool fromEnd = false)
        {
            _Playing_ = name;
            _IsPlaying_ = true;
        } // end Play();



        public void _Finish_()
        {
            _IsPlaying_ = false;
            AnimationFinished?.Invoke();
        } // end _Finish_()

    } // end class
} // end namespace
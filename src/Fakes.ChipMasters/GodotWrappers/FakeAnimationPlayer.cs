using ChipMasters.GodotWrappers;

namespace Fakes.ChipMasters.GodotWrappers
{
    public sealed class FakeAnimationPlayer : IAnimationPlayer
    {
        /// <summary>
        /// Time when nothing is playing or set.
        /// </summary>
        public const double NULL_TIME = -1.0d;
        /// <summary>
        /// Time at start of animation.
        /// </summary>
        public const double START_TIME = 0.0d;
        /// <summary>
        /// Time at end of animation
        /// </summary>
        public const double END_TIME = 1.0d;



        public string? _Playing_ { get; private set; }
        /// <summary>
        /// Time of animation, 0.0 if start, 1.0 if end, -1.0 if NA
        /// </summary>
        public double _Time_ { get; private set; } = NULL_TIME;
        public bool _IsPlaying_ { get; private set; }



        public void Advance(double delta)
            => _Time_ += delta;

        public void Play(string? name = null, double customBlend = -1, float customSpeed = 1, bool fromEnd = false)
        {
            _Playing_ = name;
            _IsPlaying_ = true;
            _Time_ = fromEnd ? END_TIME : START_TIME;
        } // end Play()

        public void PlayBackwards(string? name = null, double customBlend = -1)
            => Play(name, customBlend, -1, true);

        public void Stop(bool keepState = false)
        {
            _IsPlaying_ = false;
            if (!keepState)
            {
                _Playing_ = null;
                _Time_ = NULL_TIME;
            }
        } // end Stop()
    } // end class
} // end namespace
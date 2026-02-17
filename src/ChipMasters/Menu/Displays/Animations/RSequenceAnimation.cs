using System;
using System.Collections.Generic;

namespace ChipMasters.Menu.Displays.Animations
{
    /// <summary>
    /// <see cref="IAnimation"/> resulting from aggregating <see cref="IAnimator{T}"/>s being played on an instance of <typeparamref name="T"/>.
    /// </summary>
    public sealed class RSequenceAnimation<T> : IAnimation
    {
        /// <inheritdoc/>
        public bool IsFinished { get; private set; }

        /// <inheritdoc/>
        public event Action? OnFinished;

        private readonly T _instance;
        private readonly IReadOnlyList<IAnimator<T>> _animators;



        /// <inheritdoc/>
        public RSequenceAnimation(T instance, IReadOnlyList<IAnimator<T>> animators)
        {
            _instance = instance;
            _animators = animators.AssertNotNull();



            IAnimation s = animators[0].Animate(instance);
            Chain(s, 1);
        } // end ctor

        private void Chain(IAnimation anim, int index)
        {
            if (_animators.Count - 1 < index)
            {
                if (anim.IsFinished)
                    Finish();
                else
                    anim.OnFinished += Finish;
                return;
            }

            if (anim.IsFinished)
                Chain(_animators[index].Animate(_instance), ++index);
            else
                anim.OnFinished += () => Chain(_animators[index].Animate(_instance), ++index);
        } // end Link()

        private void Finish()
        {
            IsFinished = true;
            OnFinished?.Invoke();
        }

    } // end class
} // end namespace
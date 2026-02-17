using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Menu.Displays.Animations
{
    /// <summary>
    /// <see cref="IAnimator{T}"/> that sequentially triggers a series of <see cref="IAnimator{T}"/>s.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RSequenceAnimator<T> : IAnimator<T>
    {
        private readonly IReadOnlyList<IAnimator<T>> _animators;



        /// <inheritdoc/>
        public RSequenceAnimator(params IAnimator<T>[] animators)
            : this((IEnumerable<IAnimator<T>>)animators) { } // end ctor

        /// <inheritdoc/>
        public RSequenceAnimator(IEnumerable<IAnimator<T>> animators)
        {
            _animators = animators.AssertNotNull().Select(x => x.AssertNotNull()).ToImmutableList();
        } // end ctor



        /// <inheritdoc/>
        public IAnimation Animate(T instance) => new RSequenceAnimation<T>(instance, _animators);
    } // end class
} // end namespace
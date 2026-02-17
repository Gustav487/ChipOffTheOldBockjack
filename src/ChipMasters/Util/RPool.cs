using System;
using System.Collections.Generic;

namespace ChipMasters.Util
{
    /// <summary>
    /// Simple <see cref="IPool{T}"/> implementation
    /// </summary>
    public class RPool<T> : IPool<T>
        where T : class
    {
        private readonly IList<T> _free = new List<T>();
        private readonly IList<T> _held = new List<T>();

        private readonly Func<T> _instantiator;



        /// <inheritdoc/>
        public RPool(Func<T> instantiator)
        {
            _instantiator = instantiator.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        public T Get()
        {
            T t;
            if (_free.Count == 0)
                t = _instantiator();
            else
            {
                t = _free[0];
                _free.RemoveAt(0);
            }

            _held.Add(t);
            return t;
        } // end Get()
#warning list comparison and removals should be based on references

        /// <inheritdoc/>
        public void Release(T instance)
        {
            if (!_held.Remove(instance))
                throw new InvalidOperationException();

            _free.Add(instance);
        } // end Release()

    } // end class
} // end namespace
using GSR.EnDecic;
using GSR.Utilic;
using System;

namespace ChipMasters.IO
{
    /// <summary>
    /// Simple <see cref="IType{T}"/> implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RType<T> : IType<T>
    {
        /// <inheritdoc/>
        public IEnDec<T> EnDec { get; }

        private readonly Func<T> _instantiate;
        private readonly Func<T, bool> _isDefault;
        private readonly Func<T, bool> _isTypeOf;



        /// <inheritdoc/>
        public RType(IEnDec<T> enDec, Func<T, bool> isTypeOf)
            : this(enDec, () => throw new UnexpectedStateException(), (x) => false, isTypeOf) { } // end ctor
        /// <inheritdoc/>
        public RType(IEnDec<T> enDec, Func<T> instantiate, Func<T, bool> isDefault, Func<T, bool> isTypeOf)
        {
            EnDec = enDec.AssertNotNull();
            _instantiate = instantiate.AssertNotNull();
            _isDefault = isDefault.AssertNotNull();
            _isTypeOf = isTypeOf.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        public T Instantiate() => _instantiate();

        /// <inheritdoc/>
        public bool IsDefault(T instance) => _isDefault(instance);

        /// <inheritdoc/>
        public bool IsTypeOf(T instance) => _isTypeOf(instance);
    } // end class
} // end namespace
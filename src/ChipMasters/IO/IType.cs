using GSR.EnDecic;

namespace ChipMasters.IO
{
    /// <summary>
    /// Contract for an object identifying <typeparamref name="T"/> typed objects for distinct serialization.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IType<T>
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <typeparamref name="T"/> instances.
        /// </summary>
        IEnDec<T> EnDec { get; }

        /// <summary>
        /// Create default instance of <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        T Instantiate();

        /// <summary>
        /// Is the <paramref name="instance"/> in the default state.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool IsDefault(T instance);

        /// <summary>
        /// Is this the type of the <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance"></param>
        public bool IsTypeOf(T instance);
    } // end class
} // end namespace
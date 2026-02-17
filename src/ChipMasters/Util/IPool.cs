namespace ChipMasters.Util
{
    /// <summary>
    /// Contract for an object which pools a resource <typeparamref name="T"/> for reuse.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPool<T>
        where T : class
    {
        /// <summary>
        /// Get a instance of <typeparamref name="T"/> from pool, instantiating a new one if is needed.
        /// </summary>
        /// <returns></returns>
        T Get();

        /// <summary>
        /// Return an <paramref name="instance"/> to thepool to be reuse.
        /// </summary>
        /// <param name="instance"></param>
        void Release(T instance);

    } // end interface
} // end namespace
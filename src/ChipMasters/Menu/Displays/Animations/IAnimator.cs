namespace ChipMasters.Menu.Displays.Animations
{
    /// <summary>
    /// Contract for an object that animates another object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAnimator<in T>
    {
        /// <summary>
        /// Animate the <paramref name="instance"/> of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        IAnimation Animate(T instance);
    } // end interface
} // end namespace
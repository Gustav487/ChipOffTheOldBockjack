using ChipMasters.User;

namespace ChipMasters.Items
{
    /// <summary>
    /// <see cref="IItem"/> that can be applied somehow to an <see cref="IUser"/>.
    /// </summary>
    public interface IApplicableItem : IItem
    {
        /// <summary>
        /// Apply the item to <paramref name="user"/>.
        /// </summary>
        /// <param name="user"></param>
        void Apply(IUser user);

        /// <summary>
        /// Is the item applied to the <paramref name="user"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsApplied(IUser user);
    } // end interface
} // end namespace
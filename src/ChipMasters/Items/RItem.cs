using System;

namespace ChipMasters.Items
{
    /// <summary>
    /// Represents a standard item with a name and a price.
    /// </summary>
    public class RItem : IItem
    {
        /// <inheritdoc/>
        public EItemCategory Category { get; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public int Price { get; }



        /// <summary>
        /// Initializes a new instance of the <see cref="RItem"/> class.
        /// </summary>
        /// <param name="category">The category of the item.</param>
        /// <param name="name">The name of the item.</param>
        /// <param name="price">The price of the item.</param>
        public RItem(EItemCategory category, string name, int price)
        {
            Category = category;
            Name = name;
            Price = price;
        } // end ctor



        /// <summary>
        /// Returns a string representation of the item.
        /// </summary>
        /// <returns>A string describing the item.</returns>
        public override string ToString() => $"{Name}: {Price} Chips";

        /// <inheritdoc/>
        public override int GetHashCode() => Tuple.Create(Category, Name, Price).GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is IItem other
            && other.Category == Category
            && other.Name == Name
            && other.Price == Price;

        /// <inheritdoc/>
        public static bool operator ==(RItem a, IItem b) => a.Equals(b);

        /// <inheritdoc/>
        public static bool operator !=(RItem a, IItem b) => !(a == b);

    } // end class
} // end namespace

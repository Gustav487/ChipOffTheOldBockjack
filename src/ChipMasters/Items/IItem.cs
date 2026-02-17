using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Items
{
    /// <summary>
    /// Common contract of an item.
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="IItem"/>s in the <see cref="SItems.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<IItem> ENDEC = SItems.REGISTER.RegistryEnDec(EnDecUtil.STRING);



        /// <summary>
        /// The category of the item.
        /// </summary>
        EItemCategory Category { get; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The price of the item.
        /// </summary>
        int Price { get; }
    } // end interface
} // end namespace

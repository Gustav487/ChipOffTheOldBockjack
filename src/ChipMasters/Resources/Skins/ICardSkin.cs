using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Resources.Skins
{
    /// <summary>
    /// Represents a card skin asset set.
    /// </summary>
    public interface ICardSkin : ISkin
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="ICardSkin"/>s in the <see cref="SCardSkins.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<ICardSkin> ENDEC = SCardSkins.REGISTER.RegistryEnDec(EnDecUtil.STRING);
    } // end interface
} // end namespace
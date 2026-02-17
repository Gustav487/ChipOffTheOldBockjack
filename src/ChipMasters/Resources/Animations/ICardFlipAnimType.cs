using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Resources.Animations
{
    /// <summary>
    /// Contract for a marker identifying a card flip animation.
    /// </summary>
    public interface ICardFlipAnimType
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="ICardFlipAnimType"/>s in the <see cref="SCardFlipTypes.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<ICardFlipAnimType> ENDEC = SCardFlipTypes.REGISTER.RegistryEnDec(EnDecUtil.STRING);
    } // end interface
} // end namespace
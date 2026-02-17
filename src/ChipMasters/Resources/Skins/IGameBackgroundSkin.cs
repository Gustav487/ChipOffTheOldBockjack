using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Resources.Skins
{
    /// <summary>
    /// Represents a game background asset set.
    /// </summary>
    public interface IGameBackgroundSkin : ISkin
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="IGameBackgroundSkin"/>s in the <see cref="SGameBackgroundSkins.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<IGameBackgroundSkin> ENDEC = SGameBackgroundSkins.REGISTER.RegistryEnDec(EnDecUtil.STRING);
    } // end interface
} // end namespace

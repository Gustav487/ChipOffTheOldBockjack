using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Resources.Skins
{
    /// <summary>
    /// Represents a gui skin.
    /// </summary>
    public interface IGUISkin : ISkin
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="IGUISkin"/>s in the <see cref="SGUISkins.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<IGUISkin> ENDEC = SGUISkins.REGISTER.RegistryEnDec(EnDecUtil.STRING);
    } // end interface
} // end namespace
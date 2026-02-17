using ChipMasters.Games.BetHandlers;
using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;

namespace ChipMasters.Games.Matches.Providers
{
    /// <summary>
    /// Contract for an object that creates matches.
    /// </summary>
    public interface IMatchProvider
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding cards associated with <see cref="IBetHandler"/> <see cref="IType{T}"/>s in the <see cref="SMatchProviderTypes.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<IMatchProvider> ENDEC = SMatchProviderTypes.REGISTER.TypeRegistryEnDec();



        /// <summary>
        /// Create new match.
        /// </summary>
        /// <param name="bet">Bet to create match with.</param>
        /// <returns></returns>
        public IMatch Create(int bet);
    } // end interface
} // end namespace
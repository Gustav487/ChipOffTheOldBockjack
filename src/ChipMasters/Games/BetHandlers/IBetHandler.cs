using ChipMasters.Games.Matches;
using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;

namespace ChipMasters.Games.BetHandlers
{
    /// <summary>
    /// Contract for an object that defines legal bet range, and pays bets.
    /// </summary>
    public interface IBetHandler
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="IBetHandler"/>s associated with <see cref="IBetHandler"/> <see cref="IType{T}"/>s in the <see cref="SBetHandlerTypes.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<IBetHandler> ENDEC = SBetHandlerTypes.REGISTER.TypeRegistryEnDec();



        /// <summary>
        /// Pay the bet based on match ending.
        /// </summary>
        /// <param name="match"></param>
        public void Payout(IMatch match);

    } // end interface
} // end namespace
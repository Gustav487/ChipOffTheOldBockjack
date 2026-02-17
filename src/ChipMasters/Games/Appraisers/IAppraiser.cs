using ChipMasters.Games.Hands;
using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;

namespace ChipMasters.Games.Appraisers
{
    /// <summary>
    /// Contract for an object that appraises the state of a match to determine valuing and win conditions.
    /// </summary>
    public interface IAppraiser
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="IAppraiser"/> associated with <see cref="IAppraiser"/> <see cref="IType{T}"/>s in the <see cref="SAppraiserTypes.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<IAppraiser> ENDEC = SAppraiserTypes.REGISTER.TypeRegistryEnDec();



        /// <summary>
        /// Evaluate a <see cref="IHand"/>.
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="includeHidden">Include hidden(upside down) cards in appraisal.</param>
        /// <returns></returns>
        VHandAppraisal AppraiseHand(IHand hand, bool includeHidden);
    } // end interface
} // end namespace
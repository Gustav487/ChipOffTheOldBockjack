using ChipMasters.IO;

namespace ChipMasters.Cards
{
    /// <summary>
    /// Contract for a type identifying a type of <see cref="ICardType"/>, such as ace of spade, three of diamonds, or anything else
    /// </summary>
    public interface ICardType : IType<ICard>
    {

    } // end interface
} // end namespace
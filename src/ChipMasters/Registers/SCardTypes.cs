using ChipMasters.Cards;
using ChipMasters.IO;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a register of all <see cref="ICardType"/>s that the game can recognize and handle.
    /// </summary>
    public static class SCardTypes
    {
        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="ICardType"/>s, and their identifiers.
        /// 
        /// The unique identifier is both written to save files, and used to find the card's assets. 
        /// The type unqiuely identifies each type of a card, and provides the object used to write/read the cards state. 
        /// For instance regarding card types, an ace of spades is a type, even though multiple copies of the card may exist, and may be flipped up or flipped down, and may be instances of different classes.
        /// 
        /// </summary>
        public static readonly IBijectiveDictionary<string, IType<ICard>> REGISTER = new Dictionary<string, IType<ICard>>()
        {
            { "ace_of_clubs", new RStandardCardType(ECardRank.Ace, ECardSuit.Clubs) },
            { "ace_of_diamonds", new RStandardCardType(ECardRank.Ace, ECardSuit.Diamonds) },
            { "ace_of_hearts", new RStandardCardType(ECardRank.Ace, ECardSuit.Hearts) },
            { "ace_of_spades", new RStandardCardType(ECardRank.Ace, ECardSuit.Spades) },

            { "two_of_clubs", new RStandardCardType(ECardRank.Two, ECardSuit.Clubs) },
            { "two_of_diamonds", new RStandardCardType(ECardRank.Two, ECardSuit.Diamonds) },
            { "two_of_hearts", new RStandardCardType(ECardRank.Two, ECardSuit.Hearts) },
            { "two_of_spades", new RStandardCardType(ECardRank.Two, ECardSuit.Spades) },

            { "three_of_clubs", new RStandardCardType(ECardRank.Three, ECardSuit.Clubs) },
            { "three_of_diamonds", new RStandardCardType(ECardRank.Three, ECardSuit.Diamonds) },
            { "three_of_hearts", new RStandardCardType(ECardRank.Three, ECardSuit.Hearts) },
            { "three_of_spades", new RStandardCardType(ECardRank.Three, ECardSuit.Spades) },

            { "four_of_clubs", new RStandardCardType(ECardRank.Four, ECardSuit.Clubs) },
            { "four_of_diamonds", new RStandardCardType(ECardRank.Four, ECardSuit.Diamonds) },
            { "four_of_hearts", new RStandardCardType(ECardRank.Four, ECardSuit.Hearts) },
            { "four_of_spades", new RStandardCardType(ECardRank.Four, ECardSuit.Spades) },

            { "five_of_clubs", new RStandardCardType(ECardRank.Five, ECardSuit.Clubs) },
            { "five_of_diamonds", new RStandardCardType(ECardRank.Five, ECardSuit.Diamonds) },
            { "five_of_hearts", new RStandardCardType(ECardRank.Five, ECardSuit.Hearts) },
            { "five_of_spades", new RStandardCardType(ECardRank.Five, ECardSuit.Spades) },

            { "six_of_clubs", new RStandardCardType(ECardRank.Six, ECardSuit.Clubs) },
            { "six_of_diamonds", new RStandardCardType(ECardRank.Six, ECardSuit.Diamonds) },
            { "six_of_hearts", new RStandardCardType(ECardRank.Six, ECardSuit.Hearts) },
            { "six_of_spades", new RStandardCardType(ECardRank.Six, ECardSuit.Spades) },

            { "seven_of_clubs", new RStandardCardType(ECardRank.Seven, ECardSuit.Clubs) },
            { "seven_of_diamonds", new RStandardCardType(ECardRank.Seven, ECardSuit.Diamonds) },
            { "seven_of_hearts", new RStandardCardType(ECardRank.Seven, ECardSuit.Hearts) },
            { "seven_of_spades", new RStandardCardType(ECardRank.Seven, ECardSuit.Spades) },

            { "eight_of_clubs", new RStandardCardType(ECardRank.Eight, ECardSuit.Clubs) },
            { "eight_of_diamonds", new RStandardCardType(ECardRank.Eight, ECardSuit.Diamonds) },
            { "eight_of_hearts", new RStandardCardType(ECardRank.Eight, ECardSuit.Hearts) },
            { "eight_of_spades", new RStandardCardType(ECardRank.Eight, ECardSuit.Spades) },

            { "nine_of_clubs", new RStandardCardType(ECardRank.Nine, ECardSuit.Clubs) },
            { "nine_of_diamonds", new RStandardCardType(ECardRank.Nine, ECardSuit.Diamonds) },
            { "nine_of_hearts", new RStandardCardType(ECardRank.Nine, ECardSuit.Hearts) },
            { "nine_of_spades", new RStandardCardType(ECardRank.Nine, ECardSuit.Spades) },

            { "ten_of_clubs", new RStandardCardType(ECardRank.Ten, ECardSuit.Clubs) },
            { "ten_of_diamonds", new RStandardCardType(ECardRank.Ten, ECardSuit.Diamonds) },
            { "ten_of_hearts", new RStandardCardType(ECardRank.Ten, ECardSuit.Hearts) },
            { "ten_of_spades", new RStandardCardType(ECardRank.Ten, ECardSuit.Spades) },

            { "jack_of_clubs", new RStandardCardType(ECardRank.Jack, ECardSuit.Clubs) },
            { "jack_of_diamonds", new RStandardCardType(ECardRank.Jack, ECardSuit.Diamonds) },
            { "jack_of_hearts", new RStandardCardType(ECardRank.Jack, ECardSuit.Hearts) },
            { "jack_of_spades", new RStandardCardType(ECardRank.Jack, ECardSuit.Spades) },

            { "king_of_clubs", new RStandardCardType(ECardRank.King, ECardSuit.Clubs) },
            { "king_of_diamonds", new RStandardCardType(ECardRank.King, ECardSuit.Diamonds) },
            { "king_of_hearts", new RStandardCardType(ECardRank.King, ECardSuit.Hearts) },
            { "king_of_spades", new RStandardCardType(ECardRank.King, ECardSuit.Spades) },

            { "queen_of_clubs", new RStandardCardType(ECardRank.Queen, ECardSuit.Clubs) },
            { "queen_of_diamonds", new RStandardCardType(ECardRank.Queen, ECardSuit.Diamonds) },
            { "queen_of_hearts", new RStandardCardType(ECardRank.Queen, ECardSuit.Hearts) },
            { "queen_of_spades", new RStandardCardType(ECardRank.Queen, ECardSuit.Spades) },
        }.ToImmutableBijectiveDictionary();

    } // end class
} // end namespace
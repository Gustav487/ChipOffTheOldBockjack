using ChipMasters.Games.Matches;
using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using System;

namespace ChipMasters.Games.Sessions
{
    /// <summary>
    /// Contract for a single session of the game with a consistent set of rules and rewards, across one or more <see cref="IMatch"/>s.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="ISession"/> associated with <see cref="ISession"/> <see cref="IType{T}"/>s in the <see cref="SSessionTypes.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<ISession> ENDEC = SSessionTypes.REGISTER.TypeRegistryEnDec();



        /// <summary>
        /// The current match of the game session.
        /// </summary>
        public IMatch Match { get; }

        /// <summary>
        /// Event raised when a new match begins.
        /// </summary>
        public event Action? OnMatchChanged;
        /* not currently used, add only once feature needed
                /// <summary>
                /// Can the session keep going. ex.g. time limits aren't exhuasted, and dealer and player still have funds
                /// </summary>
                public bool IsConcluded { get; }

                /// <summary>
                /// Event fired when the session becomes unplayable.
                /// </summary>
                public event Action? OnConcluded;*/



        /// <summary>
        /// Start another match.
        /// </summary>
        /// <param name="bet"></param>
        public void PlayAgain(int bet);

    } // end interface
} // end namespace

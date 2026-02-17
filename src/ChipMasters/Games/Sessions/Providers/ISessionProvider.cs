using ChipMasters.Games.Matches;

namespace ChipMasters.Games.Sessions.Providers
{
    /// <summary>
    /// Contract for an object for creating a new session.
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// Create new match.
        /// </summary>
        /// <param name="bet">Bet to create <see cref="ISession"/>'s first <see cref="IMatch"/> with.</param>
        /// <returns></returns>
        public ISession Create(int bet);
    } // end interface
} // end namespace
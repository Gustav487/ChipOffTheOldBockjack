using ChipMasters.Games.Sessions;
using ChipMasters.Games.Sessions.Providers;

namespace Fakes.ChipMasters.Games.Sessions.Providers
{
    public sealed class FakeSessionProvider : ISessionProvider
    {
        public ISession Create(int bet) => new FakeSession();
    } // end class
} // end namespace
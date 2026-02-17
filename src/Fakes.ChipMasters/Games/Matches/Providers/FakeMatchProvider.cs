using ChipMasters.Games.Matches;
using ChipMasters.Games.Matches.Providers;

namespace Fakes.ChipMasters.Games.Matches.Providers
{
    public class FakeMatchProvider : IMatchProvider
    {
        private readonly Func<int, IMatch> _func;



        public FakeMatchProvider(Func<int, IMatch> func)
        {
            _func = func;
        } // end ctor



        public IMatch Create(int bet) => _func(bet);
    } // end class
} // end namespace
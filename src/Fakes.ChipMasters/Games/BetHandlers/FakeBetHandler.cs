using ChipMasters.Games.BetHandlers;
using ChipMasters.Games.Matches;
using ChipMasters.User;

namespace Fakes.ChipMasters.Games.BetHandlers
{
    public class FakeBetHandler : IBetHandler
    {
        public VBetRange BetRange => throw new NotImplementedException();

        public IWallet DealerWallet => throw new NotImplementedException();

        public IWallet PlayerWallet => throw new NotImplementedException();

        public event Action<IMatch>? OnPayout;



        public void Payout(IMatch match)
        {
            OnPayout?.Invoke(match);
        } // end Payout
    } // end class
} // end namespace
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Sessions;
using ChipMasters.Menu.Bankruptcy;
using ChipMasters.User;
using Fakes.ChipMasters.Games.Appraisers;
using Fakes.ChipMasters.Games.Matches;
using Fakes.ChipMasters.Games.Sessions;
using Fakes.ChipMasters.Menus.Bankruptcy;
using Fakes.ChipMasters.Menus.Displays.Assets.Animations.MatchConclusions;

namespace Tests.ChipMasters.Menus.Bankruptcy
{
    public static class TestRBankruptcyTrigger
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(1, EHandState.Neutral, 0, EHandState.Neutral, 0, true)]
            [DataRow(1, EHandState.Neutral, 1, EHandState.Neutral, 50, false)]
            [DataRow(1, EHandState.Neutral, 3, EHandState.Neutral, -1, true)]
            public void TriggerByBankruptcy(int v1, EHandState s1, int v2, EHandState s2, int chips, bool exVisibility)
            {
                FakeMatch m = new(
                    isConcluded: true,
                    appraiser: new FakeOrdinalAppraiser(
                    new VHandAppraisal(new int[] { v1 }, s1),
                    new VHandAppraisal(new int[] { v2 }, s2)));
                ISession s = new FakeSession(m);
                IUser u = new RUser();
                u.Wallet.Chips = chips;
                FakeMatchConclusionAnimator a = new();
                FakeBankruptcyMenu bm = new();

                RBankruptcyMenuTrigger _ = new(u.Wallet, s, a, bm);

                Assert.AreEqual(exVisibility, bm.WasShown);
            } // end TriggerByConstruction()
        } // end inner class Valid
    } // end class
} // end namespace

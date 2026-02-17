using ChipMasters.Games.Matches;
using ChipMasters.Games.Sessions;
using Fakes.ChipMasters.Games.BetHandlers;
using Fakes.ChipMasters.Games.Matches;
using Fakes.ChipMasters.Games.Matches.Providers;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.Games.Sessions
{
    public static class TestRStandardSession
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(2, false, 0)]
            [DataRow(0, false, 0)]
            [DataRow(843, false, 0)]
            [DataRow(1323, true, 1)]
            [DataRow(0, true, 1)]
            [DataRow(-789, true, 1)]
            public void Ctor(int bet, bool isConcluded, int exPayoutCount)
            {
                Func<int, IMatch> matchSupplier = (x) => new FakeMatch(bet: x, isConcluded: isConcluded);
                FakeBetHandler fbh = new();
                int payoutCount = 0;
                fbh.OnPayout += (_) => payoutCount++;

                RStandardSession session = new(bet, new FakeMatchProvider(matchSupplier), fbh, new FakeMetrics());

                // Assert.IsFalse(session.IsConcluded);
                Assert.IsNotNull(session.Match);
                Assert.AreEqual(bet, session.Match.Bet);
                Assert.AreEqual(isConcluded, session.Match.IsConcluded);

                Assert.AreEqual(exPayoutCount, payoutCount);
            } // end Ctor()

            [TestMethod]
            [DataRow(0, 0, 1)]
            [DataRow(-1, 0, 1)]
            [DataRow(-1023011, 0, 1)]
            [DataRow(1, 0, 1)]
            [DataRow(931, 0, 1)]
            public void CtorMatchConcludedPayout(int bet, int exPrePayoutCount, int exPostPayoutCount) // test that match conclusion triggers payout for the initially created match.
            {
                FakeMatch f = new(bet: bet);
                Func<int, IMatch> matchSupplier = (x) => f;

                FakeBetHandler fbh = new();
                int payoutCount = 0;
                fbh.OnPayout += (_) => payoutCount++;

                RStandardSession session = new(bet, new FakeMatchProvider(matchSupplier), fbh, new FakeMetrics());

                // Assert.IsFalse(session.IsConcluded);
                Assert.IsNotNull(session.Match);
                Assert.IsFalse(session.Match.IsConcluded);
                Assert.AreEqual(exPrePayoutCount, payoutCount);

                f.Conclude();

                Assert.IsTrue(session.Match.IsConcluded);
                Assert.AreEqual(exPostPayoutCount, payoutCount);
            } // end CtorMatchConcludedPayout()

            [TestMethod]
            [DataRow(0, false, 12, 0, 1)]
            [DataRow(92, false, -3, 0, 1)]
            [DataRow(-103, false, 0, 0, 1)]
            [DataRow(0, true, 12, 1, 2)]
            [DataRow(92, true, -3, 1, 2)]
            [DataRow(-103, true, 0, 1, 2)]
            public void PlayAgainMatchConcludePayout(int bet1, bool isConcluded, int bet2, int exPrePayoutCount, int exPostPayoutCount) // test that match conclusion triggers payout for the initially created match.
            {
                FakeMatch f1 = new(bet: bet1, isConcluded: isConcluded);
                FakeMatch f2 = new(bet: bet2);
                Queue<IMatch> q = new Queue<IMatch>(new IMatch[] { f1, f2 });
                Func<int, IMatch> matchSupplier = (x) => q.Dequeue();

                FakeBetHandler fbh = new();
                int payoutCount = 0;
                fbh.OnPayout += (_) => payoutCount++;

                RStandardSession session = new(bet1, new FakeMatchProvider(matchSupplier), fbh, new FakeMetrics());
                Assert.AreEqual(f1, session.Match);
                session.PlayAgain(bet2);

                // Assert.IsFalse(session.IsConcluded);
                Assert.AreEqual(f2, session.Match);
                Assert.IsFalse(session.Match.IsConcluded);
                Assert.AreEqual(exPrePayoutCount, payoutCount);

                f2.Conclude();

                Assert.IsTrue(session.Match.IsConcluded);
                Assert.AreEqual(exPostPayoutCount, payoutCount);
            } // end PlayAgainMatchConcludePayout()

            [TestMethod]
            [DataRow(0, false, 12, 0, 0)]
            [DataRow(92, false, -3, 0, 0)]
            [DataRow(-103, false, 0, 0, 0)]
            [DataRow(0, true, 12, 1, 1)]
            [DataRow(92, true, -3, 1, 1)]
            [DataRow(-103, true, 0, 1, 1)]
            public void ReplacedMatchCantTriggerPayout(int bet1, bool isConcluded, int bet2, int exPrePayoutCount, int exPostPayoutCount) // test that match conclusion triggers payout for the initially created match.
            {
                FakeMatch f1 = new(bet: bet1, isConcluded: isConcluded);
                FakeMatch f2 = new(bet: bet2);
                Queue<IMatch> q = new Queue<IMatch>(new IMatch[] { f1, f2 });
                Func<int, IMatch> matchSupplier = (x) => q.Dequeue();

                FakeBetHandler fbh = new();
                int payoutCount = 0;
                fbh.OnPayout += (_) => payoutCount++;

                RStandardSession session = new(bet1, new FakeMatchProvider(matchSupplier), fbh, new FakeMetrics());
                Assert.AreEqual(f1, session.Match);
                session.PlayAgain(bet2);

                // Assert.IsFalse(session.IsConcluded);
                Assert.AreEqual(f2, session.Match);
                Assert.IsFalse(session.Match.IsConcluded);
                Assert.AreEqual(exPrePayoutCount, payoutCount);

                f1.Conclude();

                Assert.IsFalse(session.Match.IsConcluded);
                Assert.AreEqual(exPostPayoutCount, payoutCount);
            } // end ReplacedMatchCantTriggerPayout()
        } // end inner class Valid
    } // end class
} // end namespace
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Matches;
using ChipMasters.User;
using Fakes.ChipMasters.Games.Appraisers;
using Fakes.ChipMasters.Games.Matches;

namespace Tests.ChipMasters.User
{
    [TestClass]
    public class TestRMetrics
    {
        [TestMethod]
        public void RecordChipCount()
        {
            IMetrics m = new RMetrics();
            Assert.AreEqual(0, m.ChipHistory.Count);

            for (int i = 0; i <= RMetrics.CHIP_HISTORY_LENGTH; i++)
                m.RecordChipCount(i);

            Assert.AreEqual(RMetrics.CHIP_HISTORY_LENGTH, m.ChipHistory.Count);
            Assert.AreEqual(1, m.ChipHistory[0].Count); // should've removed the oldest entry i.e. '0'
            Assert.AreEqual(RMetrics.CHIP_HISTORY_LENGTH, m.ChipHistory[^1].Count);
        } // end RecordChipCount()

        [TestMethod]
        [DataRow(0, RMetrics.CHIP_HISTORY_LENGTH + 1, 1, RMetrics.CHIP_HISTORY_LENGTH)]
        [DataRow(0, RMetrics.CHIP_HISTORY_LENGTH + 4, 4, RMetrics.CHIP_HISTORY_LENGTH + 3)]
        [DataRow(1, RMetrics.CHIP_HISTORY_LENGTH + 4, 5, RMetrics.CHIP_HISTORY_LENGTH + 4)]
        public void Ctor_ChipHistoryClamped(int r1, int r2, int ex1, int ex2)
        {
            IMetrics m = new RMetrics(
                Array.Empty<VMatchRecord>(),
                Enumerable
                    .Range(r1, r2)
                    .Select((x) => new VChipRecord(DateTime.MaxValue, x, null)),
                new Dictionary<string, int>());

            Assert.AreEqual(RMetrics.CHIP_HISTORY_LENGTH, m.ChipHistory.Count);
            Assert.AreEqual(ex1, m.ChipHistory[0].Count);
            Assert.AreEqual(ex2, m.ChipHistory[^1].Count);
        } // end Ctor_ChipHistoryClamped()



        [TestMethod]
        public void RecordMatch()
        {
            IMetrics m = new RMetrics();
            Assert.AreEqual(0, m.MatchHistory.Count);

            for (int i = 0; i <= RMetrics.MATCH_HISTORY_LENGTH; i++)
                m.RecordMatch(FM(i));

            Assert.AreEqual(RMetrics.MATCH_HISTORY_LENGTH, m.MatchHistory.Count);
            Assert.AreEqual(1, m.MatchHistory[0].Bet); // should've removed the oldest entry i.e. '0'
            Assert.AreEqual(RMetrics.MATCH_HISTORY_LENGTH, m.MatchHistory[^1].Bet);
        } // end RecordMatch()

        [TestMethod]
        [DataRow(0, RMetrics.MATCH_HISTORY_LENGTH + 1, 1, RMetrics.MATCH_HISTORY_LENGTH)]
        [DataRow(0, RMetrics.MATCH_HISTORY_LENGTH + 4, 4, RMetrics.MATCH_HISTORY_LENGTH + 3)]
        [DataRow(1, RMetrics.MATCH_HISTORY_LENGTH + 4, 5, RMetrics.MATCH_HISTORY_LENGTH + 4)]
        public void Ctor_MatchHistoryClamped(int r1, int r2, int ex1, int ex2)
        {
            IMetrics m = new RMetrics(
                Enumerable
                    .Range(r1, r2)
                    .Select(FM)
                    .Select((x) => new VMatchRecord(x)),
                Array.Empty<VChipRecord>(),
                new Dictionary<string, int>());

            Assert.AreEqual(RMetrics.MATCH_HISTORY_LENGTH, m.MatchHistory.Count);
            Assert.AreEqual(ex1, m.MatchHistory[0].Bet);
            Assert.AreEqual(ex2, m.MatchHistory[^1].Bet);
        } // end Ctor_ChipHistoryClamped()

        [TestMethod]
        [DataRow(0, 0, null, 0)]
        [DataRow(0, 2, null, 2)]
        [DataRow(4333, 2, null, -4331)]
        public void CalculatesDelta(int a, int b, int? exADel, int? exBDel)
        {
            IMetrics m = new RMetrics();
            m.RecordChipCount(a);
            m.RecordChipCount(b);

            Assert.AreEqual(a, m.ChipHistory[0].Count);
            Assert.AreEqual(exADel, m.ChipHistory[0].Delta);

            Assert.AreEqual(b, m.ChipHistory[1].Count);
            Assert.AreEqual(exBDel, m.ChipHistory[1].Delta);
        } // end CalculatesDelta()



        private static IMatch FM(int i)
        {
            IAppraiser ap = new FakeOrdinalAppraiser(
                new VHandAppraisal(new int[] { i }, EHandState.Neutral),
                new VHandAppraisal(new int[] { i }, EHandState.Neutral));
            return new FakeMatch(bet: i, appraiser: ap);
        } // end FM()


        [TestMethod]

        [DataRow(SStats.GAMES_PLAYED, 0)]
        [DataRow(SStats.WINS, 42)]
        [DataRow(SStats.LOSSES, int.MaxValue)]
        [DataRow(SStats.BUSTS, 13)]
        public void SetStat_StoresAndReturnsValue(string statKey, int value)
        {
            var metrics = new RMetrics();
            metrics.SetStat(statKey, value);

            int result = metrics.GetStat(statKey);
            Assert.AreEqual(value, result);
        } // end SetStat_StoresAndReturnsValue()


        [TestMethod]

        [DataRow(0, 0, 0)]
        [DataRow(50, 0, 0)]
        [DataRow(0, 1, 99)]
        public void WinRatio(int wins, int ties, int losses)
        {
            var metrics = new RMetrics();
            metrics.SetStat(SStats.WINS, wins);
            metrics.SetStat(SStats.TIES, ties);
            metrics.SetStat(SStats.LOSSES, losses);

            var ratio = metrics.WinRatio;

            Assert.AreEqual(wins, ratio.Wins);
            Assert.AreEqual(ties, ratio.Ties);
            Assert.AreEqual(losses, ratio.Losses);
        } // end WinRatio()


    } // end class
} // end namespace
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Sessions;
using ChipMasters.Menu.Displays.Assets.Animations.MatchConclusion;
using Fakes.ChipMasters.Games.Appraisers;
using Fakes.ChipMasters.Games.Matches;
using Fakes.ChipMasters.Games.Sessions;
using Fakes.ChipMasters.Menus.Displays.Assets.Animations.MatchConclusions;

namespace Tests.ChipMasters.Menus.Displays.Assets.Animations.MatchConclusions
{
    [TestClass]
    public class TestRSessionMatchConclusionAnimTrigger
    {
        [TestMethod]
        [DataRow(1, EHandState.Neutral, 0, EHandState.Neutral, 1, 0, 0)]
        [DataRow(1, EHandState.Neutral, 1, EHandState.Neutral, 0, 1, 0)]
        [DataRow(1, EHandState.Neutral, 3, EHandState.Neutral, 0, 0, 1)]
        public void TriggerByConstruction(int v1, EHandState s1, int v2, EHandState s2, int exWins, int exTies, int exLosses)
        {
            FakeMatch m = new(
                isConcluded: true,
                appraiser: new FakeOrdinalAppraiser(
                new VHandAppraisal(new int[] { v1 }, s1),
                new VHandAppraisal(new int[] { v2 }, s2))); ;
            ISession s = new FakeSession(m);
            FakeMatchConclusionAnimator a = new();

            RSessionMatchConclusionAnimTrigger trigger = new(s, a);

            Assert.AreEqual(true, a.IsPlaying);
            Assert.AreEqual(exWins, a.Wins);
            Assert.AreEqual(exTies, a.Ties);
            Assert.AreEqual(exLosses, a.Losses);
        } // end TriggerByConstruction()

        [TestMethod]
        [DataRow(1, EHandState.Neutral, 0, EHandState.Neutral, 1, 0, 0)]
        [DataRow(1, EHandState.Neutral, 1, EHandState.Neutral, 0, 1, 0)]
        [DataRow(1, EHandState.Neutral, 3, EHandState.Neutral, 0, 0, 1)]
        public void TriggerByConclusion(int v1, EHandState s1, int v2, EHandState s2, int exWins, int exTies, int exLosses)
        {
            FakeMatch m = new(appraiser: new FakeOrdinalAppraiser(
                new VHandAppraisal(new int[] { v1 }, s1),
                new VHandAppraisal(new int[] { v2 }, s2)));
            ISession s = new FakeSession(m);
            FakeMatchConclusionAnimator a = new();
            RSessionMatchConclusionAnimTrigger trigger = new(s, a);

            m.Conclude();

            Assert.AreEqual(true, a.IsPlaying);
            Assert.AreEqual(exWins, a.Wins);
            Assert.AreEqual(exTies, a.Ties);
            Assert.AreEqual(exLosses, a.Losses);
        } // end TriggerByConclusion()

        [TestMethod]
        [DataRow(1, EHandState.Neutral, 0, EHandState.Neutral, 1, EHandState.Bust, 0, EHandState.Neutral, 1, 0, 1)]
        [DataRow(1, EHandState.Neutral, 1, EHandState.Neutral, 1, EHandState.Neutral, 12, EHandState.Neutral, 0, 1, 1)]
        [DataRow(1, EHandState.Neutral, 3, EHandState.Neutral, 1, EHandState.Neutral, 1, EHandState.Neutral, 0, 1, 1)]
        [DataRow(1, EHandState.Neutral, 3, EHandState.Neutral, 1, EHandState.Neutral, 1999, EHandState.Neutral, 0, 0, 2)]
        public void TriggerByConstructionCycledOver(int v1, EHandState s1, int v2, EHandState s2,
            int v3, EHandState s3, int v4, EHandState s4,
            int exWins, int exTies, int exLosses)
        {
            FakeMatch m = new(
                isConcluded: true,
                appraiser: new FakeOrdinalAppraiser(
                new VHandAppraisal(new int[] { v1 }, s1),
                new VHandAppraisal(new int[] { v2 }, s2)));
            FakeMatch m2 = new(
                isConcluded: true,
                appraiser: new FakeOrdinalAppraiser(
                new VHandAppraisal(new int[] { v3 }, s3),
                new VHandAppraisal(new int[] { v4 }, s4)));
            ISession s = new FakeSession(m, m2);
            FakeMatchConclusionAnimator a = new();

            RSessionMatchConclusionAnimTrigger trigger = new(s, a);
            s.PlayAgain(0);

            Assert.AreEqual(true, a.IsPlaying);
            Assert.AreEqual(exWins, a.Wins);
            Assert.AreEqual(exTies, a.Ties);
            Assert.AreEqual(exLosses, a.Losses);
        } // end TriggerByConstructionCycledOver()


        [TestMethod]
        [DataRow(1, EHandState.Neutral, 0, EHandState.Neutral, 1, EHandState.Bust, 0, EHandState.Neutral, 1, 0, 1)]
        [DataRow(1, EHandState.Neutral, 1, EHandState.Neutral, 1, EHandState.Neutral, 12, EHandState.Neutral, 0, 1, 1)]
        [DataRow(1, EHandState.Neutral, 3, EHandState.Neutral, 1, EHandState.Neutral, 1, EHandState.Neutral, 0, 1, 1)]
        [DataRow(1, EHandState.Neutral, 3, EHandState.Neutral, 1, EHandState.Neutral, 1999, EHandState.Neutral, 0, 0, 2)]
        public void TriggerByConclusionCycledOver(int v1, EHandState s1, int v2, EHandState s2,
            int v3, EHandState s3, int v4, EHandState s4,
            int exWins, int exTies, int exLosses)
        {
            FakeMatch m = new(
                isConcluded: true,
                appraiser: new FakeOrdinalAppraiser(
                new VHandAppraisal(new int[] { v1 }, s1),
                new VHandAppraisal(new int[] { v2 }, s2)));
            FakeMatch m2 = new(
                isConcluded: true,
                appraiser: new FakeOrdinalAppraiser(
                new VHandAppraisal(new int[] { v3 }, s3),
                new VHandAppraisal(new int[] { v4 }, s4)));
            ISession s = new FakeSession(m, m2);
            FakeMatchConclusionAnimator a = new();
            RSessionMatchConclusionAnimTrigger trigger = new(s, a);

            m.Conclude();
            s.PlayAgain(0);
            m2.Conclude();

            Assert.AreEqual(true, a.IsPlaying);
            Assert.AreEqual(exWins, a.Wins);
            Assert.AreEqual(exTies, a.Ties);
            Assert.AreEqual(exLosses, a.Losses);
        } // end TriggerByConstructionCycledOver()

        [TestMethod]
        public void Dispose()
        {
            FakeMatch m = new();
            ISession s = new FakeSession(m);
            IMatchConclusionAnimator a = new FakeMatchConclusionAnimator();
            RSessionMatchConclusionAnimTrigger trigger = new(s, a);

            trigger.Dispose();
            m.Conclude(); // run conclude to assure event is detached.

            Assert.AreEqual(a.IsPlaying, false);
        } // end Dispose()

    } // end class
} // end namespace
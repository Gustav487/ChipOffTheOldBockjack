using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.Matches;
using Fakes.ChipMasters.Games.Appraisers;
using Fakes.ChipMasters.Games.Matches;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.Displays.Matches
{
    [TestClass]
    public class TestRConclusionSounds
    {
        [TestMethod]
        [DataRow(9, EHandState.Neutral, 8, EHandState.Neutral, true, false, false)]
        [DataRow(9, EHandState.Bust, 8, EHandState.Neutral, false, false, true)]
        [DataRow(9, EHandState.Bust, 8, EHandState.Bust, false, true, false)]
        public void SetMatchConcluded(int pV, EHandState pS, int dV, EHandState dS, bool exWPlaying, bool exTPlaying, bool exLPlaying)
        {
            VHandAppraisal pAp = new(new int[] { pV }, pS);
            VHandAppraisal dAp = new(new int[] { dV }, dS);

            IMatch m = new FakeMatch(isConcluded: true, appraiser: new FakeOrdinalAppraiser(pAp, dAp));

            IAudioStreamPlayer wp = new FakeAudioStreamPlayer();
            IAudioStreamPlayer tp = new FakeAudioStreamPlayer();
            IAudioStreamPlayer lp = new FakeAudioStreamPlayer();

            IDisplay<IMatch> cs = new RMatchConclusionsSoundDisplay(wp, tp, lp);

            // act
            cs.Display = m;

            // assert
            Assert.AreEqual(exWPlaying, wp.Playing);
            Assert.AreEqual(exTPlaying, tp.Playing);
            Assert.AreEqual(exLPlaying, lp.Playing);
        } // end SetMatchConcluded()

        [TestMethod]
        [DataRow(9, EHandState.Neutral, 8, EHandState.Neutral, true, false, false)]
        [DataRow(9, EHandState.Bust, 8, EHandState.Neutral, false, false, true)]
        [DataRow(9, EHandState.Bust, 8, EHandState.Bust, false, true, false)]
        public void SetMatchOnConcluded(int pV, EHandState pS, int dV, EHandState dS, bool exWPlaying, bool exTPlaying, bool exLPlaying)
        {
            VHandAppraisal pAp = new(new int[] { pV }, pS);
            VHandAppraisal dAp = new(new int[] { dV }, dS);

            FakeMatch m = new FakeMatch(isConcluded: false, appraiser: new FakeOrdinalAppraiser(pAp, dAp));

            IAudioStreamPlayer wp = new FakeAudioStreamPlayer();
            IAudioStreamPlayer tp = new FakeAudioStreamPlayer();
            IAudioStreamPlayer lp = new FakeAudioStreamPlayer();

            IDisplay<IMatch> cs = new RMatchConclusionsSoundDisplay(wp, tp, lp);

            cs.Display = m;

            Assert.IsFalse(wp.Playing); // shouldn't play yet
            Assert.IsFalse(tp.Playing);
            Assert.IsFalse(lp.Playing);

            m.Conclude();

            Assert.AreEqual(exWPlaying, wp.Playing);
            Assert.AreEqual(exTPlaying, tp.Playing);
            Assert.AreEqual(exLPlaying, lp.Playing);
        } // end SetMatchOnConcluded()

        [TestMethod]
        public void SetMatchNull()
        {
            IAudioStreamPlayer wp = new FakeAudioStreamPlayer();
            IAudioStreamPlayer tp = new FakeAudioStreamPlayer();
            IAudioStreamPlayer lp = new FakeAudioStreamPlayer();

            IDisplay<IMatch> cs = new RMatchConclusionsSoundDisplay(wp, tp, lp);

            // act
            cs.Display = null;

            // assert
            Assert.IsFalse(wp.Playing);
            Assert.IsFalse(tp.Playing);
            Assert.IsFalse(lp.Playing);
        } // end SetMatch()

        [TestMethod]
        [DataRow(9, EHandState.Neutral, 8, EHandState.Neutral, true, false, false)]
        [DataRow(9, EHandState.Bust, 8, EHandState.Neutral, false, false, true)]
        [DataRow(9, EHandState.Bust, 8, EHandState.Bust, false, true, false)]
        public void Dispose(int pV, EHandState pS, int dV, EHandState dS, bool exWPlaying, bool exTPlaying, bool exLPlaying)
        {
            VHandAppraisal pAp = new(new int[] { pV }, pS);
            VHandAppraisal dAp = new(new int[] { dV }, dS);

            FakeMatch m = new FakeMatch(isConcluded: false, appraiser: new FakeOrdinalAppraiser(pAp, dAp));

            IAudioStreamPlayer wp = new FakeAudioStreamPlayer();
            IAudioStreamPlayer tp = new FakeAudioStreamPlayer();
            IAudioStreamPlayer lp = new FakeAudioStreamPlayer();

            IDisplay<IMatch> cs = new RMatchConclusionsSoundDisplay(wp, tp, lp);

            cs.Display = m;

            Assert.IsFalse(wp.Playing); // shouldn't play yet
            Assert.IsFalse(tp.Playing);
            Assert.IsFalse(lp.Playing);

            cs.Dispose();
            m.Conclude();

            Assert.IsFalse(wp.Playing); // shouldn't play due to dispose.
            Assert.IsFalse(tp.Playing);
            Assert.IsFalse(lp.Playing);
        } // end SetMatchOnConcluded()
    } // end class
} // end namespace
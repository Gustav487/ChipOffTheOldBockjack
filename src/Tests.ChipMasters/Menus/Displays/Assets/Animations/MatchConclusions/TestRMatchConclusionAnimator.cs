using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Assets.Animations.MatchConclusion;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.Displays.Assets.Animations.MatchConclusions
{
    [TestClass]
    public class TestRMatchConclusionAnimator
    {
        [TestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public void IsPlaying(bool isPlaying, bool exPlaying)
        {
            IAnimatedSprite2D a = new FakeAnimatedSprite2D() { _IsPlaying_ = isPlaying };
            IMatchConclusionAnimator mca = new RMatchConclusionAnimator(a, "", "", "");

            Assert.AreEqual(mca.IsPlaying, exPlaying);
        } // end IsPlaying()

        [TestMethod]
        public void OnStopped()
        {
            FakeAnimatedSprite2D a = new() { _IsPlaying_ = true };
            IMatchConclusionAnimator mca = new RMatchConclusionAnimator(a, "", "", "");
            int stoppedCount = 0;
            mca.OnStopped += () => stoppedCount++;

            Assert.AreEqual(0, stoppedCount);
            a._Finish_();
            Assert.AreEqual(1, stoppedCount);
        } // end OnStopped()

        private void Play(Action<IMatchConclusionAnimator> play, string winKey, string tieKey, string lossKey, string exAnim)
        {
            FakeAnimatedSprite2D a = new() { _IsPlaying_ = false, Visible = false };
            IMatchConclusionAnimator mca = new RMatchConclusionAnimator(a, winKey: winKey, tieKey: tieKey, lossKey: lossKey);
            int stoppedCount = 0;
            mca.OnStopped += () => stoppedCount++;
            int playedCount = 0;
            mca.OnPlaying += () => playedCount++;

            Assert.AreEqual(0, stoppedCount);
            Assert.AreEqual(0, playedCount);
            Assert.AreEqual(false, mca.IsPlaying);
            Assert.AreEqual(false, a.Visible);
            Assert.AreEqual(false, a.IsPlaying());
            Assert.AreEqual(null, a._Playing_);

            play(mca);

            Assert.AreEqual(0, stoppedCount);
            Assert.AreEqual(1, playedCount);
            Assert.AreEqual(true, mca.IsPlaying);
            Assert.AreEqual(true, a.Visible);
            Assert.AreEqual(true, a.IsPlaying());
            Assert.AreEqual(exAnim, a._Playing_);
        } // end Play()

        [TestMethod]
        [DataRow("w", "t", "l")]
        [DataRow("winnn", "winnn", "winnn")]
        public void PlayWin(string winKey, string tieKey, string lossKey)
            => Play((x) => x.PlayWin(), winKey: winKey, tieKey: tieKey, lossKey: lossKey,
                exAnim: winKey);

        [TestMethod]
        [DataRow("w", "t", "l")]
        [DataRow("tieee", "tieee", "tieee")]
        public void PlayTie(string winKey, string tieKey, string lossKey)
            => Play((x) => x.PlayTie(), winKey: winKey, tieKey: tieKey, lossKey: lossKey,
                exAnim: tieKey);

        [TestMethod]
        [DataRow("w", "t", "l")]
        [DataRow("lossss", "lossss", "lossss")]
        public void PlayLoss(string winKey, string tieKey, string lossKey)
            => Play((x) => x.PlayLoss(), winKey: winKey, tieKey: tieKey, lossKey: lossKey,
                exAnim: lossKey);

    } // end class
} // end namespace
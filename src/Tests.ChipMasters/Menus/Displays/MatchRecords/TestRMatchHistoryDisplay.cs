using ChipMasters.Games.Appraisers;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.MatchRecords;
using ChipMasters.User;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.Displays.MatchRecords
{
    [TestClass]
    public class TestRMatchHistoryDisplay
    {
        [TestMethod]
        [DataRow(RMatchHistoryDisplay.UNDEFINED, null)]
        [DataRow(RMatchHistoryDisplay.NO_DATA)]
        [DataRow("$1 | 2 vs 2", 1, 2, EHandState.Neutral, 2, EHandState.Neutral)]
        [DataRow("" +
            "$1 | 2 vs 2\r" +
            "$1 | 2 vs 2",
            1, 2, EHandState.Neutral, 2, EHandState.Neutral,
            1, 2, EHandState.Neutral, 2, EHandState.Neutral)]
        [DataRow("" +
            "$100 | Bust vs 2\r" +
            "$1   |    2 vs 2",
            100, 2, EHandState.Bust, 2, EHandState.Neutral,
            1, 2, EHandState.Neutral, 2, EHandState.Neutral)]
        [DataRow("" +
            "$100 | Bust vs Blackjack\r" +
            "$1   |    2 vs 2",
            100, 2, EHandState.Bust, 2, EHandState.Blackjack,
            1, 2, EHandState.Neutral, 2, EHandState.Neutral)]
        [DataRow("" +
            "$100  | Bust vs Blackjack\r" +
            "$1944 | 1944 vs 3\r" +
            "$1944 |    0 vs Bust",
            100, 2, EHandState.Bust, 2, EHandState.Blackjack,
            1944, 1944, EHandState.Neutral, 3, EHandState.Neutral,
            1944, 0, EHandState.Neutral, 23, EHandState.Bust)]
        public void SetHistory(string ex, params object[] history)
        {
            List<VMatchRecord>? h;
            if (history.Length == 1)
                h = null;
            else
            {
                h = new();
                Assert.AreEqual(0, history.Length % 5);
                for (int i = 0; i < history.Length / 5; i++)
                {
                    int indO = i * 5;
                    h.Add(new(
                        (int)history[indO + 0],
                        new VHandAppraisal(new int[] { (int)history[indO + 1] }, (EHandState)history[indO + 2]),
                        new VHandAppraisal(new int[] { (int)history[indO + 3] }, (EHandState)history[indO + 4])
                        ));
                }
            }

            ILabel l = new FakeLabel();
            Assert.AreEqual("", l.Text);

            IDisplay<IReadOnlyList<VMatchRecord>> mhd = new RMatchHistoryDisplay(l);
            Assert.AreEqual(RMatchHistoryDisplay.UNDEFINED, l.Text);

            mhd.Display = h;
            Assert.AreEqual(ex, l.Text);
        } // SetHistory()
    } // end class
} // end namespace
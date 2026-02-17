using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Hand.Appraisal;
using ChipMasters.Registers;
using Fakes.ChipMasters.Cards;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.Displays.Hands.Appraisals
{
    public static class TestRHandAppraisalDisplay
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow("24", "seven_of_clubs", "nine_of_diamonds", "eight_of_clubs")]
            public void LabelsStatic1(string exTotal, params string[] args)
            {
                IList<ICard> cardArgs = args.Select((x) => SCardTypes.REGISTER[x].Instantiate()).ToList();

                IHand hand = new RHand(cardArgs);
                ILabel total = new FakeLabel();
                IHandAppraisalDisplay had = new RHandAppraisalDisplay(total);
                Assert.AreEqual("N/A", total.Text);

                had.Appraiser = new RStandardAppraiser();
                had.Hand = hand;

                Assert.AreEqual(exTotal, total.Text);
            } // end LabelsStatic1()

            [TestMethod]
            [DataRow("24", "seven_of_clubs", "nine_of_diamonds", "eight_of_clubs")]
            public void LabelsStatic2(string exTotal, params string[] args)
            {
                IList<ICard> cardArgs = args.Select((x) => SCardTypes.REGISTER[x].Instantiate()).ToList();

                IHand hand = new RHand(cardArgs);
                ILabel total = new FakeLabel();
                IHandAppraisalDisplay had = new RHandAppraisalDisplay(total);
                Assert.AreEqual("N/A", total.Text);

                had.Hand = hand;
                had.Appraiser = new RStandardAppraiser();

                Assert.AreEqual(exTotal, total.Text);
            } // end LabelsStatic2()

            [TestMethod]
            [DataRow("18?", "seven_of_clubs", "nine_of_diamonds", "two_of_clubs")]
            [DataRow("21?", "ace_of_clubs", "eight_of_diamonds", "two_of_clubs")] // assure ace adjust doesn't reveal any information
            public void Labels1Vieled(string exTotal, params string[] args)
            {
                IList<ICard> cardArgs = args.Select((x) => SCardTypes.REGISTER[x].Instantiate()).ToList();

                IHand hand = new RHand(cardArgs);
                ILabel total = new FakeLabel();
                IHandAppraisalDisplay had = new RHandAppraisalDisplay(total);
                Assert.AreEqual("N/A", total.Text);

                had.Appraiser = new RStandardAppraiser();
                had.Hand = hand;

                hand.AddCard(new FakeCard(ECardRank.Two, ECardSuit.Clubs, veiled: true));

                Assert.AreEqual(exTotal, total.Text);
            } // end Labels1Veiled()

            [TestMethod]
            [DataRow("18?", "seven_of_clubs", "nine_of_diamonds", "two_of_clubs")]
            public void Labels2Vieled1(string exTotal, params string[] args)
            {
                IList<ICard> cardArgs = args.Select((x) => SCardTypes.REGISTER[x].Instantiate()).ToList();

                IHand hand = new RHand(cardArgs);
                ILabel total = new FakeLabel();
                IHandAppraisalDisplay had = new RHandAppraisalDisplay(total);
                Assert.AreEqual("N/A", total.Text);

                had.Appraiser = new RStandardAppraiser();
                had.Hand = hand;

                hand.AddCard(new FakeCard(ECardRank.Two, ECardSuit.Clubs, veiled: true));
                hand.AddCard(new FakeCard(ECardRank.Four, ECardSuit.Clubs, veiled: true));

                Assert.AreEqual(exTotal, total.Text);
            } // end Labels2Veiled1()

            [TestMethod]
            [DataRow("18?", "seven_of_clubs", "nine_of_diamonds", "two_of_clubs")]
            public void Labels2Vieled2(string exTotal, params string[] args)
            {
                IList<ICard> cardArgs = args.Select((x) => SCardTypes.REGISTER[x].Instantiate()).ToList();

                IHand hand = new RHand(cardArgs);
                ILabel total = new FakeLabel();
                IHandAppraisalDisplay had = new RHandAppraisalDisplay(total);
                Assert.AreEqual("N/A", total.Text);

                had.Hand = hand;
                had.Appraiser = new RStandardAppraiser();

                hand.AddCard(new FakeCard(ECardRank.Two, ECardSuit.Clubs, veiled: true));
                hand.AddCard(new FakeCard(ECardRank.Four, ECardSuit.Clubs, veiled: true));

                Assert.AreEqual(exTotal, total.Text);
            } // end Labels2Veiled2()

            [TestMethod]
            [DataRow("16?", "18", "seven_of_clubs", "nine_of_diamonds")]
            public void LabelsUnvieling(string exTotal1, string exTotal2, params string[] args)
            {
                IList<ICard> cardArgs = args.Select((x) => SCardTypes.REGISTER[x].Instantiate()).ToList();

                IHand hand = new RHand(cardArgs);
                ILabel total = new FakeLabel();
                IHandAppraisalDisplay had = new RHandAppraisalDisplay(total);
                Assert.AreEqual("N/A", total.Text);

                had.Appraiser = new RStandardAppraiser();
                had.Hand = hand;

                ICard v = new FakeCard(ECardRank.Two, ECardSuit.Clubs, veiled: true);
                hand.AddCard(v);

                Assert.AreEqual(exTotal1, total.Text);

                v.Veiled = false;
                Assert.AreEqual(exTotal2, total.Text);
            } // end Labels2Veiled()

            // test more so unsubscription cases.

        } // end inner class Valid
    } // end class
} // end namespace
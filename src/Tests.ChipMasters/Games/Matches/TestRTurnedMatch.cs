using ChipMasters;
using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Matches;
using Fakes.ChipMasters.Cards;

namespace Tests.ChipMasters.Games.Matches
{
    public static class TestRTurnedMatch
    {
        [TestClass]
        public class Valid
        {
            /*            [TestMethod]
                        [DataRow(EHandState.Neutral, 0, false)]
                        [DataRow(EHandState.Neutral, 11, false, 11, "ace_of_spades", false)]
                        [DataRow(EHandState.Neutral, 12, false, 1, 11, "ace_of_spades", false, "ace_of_spades", false)]
                        [DataRow(EHandState.Blackjack, 21, false, 10, 11, "queen_of_hearts", false, "ace_of_spades", false)]
                        [DataRow(EHandState.Blackjack, 21, false, 10, 10, 1, "queen_of_hearts", false, "queen_of_hearts", false, "ace_of_spades", false)]
                        [DataRow(EHandState.Neutral, 13, false, 2, 1, 9, 1, "two_of_clubs", false, "ace_of_diamonds", false, "nine_of_clubs", false, "ace_of_spades", false)]
                        [DataRow(EHandState.Bust, 24, false, 7, 9, 8, "seven_of_clubs", false, "nine_of_diamonds", false, "eight_of_clubs", false)]

                        [DataRow(EHandState.Neutral, 0, true)]
                        [DataRow(EHandState.Neutral, 11, true, 11, "ace_of_spades", false)]
                        [DataRow(EHandState.Neutral, 12, true, 1, 11, "ace_of_spades", false, "ace_of_spades", false)]
                        [DataRow(EHandState.Blackjack, 21, true, 10, 11, "queen_of_hearts", false, "ace_of_spades", false)]
                        [DataRow(EHandState.Blackjack, 21, true, 10, 10, 1, "queen_of_hearts", false, "queen_of_hearts", false, "ace_of_spades", false)]
                        [DataRow(EHandState.Neutral, 13, true, 2, 1, 9, 1, "two_of_clubs", false, "ace_of_diamonds", false, "nine_of_clubs", false, "ace_of_spades", false)]
                        [DataRow(EHandState.Bust, 24, true, 7, 9, 8, "seven_of_clubs", false, "nine_of_diamonds", false, "eight_of_clubs", false)]

                        [DataRow(EHandState.Unknown, 17, false, 9, 8, "seven_of_clubs", true, "nine_of_diamonds", false, "eight_of_clubs", false)]
                        [DataRow(EHandState.Bust, 24, true, 7, 9, 8, "seven_of_clubs", true, "nine_of_diamonds", false, "eight_of_clubs", false)]
                        public void AppraiseHand(int bet, bool exConcluded, params object[] args)
                        {
                            IList<ICard> cardArgs = new List<ICard>();
                            IList<int> valArgs = new List<int>();

                            for (int i = 0; i < args.Length; i++)
                            {
                                object arg = args[i];
                                if (arg is string s)
                                {
                                    ICard c = SCardTypes.REGISTER[s].Instantiate();
                                    c.Veiled = (bool)args[++i];
                                    cardArgs.Add(c);
                                }
                                else if (arg is int j)
                                    valArgs.Add(j);
                                else
                                    throw new InvalidOperationException();
                            }*/


            [TestMethod]
            public void Hit()
            {
                ICard c1 = new FakeCard(rank: ECardRank.Ace);
                ICard c2 = new FakeCard(rank: ECardRank.Two);
                ICard c3 = new FakeCard(rank: ECardRank.Three);
                ICard c4 = new FakeCard(rank: ECardRank.Four);

                ICard c5 = new FakeCard(rank: ECardRank.Ace, suit: ECardSuit.Clubs);
                ICard c6 = new FakeCard(rank: ECardRank.Ace, suit: ECardSuit.Diamonds);
                IDeck newFakeDeck = new FakeDeck(c1, c2, c3, c4, c5, c6);

                IMatch m = new RTurnedMatch(newFakeDeck, newFakeDeck, 1);

                Assert.AreEqual(2, m.PlayerHand.Count);
                Assert.AreEqual(2, m.DealerHand.Count);



                Assert.AreEqual(c1, m.PlayerHand[0]);
                Assert.IsFalse(m.PlayerHand[0].Veiled);

                Assert.AreEqual(c2, m.DealerHand[0]);
                Assert.IsFalse(m.DealerHand[0].Veiled);

                Assert.AreEqual(c3, m.PlayerHand[1]);
                Assert.IsFalse(m.PlayerHand[1].Veiled);

                Assert.AreEqual(c4, m.DealerHand[1]);
                Assert.IsTrue(m.DealerHand[1].Veiled);

                Assert.IsFalse(m.IsConcluded);

                // 14 player
                // 6 dealer

                m.Hit();

                Assert.AreEqual(3, m.PlayerHand.Count);
                Assert.AreEqual(3, m.DealerHand.Count);

                Assert.AreEqual(m.PlayerHand[2], c5);
                Assert.IsFalse(m.PlayerHand[2].Veiled);

                Assert.IsFalse(m.DealerHand[1].Veiled); // dealer card unveiled

                Assert.AreEqual(m.DealerHand[2], c6);
                Assert.IsFalse(m.DealerHand[2].Veiled);

                Assert.IsFalse(m.IsConcluded);
            } // end Deal()

            [TestMethod]
            public void Stand()
            {
                ICard c1 = new FakeCard(rank: ECardRank.Ace);
                ICard c2 = new FakeCard(rank: ECardRank.Two);
                ICard c3 = new FakeCard(rank: ECardRank.Three);
                ICard c4 = new FakeCard(rank: ECardRank.Four);

                ICard c5 = new FakeCard(rank: ECardRank.Four, suit: ECardSuit.Clubs);
                ICard c6 = new FakeCard(rank: ECardRank.Four, suit: ECardSuit.Diamonds);
                IDeck newFakeDeck = new FakeDeck(c1, c2, c3, c4, c5, c6);


                IMatch m = new RTurnedMatch(
                    newFakeDeck,
                    newFakeDeck,
                    1,
                    dealerGoal: 12);

                Assert.AreEqual(2, m.PlayerHand.Count);
                Assert.AreEqual(2, m.DealerHand.Count);



                Assert.AreEqual(c1, m.PlayerHand[0]);
                Assert.IsFalse(m.PlayerHand[0].Veiled);

                Assert.AreEqual(c2, m.DealerHand[0]);
                Assert.IsFalse(m.DealerHand[0].Veiled);

                Assert.AreEqual(c3, m.PlayerHand[1]);
                Assert.IsFalse(m.PlayerHand[1].Veiled);

                Assert.AreEqual(c4, m.DealerHand[1]);
                Assert.IsTrue(m.DealerHand[1].Veiled);

                Assert.IsFalse(m.IsConcluded);

                // 14 player
                // 6 dealer

                m.Stand();

                Assert.AreEqual(2, m.PlayerHand.Count);
                Assert.AreEqual(4, m.DealerHand.Count);

                Assert.IsFalse(m.DealerHand[1].Veiled); // dealer card unveiled

                Assert.AreEqual(m.DealerHand[2], c5);
                Assert.IsFalse(m.DealerHand[2].Veiled);

                Assert.AreEqual(m.DealerHand[3], c6);
                Assert.IsFalse(m.DealerHand[3].Veiled);

                Assert.IsTrue(m.IsConcluded);
            } // end Stand()

            [TestMethod]
            public void DealerStand()
            {
                ICard c1 = new FakeCard(rank: ECardRank.Ace);
                ICard c2 = new FakeCard(rank: ECardRank.Two);
                ICard c3 = new FakeCard(rank: ECardRank.Three);
                ICard c4 = new FakeCard(rank: ECardRank.Four);

                ICard c5 = new FakeCard(rank: ECardRank.Four, suit: ECardSuit.Clubs);
                ICard c6 = new FakeCard(rank: ECardRank.Four, suit: ECardSuit.Diamonds);
                IDeck newFakeDeck = new FakeDeck(c1, c2, c3, c4, c5, c6);


                IMatch m = new RTurnedMatch(
                    newFakeDeck,
                    newFakeDeck,
                    1,
                    dealerGoal: -44);

                Assert.AreEqual(2, m.PlayerHand.Count);
                Assert.AreEqual(2, m.DealerHand.Count);



                Assert.AreEqual(c1, m.PlayerHand[0]);
                Assert.IsFalse(m.PlayerHand[0].Veiled);

                Assert.AreEqual(c2, m.DealerHand[0]);
                Assert.IsFalse(m.DealerHand[0].Veiled);

                Assert.AreEqual(c3, m.PlayerHand[1]);
                Assert.IsFalse(m.PlayerHand[1].Veiled);

                Assert.AreEqual(c4, m.DealerHand[1]);
                Assert.IsTrue(m.DealerHand[1].Veiled);

                Assert.IsFalse(m.IsConcluded);

                // 14 player
                // 6 dealer

                m.Hit();

                Assert.AreEqual(3, m.PlayerHand.Count);
                Assert.AreEqual(2, m.DealerHand.Count); // dealer has stood

                Assert.IsFalse(m.DealerHand[1].Veiled); // dealer card unveiled

                m.Hit();

                Assert.AreEqual(4, m.PlayerHand.Count);
                Assert.AreEqual(2, m.DealerHand.Count);

                Assert.AreEqual(m.PlayerHand[2], c5);
                Assert.IsFalse(m.PlayerHand[2].Veiled);

                Assert.AreEqual(m.PlayerHand[3], c6);
                Assert.IsFalse(m.PlayerHand[3].Veiled);

                Assert.IsFalse(m.IsConcluded);

                m.Stand();

                Assert.IsTrue(m.IsConcluded);
            } // end Stand()

            [TestMethod]
            public void DealerStopsWhenPlayerLoses()
            {
                IMatch m = new RTurnedMatch(
                    playerDeck: new FakeDeck(
                         new FakeCard(rank: ECardRank.Ten),
                          new FakeCard(rank: ECardRank.Ten),
                           new FakeCard(rank: ECardRank.Six),
                            new FakeCard(rank: ECardRank.Ten)),
                    dealerDeck: new FakeDeck(
                         new FakeCard(rank: ECardRank.Ten),
                          new FakeCard(rank: ECardRank.Ten),
                           new FakeCard(rank: ECardRank.Six),
                            new FakeCard(rank: ECardRank.Ten)),
                    31,
                    dealerGoal: 31,
                    appraiser: new RTotalValueAppraiser(31));

                Assert.AreEqual(2, m.PlayerHand.Count);
                Assert.AreEqual(2, m.DealerHand.Count);
                Assert.IsFalse(m.IsConcluded);

                // 20 vs 20
                m.Hit();

                Assert.AreEqual(3, m.PlayerHand.Count);
                Assert.AreEqual(3, m.DealerHand.Count);
                Assert.IsFalse(m.IsConcluded);

                // 26 vs 26
                m.Hit();

                Assert.AreEqual(4, m.PlayerHand.Count); // 36(bust)
                Assert.AreEqual(3, m.DealerHand.Count); // 26
                Assert.IsTrue(m.IsConcluded);
            } // end Stand()

            [TestMethod]
            public void DealerStopsWhenPlayerWins()
            {
                IMatch m = new RTurnedMatch(
                    playerDeck: new FakeDeck(
                         new FakeCard(rank: ECardRank.Ten),
                          new FakeCard(rank: ECardRank.Ten),
                           new FakeCard(rank: ECardRank.Six),
                            new FakeCard(rank: ECardRank.Five)),
                    dealerDeck: new FakeDeck(
                         new FakeCard(rank: ECardRank.Ten),
                          new FakeCard(rank: ECardRank.Ten),
                           new FakeCard(rank: ECardRank.Six),
                            new FakeCard(rank: ECardRank.Five)),
                    31,
                    dealerGoal: 31,
                    appraiser: new RTotalValueAppraiser(31));

                Assert.AreEqual(2, m.PlayerHand.Count);
                Assert.AreEqual(2, m.DealerHand.Count);
                Assert.IsFalse(m.IsConcluded);

                // 20 vs 20
                m.Hit();

                Assert.AreEqual(3, m.PlayerHand.Count);
                Assert.AreEqual(3, m.DealerHand.Count);
                Assert.IsFalse(m.IsConcluded);

                // 26 vs 26
                m.Hit();

                Assert.AreEqual(4, m.PlayerHand.Count); // 31(blackjack)
                Assert.AreEqual(3, m.DealerHand.Count); // 26
                Assert.IsTrue(m.IsConcluded);
            } // end Stand()

        } // end Valid()

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void CtorNegativeBet()
            {
                IDeck newFakeDeck = new FakeDeck();
                new RTurnedMatch(newFakeDeck, newFakeDeck, -1);
            } // end CtorNegativeBet()

            [TestMethod]
            [ExpectedException(typeof(RGameOverException))]
            public void Hit()
            {
                IDeck newFakeDeck = new FakeDeck(
                        new FakeCard(rank: ECardRank.Queen),
                        new FakeCard(rank: ECardRank.Ace),
                        new FakeCard(rank: ECardRank.Ace),
                        new FakeCard(rank: ECardRank.Queen));
                IMatch m = new RTurnedMatch(
                    newFakeDeck,
                    newFakeDeck,
                    1);

                Assert.IsTrue(m.IsConcluded);
                m.Hit();
            } // end Hit()

            [TestMethod]
            [ExpectedException(typeof(RGameOverException))]
            public void Stand()
            {
                IDeck newFakeDeck = new FakeDeck(
                        new FakeCard(rank: ECardRank.Queen),
                        new FakeCard(rank: ECardRank.Ace),
                        new FakeCard(rank: ECardRank.Ace),
                        new FakeCard(rank: ECardRank.Queen));
                IMatch m = new RTurnedMatch(
                    newFakeDeck,
                    newFakeDeck,
                    0);

                Assert.IsTrue(m.IsConcluded);
                m.Stand();
            } // end Stand()
        } // end Invalid()
    } // end class
} // end namespace
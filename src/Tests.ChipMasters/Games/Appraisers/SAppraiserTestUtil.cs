using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;
using ChipMasters.Registers;

namespace Tests.ChipMasters.Games.Appraisers
{
    public static class SAppraiserTestUtil
    {
        /// <summary>
        /// Test an <paramref name="appraiser"/>s <see cref="IAppraiser.AppraiseHand(IHand, bool)"/> behavior.
        /// </summary>
        /// <param name="appraiser"></param>
        /// <param name="exState"></param>
        /// <param name="exTotalValue"></param>
        /// <param name="includeHidden"></param>
        /// <param name="args">Alternating strings and bools representing card ids and if the card is veiled.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void TestAppraiseHand(IAppraiser appraiser, EHandState exState, int exTotalValue, bool includeHidden, params object[] args)
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
            }
            IHand hand = new RHand(cardArgs);

            VHandAppraisal appraisal = appraiser.AppraiseHand(hand, includeHidden);

            Assert.AreEqual(exState, appraisal.State);
            Assert.AreEqual(exTotalValue, appraisal.TotalValue);
            Assert.AreEqual(valArgs.Count, appraisal.Values.Count);
            for (int i = 0; i < valArgs.Count; i++)
                Assert.AreEqual(valArgs[i], appraisal.Values[i]);
        } // end AppraiseHand()
    } // end class
} // end namespace
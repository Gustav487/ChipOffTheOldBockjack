using ChipMasters.Games.Appraisers;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace Tests.ChipMasters.Games.Appraisers
{
    public static class TestVHandAppraisal
    {
        public static class ENDEC
        {
            [TestClass]
            public class Valid
            {
                [TestMethod]
                [DataRow("{\"values\":[],\"state\":\"Neutral\"}", EHandState.Neutral)]
                [DataRow("{\"values\":[8,10],\"state\":\"Neutral\"}", EHandState.Neutral, 8, 10)]
                [DataRow("{\"values\":[8,10,102],\"state\":\"Neutral\"}", EHandState.Neutral, 8, 10, 102)]
                [DataRow("{\"values\":[9],\"state\":\"Bust\"}", EHandState.Bust, 9)]
                [DataRow("{\"values\":[],\"state\":\"Unknown\"}", EHandState.Unknown)]
                [DataRow("{\"values\":[1,2,3],\"state\":\"Unknown\"}", EHandState.Unknown, 1, 2, 3)]
                public void Encode(string exJson, EHandState state, params int[] values)
                {
                    JsonElement stream = VHandAppraisal.ENDEC.Encode(
                        JsonStreamCoder.INSTANCE,
                        new VHandAppraisal(values, state),
                        CoderSettings.DEFAULT);
                    Assert.AreEqual(exJson, stream.ToString(JsonFormatting.COMPRESSED));
                } // end Encode()

                [TestMethod]
                [DataRow("{\"values\":[],\"state\":\"Neutral\"}", EHandState.Neutral)]
                [DataRow("{\"values\":[8,10],\"state\":\"Neutral\"}", EHandState.Neutral, 8, 10)]
                [DataRow("{\"values\":[8,10,102],\"state\":\"Neutral\"}", EHandState.Neutral, 8, 10, 102)]
                [DataRow("{\"values\":[9],\"state\":\"Bust\"}", EHandState.Bust, 9)]
                [DataRow("{\"values\":[],\"state\":\"Unknown\"}", EHandState.Unknown)]
                [DataRow("{\"values\":[1, 2, 3],\"state\":\"Unknown\"}", EHandState.Unknown, 1, 2, 3)]
                public void Decode(string stream, EHandState exState, params int[] exValues)
                {
                    VHandAppraisal data = VHandAppraisal.ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(stream),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(exState, data.State);
                    Assert.AreEqual(exValues.Length, data.Values.Count);
                    for (int i = 0; i < exValues.Length; i++)
                        Assert.AreEqual(exValues[i], data.Values[i]);
                } // end Decode()
            } // end inner inner class Valid
        } // end inner class ENDEC

        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(false, 1, EHandState.Neutral, EHandState.Blackjack, 0, 21)]
            [DataRow(false, 1, EHandState.Bust, EHandState.Blackjack, 70, 21)]
            [DataRow(false, 1, EHandState.Bust, EHandState.Neutral, 6, 2)]
            [DataRow(true, 1, EHandState.Bust, EHandState.Bust, 0, 21)]
            [DataRow(true, 1, EHandState.Blackjack, EHandState.Blackjack, 21, 21)]
            [DataRow(true, 2, EHandState.Blackjack, EHandState.Blackjack, 10, 11, 11, 10)]
            [DataRow(true, 2, EHandState.Neutral, EHandState.Neutral, 10, 11, 11, 10)]
            [DataRow(false, 2, EHandState.Neutral, EHandState.Neutral, 9, 11, 11, 10)]

            [DataRow(false, 1, EHandState.Unknown, EHandState.Blackjack, 70, 21)]
            [DataRow(false, 1, EHandState.Bust, EHandState.Unknown, 70, 21)]
            [DataRow(false, 2, EHandState.Unknown, EHandState.Neutral, 10, 11, 11, 10)]
            [DataRow(false, 3, EHandState.Unknown, EHandState.Unknown, 10, 11, 2, 2, 11, 10)]
            public void Equals(bool exEq, int cutoff, EHandState stateA, EHandState stateB, params int[] values)
            {
                VHandAppraisal a = new(values[0..cutoff], stateA);
                VHandAppraisal b = new(values[cutoff..^0], stateB);

                Assert.AreEqual(exEq, a.Equals(b));
                Assert.AreEqual(exEq, a == b);
                Assert.AreEqual(!exEq, a != b);
            } // end Equals()

            [TestMethod]
            [DataRow(false, 1, EHandState.Blackjack, EHandState.Blackjack, 0, 21)] // equals
            [DataRow(false, 1, EHandState.Blackjack, EHandState.Blackjack, 21, 21)]
            [DataRow(false, 2, EHandState.Bust, EHandState.Bust, 10, 11, 11, 10)]
            [DataRow(false, 2, EHandState.Neutral, EHandState.Neutral, 10, 11, 11, 10)]

            [DataRow(true, 2, EHandState.Blackjack, EHandState.Neutral, 10, 11, 11, 10)] // left hand wins
            [DataRow(true, 2, EHandState.Neutral, EHandState.Neutral, 19, 11, 11, 10)]
            [DataRow(true, 2, EHandState.Neutral, EHandState.Bust, 19, 11, 11, 10)]
            [DataRow(true, 2, EHandState.Neutral, EHandState.Bust, 1, 11, 11, 10)]

            [DataRow(false, 1, EHandState.Bust, EHandState.Blackjack, 70, 21)] // right hand wins
            [DataRow(false, 1, EHandState.Bust, EHandState.Neutral, 6, 2)]
            [DataRow(false, 1, EHandState.Neutral, EHandState.Blackjack, 0, 21)]
            [DataRow(false, 2, EHandState.Neutral, EHandState.Neutral, 9, 11, 11, 10)]

            [DataRow(false, 2, EHandState.Unknown, EHandState.Neutral, 9, 11, 11, 10)] // winner is unknown
            [DataRow(false, 2, EHandState.Unknown, EHandState.Unknown, 9, 11, 11, 10)]
            [DataRow(false, 2, EHandState.Unknown, EHandState.Unknown, 9, 11, 11, 10)]
            [DataRow(false, 2, EHandState.Bust, EHandState.Unknown, 9, 11, 11, 10)]
            public void GreaterThan(bool exEq, int cutoff, EHandState stateA, EHandState stateB, params int[] values)
            {
                VHandAppraisal a = new(values[0..cutoff], stateA);
                VHandAppraisal b = new(values[cutoff..^0], stateB);

                Assert.AreEqual(exEq, a > b);
            } // end GreaterThan()

            [TestMethod]
            [DataRow(false, 1, EHandState.Blackjack, EHandState.Blackjack, 0, 21)] // equals
            [DataRow(false, 1, EHandState.Blackjack, EHandState.Blackjack, 21, 21)]
            [DataRow(false, 2, EHandState.Bust, EHandState.Bust, 10, 11, 11, 10)]
            [DataRow(false, 2, EHandState.Neutral, EHandState.Neutral, 10, 11, 11, 10)]

            [DataRow(false, 2, EHandState.Blackjack, EHandState.Neutral, 10, 11, 11, 10)] // left hand wins
            [DataRow(false, 2, EHandState.Neutral, EHandState.Neutral, 19, 11, 11, 10)]
            [DataRow(false, 2, EHandState.Neutral, EHandState.Bust, 19, 11, 11, 10)]
            [DataRow(false, 2, EHandState.Neutral, EHandState.Bust, 1, 11, 11, 10)]

            [DataRow(true, 1, EHandState.Bust, EHandState.Blackjack, 70, 21)] // right hand wins
            [DataRow(true, 1, EHandState.Bust, EHandState.Neutral, 6, 2)]
            [DataRow(true, 1, EHandState.Neutral, EHandState.Blackjack, 0, 21)]
            [DataRow(true, 2, EHandState.Neutral, EHandState.Neutral, 9, 11, 11, 10)]

            [DataRow(false, 2, EHandState.Unknown, EHandState.Neutral, 9, 11, 11, 10)] // winner is unknown
            [DataRow(false, 2, EHandState.Unknown, EHandState.Unknown, 9, 11, 11, 10)]
            [DataRow(false, 2, EHandState.Unknown, EHandState.Unknown, 9, 11, 11, 10)]
            [DataRow(false, 2, EHandState.Bust, EHandState.Unknown, 9, 11, 11, 10)]
            public void LessThan(bool exEq, int cutoff, EHandState stateA, EHandState stateB, params int[] values)
            {
                VHandAppraisal a = new(values[0..cutoff], stateA);
                VHandAppraisal b = new(values[cutoff..^0], stateB);

                Assert.AreEqual(exEq, a < b);
            } // end LessThan()

        } // end inner class Valid
    } // end class
} // end namespace
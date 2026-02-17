using ChipMasters.Games.Appraisers;
using ChipMasters.User;
using Fakes.ChipMasters.Games.Appraisers;
using Fakes.ChipMasters.Games.Matches;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace Tests.ChipMasters.User
{
    public static class TestVMatchRecord
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(8, 9, EHandState.Blackjack, 2342, EHandState.Neutral)]
            public void Ctor1(int bet, int pV, EHandState pS, int dV, EHandState dS)
            {
                VHandAppraisal pAp = new(new int[] { pV }, pS);
                VHandAppraisal dAp = new(new int[] { dV }, dS);

                FakeMatch m = new(bet: bet, appraiser: new FakeOrdinalAppraiser(pAp, dAp));

                VMatchRecord mr = new(m);

                Assert.AreEqual(bet, mr.Bet);
                Assert.AreEqual(pV, mr.PlayerResult.TotalValue);
                Assert.AreEqual(pS, mr.PlayerResult.State);
                Assert.AreEqual(dV, mr.DealerResult.TotalValue);
                Assert.AreEqual(dS, mr.DealerResult.State);
            } // end Ctor1()

            [TestMethod]
            [DataRow(8, 9, EHandState.Blackjack, 2342, EHandState.Neutral)]
            public void Ctor2(int bet, int pV, EHandState pS, int dV, EHandState dS)
            {
                VHandAppraisal pAp = new(new int[] { pV }, pS);
                VHandAppraisal dAp = new(new int[] { dV }, dS);

                VMatchRecord mr = new(bet, pAp, dAp);

                Assert.AreEqual(bet, mr.Bet);
                Assert.AreEqual(pV, mr.PlayerResult.TotalValue);
                Assert.AreEqual(pS, mr.PlayerResult.State);
                Assert.AreEqual(dV, mr.DealerResult.TotalValue);
                Assert.AreEqual(dS, mr.DealerResult.State);
            } // end Ctor2()
        } // end TestVMatchRecord.Valid()

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            public void Ctor0()
            {
                new VMatchRecord();
            } // end Ctor0()

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            [DataRow(8, 9, EHandState.Unknown, 2342, EHandState.Neutral)]
            [DataRow(8, 9, EHandState.Unknown, 2342, EHandState.Unknown)]
            [DataRow(8, -232, EHandState.Bust, 0, EHandState.Unknown)]
            public void Ctor1(int bet, int pV, EHandState pS, int dV, EHandState dS)
            {
                VHandAppraisal pAp = new(new int[] { pV }, pS);
                VHandAppraisal dAp = new(new int[] { dV }, dS);

                FakeMatch m = new(bet: bet, appraiser: new FakeOrdinalAppraiser(pAp, dAp));

                new VMatchRecord(m);
            } // end Ctor1()
        } // end TestVMatchRecord.Invalid()

        public static class ENDEC
        {
            [TestClass]
            public class Valid
            {

                [TestMethod]
                public void Encode()
                {
                    VHandAppraisal pAp = new(new int[] { 10, 10 }, EHandState.Neutral);
                    VHandAppraisal dAp = new(new int[] { 7, 6, 9 }, EHandState.Bust);

                    JsonElement stream = VMatchRecord.ENDEC.Encode(
                        JsonStreamCoder.INSTANCE,
                        new VMatchRecord(25, pAp, dAp),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual("{\"bet\":25,\"player_result\":{\"values\":[10,10],\"state\":\"Neutral\"},\"dealer_result\":{\"values\":[7,6,9],\"state\":\"Bust\"}}",
                        stream.ToString(JsonFormatting.COMPRESSED));
                } // end Encode()

                [TestMethod]
                [DataRow("{\"bet\":25,\"player_result\":{\"values\":[10,10],\"state\":\"Neutral\"},\"dealer_result\":{\"values\":[],\"state\":\"Bust\"}}",
                    25, 20, EHandState.Neutral, 0, EHandState.Bust)]
                [DataRow("{\"bet\":0,\"player_result\":{\"values\":[10,10],\"state\":\"Neutral\"},\"dealer_result\":{\"values\":[103],\"state\":\"Blackjack\"}}",
                    0, 20, EHandState.Neutral, 103, EHandState.Blackjack)]
                public void Decode(string json, int exBet, int exPV, EHandState exPS, int exDV, EHandState exDS)
                {
                    VMatchRecord vmr = VMatchRecord.ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(json),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(exBet, vmr.Bet);
                    Assert.AreEqual(exPV, vmr.PlayerResult.TotalValue);
                    Assert.AreEqual(exPS, vmr.PlayerResult.State);
                    Assert.AreEqual(exDV, vmr.DealerResult.TotalValue);
                    Assert.AreEqual(exDS, vmr.DealerResult.State);
                } // end Decode()
            } // end inner class TestVMatchRecord.ENDEC.Valid

            [TestClass]
            public class Invalid
            {
                [TestMethod]
                [ExpectedException(typeof(InvalidOperationException))]
                [DataRow("{\"bet\":25,\"player_result\":{\"values\":[10,10],\"state\":\"Neutral\"},\"dealer_result\":{\"values\":[],\"state\":\"Unknown\"}}")]
                [DataRow("{\"bet\":0,\"player_result\":{\"values\":[10,10],\"state\":\"Unknown\"},\"dealer_result\":{\"values\":[103],\"state\":\"Blackjack\"}}")]
                public void Decode(string json)
                {
                    VMatchRecord.ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(json),
                        CoderSettings.DEFAULT);
                } // end Decode()
            } // end inner class TestVMatchRecord.ENDEC.Invalid
        } // end inner class ENDEC
    } // end class
} // end namespace
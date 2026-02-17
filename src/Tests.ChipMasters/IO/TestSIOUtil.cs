using ChipMasters.IO;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;

namespace Tests.ChipMasters.IO
{
    public static class TestSIOUtil
    {
        public class DATE_TIME_ENDEC
        {
            [TestClass]
            public class Valid
            {
                [TestMethod]
                public void EncodeDecode()
                {
                    DateTime dt = DateTime.Now;

                    DateTime ed = SIOUtil.DATE_TIME_ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        SIOUtil.DATE_TIME_ENDEC.Encode(
                            JsonStreamCoder.INSTANCE,
                            dt,
                            CoderSettings.DEFAULT),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(dt.Year, ed.Year);
                    Assert.AreEqual(dt.Month, ed.Month);
                    Assert.AreEqual(dt.Day, ed.Day);
                    Assert.AreEqual(dt.Hour, ed.Hour);
                    Assert.AreEqual(dt.Minute, ed.Minute);
                    Assert.AreEqual(dt.Second, ed.Second);
                } // end EncodeDecode
            } // end class TestSIOUtil.DATE_TIME_ENDEC.Valid
        } // end class TestSIOUtil.DATE_TIME_ENDEC
    } // end class
} // end namespace
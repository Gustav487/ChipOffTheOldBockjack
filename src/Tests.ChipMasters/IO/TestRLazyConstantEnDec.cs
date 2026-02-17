using ChipMasters.IO;
using Fakes.ChipMasters.IO;
using GSR.EnDecic;

namespace Tests.ChipMasters.IO
{
    public class TestRLazyConstantEnDec
    {
        [TestClass]
        public class Valid
        {
            private const string _constant = "Test123";

            [TestMethod]
            public void Encode()
            {
                var endec = new RLazyConstantEnDec<string>(new Lazy<string>(_constant));
                var encode = endec.Encode(new FakeStreamEncoder(), _constant, new CoderSettings());
                Assert.AreEqual("encoded", encode);
            } // end Encode()

            [TestMethod]
            public void Decode()
            {
                var endec = new RLazyConstantEnDec<string>(() => _constant);
                var decode = endec.Decode(new FakeStreamDecoder(), _constant, new CoderSettings());
                Assert.AreEqual(_constant, decode);
            } // end Decode()
        } // end inner class Valid
    } // end class
} // end namespace

using ChipMasters.IO;
using Fakes.ChipMasters.IO;
using GSR.Utilic;

namespace Tests.ChipMasters.IO
{
    public class TestRType
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void Ctor_Minimal()
            {
                var enc = new FakeEnDec<string>();
                var type = new RType<string>(enc, s => s == "valid");

                Assert.ThrowsException<UnexpectedStateException>(() => type.Instantiate());
                Assert.IsFalse(type.IsDefault("valid"));
                Assert.IsTrue(type.IsTypeOf("valid"));
                Assert.IsFalse(type.IsTypeOf("invalid"));
            } // end Ctor_Minimal()

            [TestMethod]
            public void Ctor_Full()
            {
                var enc = new FakeEnDec<int>();
                var type = new RType<int>(
                    enc,
                    instantiate: () => 42,
                    isDefault: x => x == 0,
                    isTypeOf: x => x == 5);

                Assert.AreEqual(42, type.Instantiate());

                Assert.IsTrue(type.IsDefault(0));
                Assert.IsFalse(type.IsDefault(99));

                Assert.IsTrue(type.IsTypeOf(5));
                Assert.IsFalse(type.IsTypeOf(-1));
            } // end Ctor_Full()

            [TestMethod]
            public void Ctor_ThrowsNullArguments()
            {
                var enc = new FakeEnDec<int>();
                Assert.ThrowsException<ArgumentNullException>(() => new RType<int>(null!, x => true));
                Assert.ThrowsException<ArgumentNullException>(() => new RType<int>(enc, null!, x => false, x => true));
                Assert.ThrowsException<ArgumentNullException>(() => new RType<int>(enc, () => 1, null!, x => true));
                Assert.ThrowsException<ArgumentNullException>(() => new RType<int>(enc, () => 1, x => false, null!));
            } // end Ctor_ThrowsNullArguments()
        } // end inner class Valid
    } // end class
} // end namespace

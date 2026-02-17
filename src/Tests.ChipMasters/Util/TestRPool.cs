using ChipMasters.Util;

namespace Tests.ChipMasters.Util
{
    public static class TestRPool
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void Get()
            {
                IOException a = new();
                IOException b = new();
                Stack<IOException> instantiator = new();
                instantiator.Push(b);
                instantiator.Push(a);

                IPool<IOException> pool = new RPool<IOException>(() => instantiator.Pop());

                Assert.AreEqual(a, pool.Get());
                Assert.AreEqual(b, pool.Get());
            } // end Get()

            [TestMethod]
            public void Release()
            {
                IOException a = new();
                IOException b = new();
                Stack<IOException> instantiator = new();
                instantiator.Push(b);
                instantiator.Push(a);

                IPool<IOException> pool = new RPool<IOException>(() => instantiator.Pop());

                Assert.AreEqual(a, pool.Get());
                pool.Release(a);
                Assert.AreEqual(a, pool.Get());
                Assert.AreEqual(b, pool.Get());
            } // end Release()
        } // end inner class Valid
    } // end class
} // end namespace
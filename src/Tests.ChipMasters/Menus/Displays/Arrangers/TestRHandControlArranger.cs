using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Arrangers;
using Fakes.ChipMasters.GodotWrappers;
using System.Numerics;
using Tests.ChipMasters.Util;

namespace Tests.ChipMasters.Menus.Displays.Arrangers
{
    public static class TestRHandControlArranger
    {
        [TestClass]
        public class Valid
        {
            public const float EPSILON = 1e-5f; // different between two floats within which they're considered equal.



            [TestMethod]
            [DataRow(0f, 0f, 1.1f, 1f, 1f, 0f)]
            [DataRow(0f, 0f, 1.1f, 1f, 1f, 20f)]
            [DataRow(.1f, .1f, 1.1f, 1f, 1f, 1f)]
            public void Arrange0(
                float dOffsX, float dOffsY,
                float dWidth,
                float cSizeX, float cSizeY,
                float maxSpc)
            {
                IArranger<IControl> ar = new RHandControlArranger(
                    new Vector2(dOffsX, dOffsY),
                    dWidth,
                    new Vector2(cSizeX, cSizeY),
                    maxSpc);

                ar.Arrange(new List<IControl>());
            } // end Arrange0()

            [TestMethod]
            [DataRow(0f, 0f, 1f, 1f, 1f, 0f,
                0f, 1f, .0f, 1f)] // perfectly fits range
            [DataRow(0f, 0f, 1f, 1f, 1f, 0f,
                0f, 1f, .0f, 1f)] // fits inside range
            [DataRow(0f, 0f, 1f, 1f, 1f, 1111f,
                0f, 1f, .0f, 1f)] // fits inside range, max space does not matter
            [DataRow(0f, 0f, 1f, 1f, 1f, -1f,
                0f, 1f, .0f, 1f)]
            public void Arrange1(
                float dOffsX, float dOffsY,
                float dWidth,
                float cSizeX, float cSizeY,
                float maxSpc,
                float exAncT1, float exAncB1, float exAncL1, float exAncR1)
            {
                IArranger<IControl> ar = new RHandControlArranger(
                    new Vector2(dOffsX, dOffsY),
                    dWidth,
                    new Vector2(cSizeX, cSizeY),
                    maxSpc);

                IControl c1 = new FakeControl();
                ar.Arrange(new List<IControl>() { c1 });

                Assert.IsTrue(exAncT1.ApproximatelyEquals(c1.AnchorTop));
                Assert.IsTrue(exAncB1.ApproximatelyEquals(c1.AnchorBottom));
                Assert.IsTrue(exAncL1.ApproximatelyEquals(c1.AnchorLeft));
                Assert.IsTrue(exAncR1.ApproximatelyEquals(c1.AnchorRight));
            } // end Arrange1()

            [TestMethod]
            [DataRow(0f, 0f, 1f, .5f, .5f, 0f,
                0f, .5f, .0f, .5f,
                0f, .5f, .5f, 1f)] // perfectly fits range
            [DataRow(0f, 0f, .7f, .3f, .7f, .05f,
                0f, .7f, .025f, .325f,
                0f, .7f, .375f, .675f)] // fits inside range, maximal space used
            [DataRow(0f, 0f, .7f, .3f, .7f, .15f,
                0f, .7f, .0f, .3f,
                0f, .7f, .4f, .7f)] // fits inside range, maximal space unused
            [DataRow(0f, 0f, .15f, .1f, .7f, .1f,
                0f, .7f, .0f, .1f,
                0f, .7f, .05f, .15f)] // fits inside range, maximal space unused - overlapping
            public void Arrange2(
                float dOffsX, float dOffsY,
                float dWidth,
                float cSizeX, float cSizeY,
                float maxSpc,
                float exAncT1, float exAncB1, float exAncL1, float exAncR1,
                float exAncT2, float exAncB2, float exAncL2, float exAncR2)
            {
                IArranger<IControl> ar = new RHandControlArranger(
                    new Vector2(dOffsX, dOffsY),
                    dWidth,
                    new Vector2(cSizeX, cSizeY),
                    maxSpc);

                IControl c1 = new FakeControl();
                IControl c2 = new FakeControl();
                ar.Arrange(new List<IControl>() { c1, c2 });

                Assert.IsTrue(exAncT1.ApproximatelyEquals(c1.AnchorTop));
                Assert.IsTrue(exAncB1.ApproximatelyEquals(c1.AnchorBottom));
                Assert.IsTrue(exAncL1.ApproximatelyEquals(c1.AnchorLeft));
                Assert.IsTrue(exAncR1.ApproximatelyEquals(c1.AnchorRight));

                Assert.IsTrue(exAncT2.ApproximatelyEquals(c2.AnchorTop));
                Assert.IsTrue(exAncB2.ApproximatelyEquals(c2.AnchorBottom));
                Assert.IsTrue(exAncL2.ApproximatelyEquals(c2.AnchorLeft));
                Assert.IsTrue(exAncR2.ApproximatelyEquals(c2.AnchorRight));
            } // end Arrange2()

            [TestMethod]
            [DataRow(0f, 0f, .9f, .3f, .5f, 0f,
                0f, .5f, .0f, .3f,
                0f, .5f, .3f, .6f,
                0f, .5f, .6f, .9f)] // perfectly fits range
            [DataRow(0f, 0f, 2f, .3f, 1.2f, .05f,
                0f, 1.2f, .5f, .8f,
                0f, 1.2f, .85f, 1.15f,
                0f, 1.2f, 1.2f, 1.5f)] // fits inside range, maximal space used
            [DataRow(0f, 0f, 1f, .3f, .7f, .05f,
                0f, .7f, .0f, .30f,
                0f, .7f, .35f, .65f,
                0f, .7f, .7f, 1f)] // fits inside range, maximal space used - perfectly
            [DataRow(0f, 0f, 1f, .3f, .0f, .01f,
                0f, .0f, .04f, .34f,
                0f, .0f, .35f, .65f,
                0f, .0f, .66f, .96f)] // fits inside range, maximal space unused            
            [DataRow(0f, 0f, .2f, .1f, .8f, .1f,
                0f, .8f, .0f, .1f,
                0f, .8f, .05f, .15f,
                0f, .8f, .1f, .2f)] // fits inside range, maximal space unused - overlapping
            [DataRow(0f, 0f, .57f, .57f, .8f, 06.1f,
                0f, .8f, .0f, .57f,
                0f, .8f, .0f, .57f,
                0f, .8f, .0f, .57f)] // fully same spot
            [DataRow(0f, 1f, .571f, .57f, .8f, 06.1f,
                1f, 1.8f, .0f, .57f,
                1f, 1.8f, .0005f, .5705f,
                1f, 1.8f, .001f, .571f)] // very almost fully same spot
            public void Arrange3(
                float dOffsX, float dOffsY,
                float dWidth,
                float cSizeX, float cSizeY,
                float maxSpc,
                float exAncT1, float exAncB1, float exAncL1, float exAncR1,
                float exAncT2, float exAncB2, float exAncL2, float exAncR2,
                float exAncT3, float exAncB3, float exAncL3, float exAncR3)
            {
                IArranger<IControl> ar = new RHandControlArranger(
                    new Vector2(dOffsX, dOffsY),
                    dWidth,
                    new Vector2(cSizeX, cSizeY),
                    maxSpc);

                IControl c1 = new FakeControl();
                IControl c2 = new FakeControl();
                IControl c3 = new FakeControl();
                ar.Arrange(new List<IControl>() { c1, c2, c3 });

                Assert.IsTrue(exAncT1.ApproximatelyEquals(c1.AnchorTop));
                Assert.IsTrue(exAncB1.ApproximatelyEquals(c1.AnchorBottom));
                Assert.IsTrue(exAncL1.ApproximatelyEquals(c1.AnchorLeft));
                Assert.IsTrue(exAncR1.ApproximatelyEquals(c1.AnchorRight));

                Assert.IsTrue(exAncT2.ApproximatelyEquals(c2.AnchorTop));
                Assert.IsTrue(exAncB2.ApproximatelyEquals(c2.AnchorBottom));
                Assert.IsTrue(exAncL2.ApproximatelyEquals(c2.AnchorLeft));
                Assert.IsTrue(exAncR2.ApproximatelyEquals(c2.AnchorRight));

                Assert.IsTrue(exAncT3.ApproximatelyEquals(c3.AnchorTop));
                Assert.IsTrue(exAncB3.ApproximatelyEquals(c3.AnchorBottom));
                Assert.IsTrue(exAncL3.ApproximatelyEquals(c3.AnchorLeft));
                Assert.IsTrue(exAncR3.ApproximatelyEquals(c3.AnchorRight));
            } // end Arrange3()

#warning negative max spacing.
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            [DataRow(0f, 0f, .1f, 1f, 1f, 0f)] // card size greater than the display width
            public void Ctor(
                float dOffsX, float dOffsY,
                float dWidth,
                float cSizeX, float cSizeY,
                float maxSpc)
            {
                new RHandControlArranger(
                    new Vector2(dOffsX, dOffsY),
                    dWidth,
                    new Vector2(cSizeX, cSizeY),
                    maxSpc);
            } // end Ctor()
        } // end inner class Invalid
    } // end class
} // end namespace
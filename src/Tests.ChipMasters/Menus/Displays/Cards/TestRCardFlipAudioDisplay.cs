using ChipMasters.Menu.Displays.Cards;
using Fakes.ChipMasters.Cards;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.Displays.Cards
{
    [TestClass]
    public class TestRCardFlipAudioDisplay
    {
        [TestMethod]
        [DataRow(false, false, false, false, false)] // outside tree unveil, stay outside tree and unveil
        [DataRow(false, true, false, false, false)] // outside tree veiled, stay out tree and unveil
        [DataRow(false, true, false, true, true)] // outside tree veiled, enter tree unveil
        [DataRow(true, true, true, true, false)] // in tree veiled, exit tree unveil
        [DataRow(true, false, true, false, true)] // outside tree unveiled, stay tree veil
        public void SetAndFlipping(
            bool initInsideTree, bool initVeiled,
            bool exFlip1,
            bool invertTreeState,
            bool exFlip2)
        {

            bool insideTree = initInsideTree;
            FakeAudioStreamPlayer asd = new();
            RCardFlipAudioDisplay cd = new RCardFlipAudioDisplay(asd, () => insideTree);

            FakeCard fc = new(veiled: initVeiled);
            cd.Display = fc;

            Assert.IsFalse(asd.Playing);
            fc.Veiled = !fc.Veiled;
            Assert.AreEqual(exFlip1, asd.Playing);

            asd.Playing = false;

            if (invertTreeState)
            {
                insideTree = !insideTree;
                if (insideTree)
                    cd.EnterTree();
                else
                    cd.ExitTree();
            }

            fc.Veiled = !fc.Veiled;
            Assert.AreEqual(exFlip2, asd.Playing);
        } // SetAndFlipping()

        [TestMethod]
        public void OversetOldStopsTriggering()
        {
            FakeAudioStreamPlayer asd = new();
            RCardFlipAudioDisplay cd = new RCardFlipAudioDisplay(asd, () => true);

            FakeCard fc = new();
            cd.Display = fc;

            Assert.IsFalse(asd.Playing);
            fc.Veiled = !fc.Veiled;
            Assert.IsTrue(asd.Playing); // verify currently responds

            asd.Playing = false;
            fc.Veiled = !fc.Veiled;
            Assert.IsTrue(asd.Playing); // verify continues responding

            FakeCard fc2 = new();
            cd.Display = fc2; // set to new

            asd.Playing = false;
            fc.Veiled = !fc.Veiled;
            Assert.IsFalse(asd.Playing); // verify no longer responds to old card

            asd.Playing = false;
            fc2.Veiled = !fc2.Veiled;
            Assert.IsTrue(asd.Playing); // verify responds to new card
        } // end OversetOldStopsTriggering()

        [TestMethod]
        public void Dispose()
        {
            FakeAudioStreamPlayer asd = new();
            RCardFlipAudioDisplay cd = new RCardFlipAudioDisplay(asd, () => true);

            FakeCard fc = new();
            cd.Display = fc;

            Assert.IsFalse(asd.Playing);
            fc.Veiled = !fc.Veiled;
            Assert.IsTrue(asd.Playing); // verify currently responds

            cd.Dispose(); // Dispose

            asd.Playing = false;
            fc.Veiled = !fc.Veiled;
            Assert.IsFalse(asd.Playing); // verify no longer responds to the card
        } // end Dispose()
    } // end class
} // end namespace
using ChipMasters.Registers;
using ChipMasters.Resources.Animations;
using ChipMasters.Resources.Skins;
using ChipMasters.User;

namespace Tests.ChipMasters.User
{
    public class TestRAssetSelection
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void DefaultAssetsAssignedOnCreation()
            {
                IAssetSelection a = new RAssetSelection();
                Assert.AreEqual(SCardFlipTypes.ONE_EIGHTY, a.CardFlip);
                Assert.AreEqual(SCardSkins.DEFAULT, a.CardSkin);
                Assert.AreEqual(SGameBackgroundSkins.DEFAULT, a.GameBackgroundSkin);
                Assert.AreEqual(SGUISkins.DEFAULT, a.GUISkin);
            } // end DefaultAssetsAssignedOnCreation()

            [TestMethod]
            public void SetAssets()
            {
                ICardFlipAnimType cf = new RCardFlipAnimType();
                ICardSkin cs = new RCardSkins();
                IGUISkin gs = new RGUISkins();
                IGameBackgroundSkin bs = new RGameBackgroundSkin();
                IAssetSelection a = new RAssetSelection
                {
                    CardFlip = cf,
                    CardSkin = cs,
                    GUISkin = gs,
                    GameBackgroundSkin = bs
                };
                Assert.AreEqual(a.CardFlip, cf);
                Assert.AreEqual(a.CardSkin, cs);
                Assert.AreEqual(a.GUISkin, gs);
                Assert.AreEqual(a.GameBackgroundSkin, bs);
            } // end SetAssets()
        } // end inner class Valid
    } // end class
} // end namespace

using ChipMasters.Games.BetHandlers;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.BetSelections;
using Fakes.ChipMasters.GodotWrappers;
using Fakes.ChipMasters.Menus.BetSelections;

namespace Tests.ChipMasters.Menus.BetSelections
{
    [TestClass]
    public sealed class TestRAutobetBetSelectionMenu
    {
        [TestMethod]
        [DataRow(0, 0, 0, 0, 1, 4, 1, 4)]
        [DataRow(20, 320, 20, 320, 0, 0, 0, 0)]
        public void Range(
            int initMin, int initMax,
            int exMin1, int exMax1,
            int setToMin, int setToMax,
            int exMin2, int exMax2)
        {
            VBetRange init = new VBetRange(min: initMin, max: initMax);
            VBetRange ex1 = new VBetRange(min: exMin1, max: exMax1);
            VBetRange setTo = new VBetRange(min: setToMin, max: setToMax);
            VBetRange ex2 = new VBetRange(min: exMin2, max: exMax2);

            IBetSelectionMenu fakeBsm = new FakeBetSelectionMenu() { Range = init };
            ICheckBox cb = new FakeCheckBox();
            IBetSelectionMenu bsm = new RAutobetBetSelectionMenu(fakeBsm, cb);

            Assert.AreEqual(ex1, fakeBsm.Range);
            Assert.AreEqual(ex1, bsm.Range);

            bsm.Range = setTo;

            Assert.AreEqual(ex2, fakeBsm.Range);
            Assert.AreEqual(ex2, bsm.Range);
        } // end Range()

        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(1, 1, 0, 0)]
        [DataRow(1, 1, 3241, 3241)]
        [DataRow(0, 0, 3241, 3241)]
        public void Selected(
            int init,
            int ex1,
            int setTo,
            int ex2)
        {
            FakeBetSelectionMenu fakeBsm = new() { Selected = init };
            ICheckBox cb = new FakeCheckBox();
            IBetSelectionMenu bsm = new RAutobetBetSelectionMenu(fakeBsm, cb);

            Assert.AreEqual(ex1, fakeBsm.Selected);
            Assert.AreEqual(ex1, bsm.Selected);

            bsm.Selected = setTo;

            Assert.AreEqual(ex2, fakeBsm.Selected);
            Assert.AreEqual(ex2, bsm.Selected);
        } // end Range()

        [TestMethod]
        public void Dispose()
        {
            FakeBetSelectionMenu fakeBsm = new();
            ICheckBox cb = new FakeCheckBox();
            IBetSelectionMenu bsm = new RAutobetBetSelectionMenu(fakeBsm, cb);

            Assert.IsFalse(fakeBsm._Disposed_);

            bsm.Dispose();

            Assert.IsTrue(fakeBsm._Disposed_);
        } // end Dispose()

        [TestMethod]
        [DataRow(23, false, true, null, false, 0, 23, false, 1, 23, false, 1)] // doesn't open menu when autobetting
        [DataRow(0, false, true, null, false, 0, 0, false, 1, 0, false, 1)]
        [DataRow(497, true, true, null, true, 0, 497, true, 1, 497, false, 1)] // doesn't close menu when autobetting
        [DataRow(-12, true, true, null, true, 0, -12, true, 1, -12, false, 1)]
        [DataRow(92, false, false, null, false, 0, null, true, 0, 92, false, 1)] // does open menu when autobetting, waits on menu
        [DataRow(-1, true, false, null, true, 0, null, true, 0, -1, false, 1)] // leave open menu when autobetting, waits on menu
        public void Open(
            int selected, bool open, // inner bsm properties
            bool btnPressed, // check box properties

            int? exRecieved1, bool exOpen1, int exRecCount1, // prior to open(pretty redundant, no point removing now though it's free coverage.
            int? exRecieved2, bool exOpen2, int exRecCount2, // after open
            int? exRecieved3, bool exOpen3, int exRecCount3 // after sub submit
        )
        {
            FakeBetSelectionMenu fakeBsm = new() { Selected = selected, _Open_ = open };
            ICheckBox cb = new FakeCheckBox() { ButtonPressed = btnPressed };
            IBetSelectionMenu bsm = new RAutobetBetSelectionMenu(fakeBsm, cb);

            int? received = null;
            int receievedCount = 0;

            Assert.AreEqual(exOpen1, fakeBsm._Open_);
            Assert.AreEqual(exRecieved1, received);
            Assert.AreEqual(exRecCount1, receievedCount);

            bsm.Open((bet) =>
            {
                received = bet;
                receievedCount += 1;
            });

            Assert.AreEqual(exOpen2, fakeBsm._Open_);
            Assert.AreEqual(exRecieved2, received);
            Assert.AreEqual(exRecCount2, receievedCount);

            fakeBsm.Submit();

            Assert.AreEqual(exOpen3, fakeBsm._Open_);
            Assert.AreEqual(exRecieved3, received);
            Assert.AreEqual(exRecCount3, receievedCount);
        } // end Open()

        [TestMethod]
        public void Close()
        {
            FakeBetSelectionMenu fakeBsm = new() { _Open_ = true };
            ICheckBox cb = new FakeCheckBox();
            IBetSelectionMenu bsm = new RAutobetBetSelectionMenu(fakeBsm, cb);

            Assert.IsTrue(fakeBsm._Open_);

            bsm.Close();

            Assert.IsFalse(fakeBsm._Open_);

            bsm.Close(); // imdempotent

            Assert.IsFalse(fakeBsm._Open_);
        } // end Close()

    } // end class
} // end namespace
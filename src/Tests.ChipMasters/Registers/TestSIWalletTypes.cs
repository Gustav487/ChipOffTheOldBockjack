using ChipMasters.IO;
using ChipMasters.Registers;
using ChipMasters.User;
using Fakes.ChipMasters.Users;
using GSR.Utilic.Generic;

namespace Tests.ChipMasters.Registers
{
    [TestClass]
    public class TestSIWalletTypes
    {
        [TestMethod]
        public void GetUserWalletType()
        {
            IWallet w = new RDebtlessWallet();
            IWallet n = new RDebtlessWallet();
            IUser u = new FakeUser(wallet: w);
            IBijectiveDictionary<string, IType<IWallet>> r = SWalletTypes.REGISTER_WITH(() => u.Wallet);

            IType<IWallet> wt = r.Values.First(x => x.IsTypeOf(w));
            IType<IWallet> nt = r.Values.First(x => x.IsTypeOf(n));
            Assert.AreEqual(SWalletTypes.USER_KEY, r.I[wt]);
            Assert.AreNotEqual(SWalletTypes.USER_KEY, r.I[nt]);
        } // end GetUserWalletType()
    } // end class
} // end namespace
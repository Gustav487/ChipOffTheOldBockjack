using ChipMasters.Games.Sessions;
using ChipMasters.Items;
using ChipMasters.Menu.Avatars;
using ChipMasters.User;

namespace Fakes.ChipMasters.Users
{
    public sealed class FakeUser : IUser
    {
        public IWallet Wallet { get; }

        public IMetrics Metrics { get; } = new FakeMetrics();

        public IInventory Inventory { get; } = new FakeInventory();

        public IAssetSelection AssetSelection { get; } = new FakeAssetSelection();

        public ISession? Session
        {
            get => _session;
            set
            {
                if (_session == value)
                    return;

                _session = value;
                OnSessionChanged?.Invoke();
            }
        }
        private ISession? _session;
        public event Action? OnSessionChanged;
        public IAchievementTracker AchievementTracker { get; } = new FakeAchievementTracker();

        public IUserSettings Settings { get; } = new FakeUserSettings();

        public RAvatar Avatar { get; } = new RAvatar();

        public FakeUser(int chipCount = 0, IWallet? wallet = null)
        {
            Wallet = wallet ?? new RWallet(0);
            Wallet.Chips = chipCount;
        } // end ctor
    } // end class
} // end namespace
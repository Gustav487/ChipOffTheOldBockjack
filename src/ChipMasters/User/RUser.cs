using ChipMasters.Games.Sessions;
using ChipMasters.IO;
using ChipMasters.Items;
using ChipMasters.Menu.Avatars;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.User
{
    /// <summary>
    /// Simple <see cref="IUser"/> implementation.
    /// </summary>
    public class RUser : IUser
    {
        /// <summary>
        /// User currently playing the game. WARNING: only defined during while application is running.
        /// </summary>
        public static IUser INSTANCE => NApplicationDataManager.INSTANCE.User;

        /// <inheritdoc/>
        public IWallet Wallet { get; } = new RDebtlessWallet(SIWalletExtensions.DEFAULT_CHIPS);

        /// <inheritdoc/>
        public IMetrics Metrics { get; }

        /// <inheritdoc/>
        public IInventory Inventory { get; }

        /// <inheritdoc/>
        public IAssetSelection AssetSelection { get; }

        /// <inheritdoc/>
        public ISession? Session
        {
            get => _session;
            set
            {
                if (value == _session)
                    return;

                _session = value;
                OnSessionChanged?.Invoke();
            }
        } // end Session
        private ISession? _session = null;

        /// <inheritdoc/>
        public IAchievementTracker AchievementTracker { get; }

        /// <inheritdoc/>
        public event Action? OnSessionChanged;

        /// <inheritdoc/>
        public IUserSettings Settings { get; }

        /// <inheritdoc/>
        public RAvatar Avatar { get; }




        /// <summary>
        /// Constructor used for deserialization or explicit injection of an achievement tracker.
        /// </summary>
        public RUser(int chips, ISession? session, IMetrics metrics, IInventory inventory, IAssetSelection assetSelection, IEnumerable<IAchievement> achievements, IUserSettings settings, RAvatar avatar)
        {
            _session = session;
            Wallet.Chips = chips;
            Metrics = metrics.AssertNotNull();
            Inventory = inventory.AssertNotNull();
            AssetSelection = assetSelection.AssertNotNull();
            AchievementTracker = new RAchievementTracker(achievements, Metrics);
            Settings = settings.AssertNotNull();
            Avatar = avatar.AssertNotNull();

            Wallet.OnChipsChanged += () => Metrics.RecordChipCount(Wallet.Chips);
            if (!Metrics.ChipHistory.Any()) // If no history, log initial chip count
                Metrics.RecordChipCount(Wallet.Chips);

        } // end ctor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RUser() : this(SIWalletExtensions.DEFAULT_CHIPS, null, new RMetrics(), new RInventory(), new RAssetSelection(), Array.Empty<IAchievement>(), new RUserSettings(),
#if TESTING
            new RAvatar()
#else
			NAvatarDisplay.RandomAvatar()
#endif
            )
        { } // end ctor
    } // end class
} // end namespace

using ChipMasters.Games.Sessions;
using ChipMasters.Items;
using ChipMasters.Menu.Avatars;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;
using System.Linq;

namespace ChipMasters.User
{
    /// <summary>
    /// Contract for a player's representation.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="IUser"/> instances.
        /// 
        /// Handles fixed data that never changes on the user after load.
        /// First step of loading process
        /// </summary>
        public static IEnDec<IUser> P1_ENDEC =
            EnDecUtil.KeyedEnDecBuilder<string, IUser>(EnDecUtil.STRING)
            .Add("chips", EnDecUtil.INT_32, (x) => x.Wallet.Chips)
            .Add("metrics", IMetrics.CONTRACT_ENDEC, (x) => x.Metrics)
            .Add("inventory", IInventory.ENDEC, (x) => x.Inventory)
            .Add("assets", IAssetSelection.ENDEC, (x) => x.AssetSelection)
            .Add("achievements", IAchievement.ENDEC.ListOf(), (x) => x.AchievementTracker.Achievements.ToList())
            .Add("settings", IUserSettings.ENDEC, x => x.Settings)
            .Add("avatar", RAvatar.ENDEC, x => x.Avatar)
            .Build((c, h, i, ass, ach, settings, avatar) => new RUser(c, null, h, i, ass, ach, settings, avatar));

        /// <summary>
        /// Handles applying transient data to the user after load. Such as any ongoing sessions. 
        /// 
        /// This is by necessity split into a second step since the data may reference back the users fixed data, and thus to restore the integrity of references two load phases are required.
        /// </summary>
        public static readonly IEnApDec<IUser> P2_ENAPDEC = EnDecUtil.KeyedEnApDecBuilder<string, IUser>(EnDecUtil.STRING)
            .Add("session", ISession.ENDEC.NullableOfR(), (x) => x.Session, (x, y) => x.Session = y)
            .Build();



        /// <summary>
        /// The <see cref="IUser"/>'s <see cref="IWallet"/> - includes information and events for chip count.
        /// </summary>
        IWallet Wallet { get; }

        /// <summary>
        /// Record of the user's metrics.
        /// </summary>
        IMetrics Metrics { get; }

        /// <summary>
        /// The user's inventory of purchased items.
        /// </summary>
        IInventory Inventory { get; }

        /// <summary>
        /// The user's current cosmetic selection (skins, backgrounds, etc.).
        /// </summary>
        IAssetSelection AssetSelection { get; }

        /// <summary>
        /// Represents the current session of play for the user.
        /// </summary>
        ISession? Session { get; set; }

        /// <summary>
        /// Event fired when player's chip count changes.
        /// </summary>
        event Action? OnSessionChanged;

        /// <summary>
        /// The user's tracker of unlocked achievements.
        /// </summary>
        IAchievementTracker AchievementTracker { get; }

        /// <summary>
        /// The user's settings.
        /// </summary>
        IUserSettings Settings { get; }

        /// <summary>
        /// The user's <see cref="RAvatar"/>.
        /// </summary>
        RAvatar Avatar { get; }

    } // end interface
} // end namespace

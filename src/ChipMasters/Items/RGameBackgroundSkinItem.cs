using ChipMasters.Resources.Skins;
using ChipMasters.User;

namespace ChipMasters.Items
{
    /// <summary>
    /// <see cref="IApplicableItem"/> which applies a <see cref="IGameBackgroundSkin"/> to a <see cref="IUser"/>
    /// </summary>
    public sealed class RGameBackgroundSkinItem : RItem, IApplicableItem
    {
        private readonly IGameBackgroundSkin _gameBackgroundSkin;



        /// <inheritdoc/>
        public RGameBackgroundSkinItem(EItemCategory category, string name, int price, IGameBackgroundSkin gameBackgroundSkin) : base(category, name, price)
        {
            _gameBackgroundSkin = gameBackgroundSkin.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        public void Apply(IUser user) => user.AssetSelection.GameBackgroundSkin = _gameBackgroundSkin;

        /// <inheritdoc/>
        public bool IsApplied(IUser user) => user.AssetSelection.GameBackgroundSkin == _gameBackgroundSkin;
    } // end class
} // end namespace
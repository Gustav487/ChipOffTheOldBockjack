using ChipMasters.Resources.Skins;
using ChipMasters.User;

namespace ChipMasters.Items
{
    /// <summary>
    /// <see cref="IApplicableItem"/> which applies a <see cref="ICardSkin"/> to a <see cref="IUser"/>
    /// </summary>
    public sealed class RCardSkinItem : RItem, IApplicableItem
    {
        private readonly ICardSkin _cardskin;



        /// <inheritdoc/>
        public RCardSkinItem(EItemCategory category, string name, int price, ICardSkin cardskin) : base(category, name, price)
        {
            _cardskin = cardskin.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        public void Apply(IUser user) => user.AssetSelection.CardSkin = _cardskin;

        /// <inheritdoc/>
        public bool IsApplied(IUser user) => user.AssetSelection.CardSkin == _cardskin;
    } // end class
} // end namespace
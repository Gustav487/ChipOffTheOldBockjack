using ChipMasters.Items;

namespace Fakes.ChipMasters.Items
{
    public sealed class FakeItem : IItem
    {
        public EItemCategory Category { get; }

        public string Name { get; }

        public int Price { get; }



        public FakeItem(EItemCategory category = EItemCategory.Null, string name = "", int price = 1)
        {
            Category = category;
            Name = name;
            Price = price;
        } // end ctor
    } // end class
} // end namespace
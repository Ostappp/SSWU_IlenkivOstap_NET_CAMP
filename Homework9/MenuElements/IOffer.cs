
namespace Homework9.MenuElements
{
    internal interface IOffer
    {
        public string Name { get; }
        public string Description { get; }
        public Menu.OfferType OrderType { get; }
        public decimal Price { get; }
    }
}

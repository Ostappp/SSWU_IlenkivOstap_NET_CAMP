
using Homework9.KitchenData.Staff;

namespace Homework9.MenuElements
{
    internal interface IOffer
    {
        string Name { get; }
        string Description { get; }
        Menu.OfferType OrderType { get; }
        TimeOnly PrepearingTime { get; }
        List<IStaff> WorkedOnDish { get; }
        void AddCookToDish(IStaff staff);

    }
}

using Homework9.MenuElements;
namespace Homework9.KitchenData.Staff
{
    internal delegate void StaffNotify(IStaff staff, Menu.OfferType process);
    internal interface IStaff : IPerson
    {
        Menu.OfferType cookProcess { get; }
        void SetDishForWork(Order orderId, IOffer offer);
        event StaffNotify StaffNotify;
    }
}

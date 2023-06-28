using Homework9.MenuElements;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Homework9.KitchenData.Staff
{
    internal class Manager : IPerson
    {
        List<IStaff> _staff;
        Dictionary<Order, Dictionary<IOffer, uint>> _orders;
        public string Name { get; private set; }


        public Manager(string name, IEnumerable<IStaff> staff)
        {
            _orders = new();
            Name = name;
            SetListOfCookers(staff);
        }
        public void SetListOfCookers(IEnumerable<IStaff> staff)
        {
            if (staff.Count() < 7)
                throw new ArgumentException("Not enogh workers");
            if (staff.Where(x => (x is Cook) == true).Count() < 6)
                throw new ArgumentException("Not enogh cooks");

            _staff = new(staff);
            foreach (var cook in _staff.Where(x => (x is Cook) == true).Where(x => ((x as Cook).PizzaPrepearingStage == Pizza.PrepearingStage.PizzaBase) || (x.cookProcess != Menu.OfferType.Pizza)))
            {
                cook.StaffNotify -= GetWork;
                cook.StaffNotify += GetWork;
            }
            var unusedStaff = staff.Where(x => (x is Cook) == true).ToList();
            for (int i = 0; i < unusedStaff.Count / 6; i += 6)
            {
                (unusedStaff[i] as Cook).SetCookProcces(this, Menu.OfferType.Pizza, Pizza.PrepearingStage.PizzaBase);
                (unusedStaff[i] as Cook).SetNextWorker(unusedStaff[i + 1]);
                (unusedStaff[i + 1] as Cook).SetCookProcces(this, Menu.OfferType.Pizza, Pizza.PrepearingStage.Filling);
                (unusedStaff[i + 1] as Cook).SetNextWorker(unusedStaff[i + 2]);
                (unusedStaff[i + 2] as Cook).SetCookProcces(this, Menu.OfferType.Pizza, Pizza.PrepearingStage.Baking);

                (unusedStaff[i + 3] as Cook).SetCookProcces(this, Menu.OfferType.Dessert);
                (unusedStaff[i + 4] as Cook).SetCookProcces(this, Menu.OfferType.Drink);
                (unusedStaff[i + 5] as Cook).SetCookProcces(this, Menu.OfferType.OtherDish);

            }
            staff.Where(x => (x is Cook) == true).Where(x => x.cookProcess == Menu.OfferType.None).Select(x => x as Cook).ToList().ForEach(x => x.SetCookProcces(this, Menu.OfferType.OtherDish));
            unusedStaff = staff.Where(x => (x is StorageStaff) == true).ToList();
            SetStorageStaff(staff.Where(x => (x is Cook) == true).Where(x => (x.cookProcess != Menu.OfferType.Pizza) || ((x as Cook).PizzaPrepearingStage == Pizza.PrepearingStage.Baking)).ToList(), unusedStaff);

        }
        static void SetStorageStaff(List<IStaff> cooks, List<IStaff> storageStaff)
        {
            if (storageStaff.Count == 1)
                cooks.ForEach(x => (x as Cook).SetNextWorker(storageStaff[0]));
            else
            {
                int index = 0;
                cooks.ForEach(x => { (x as Cook).SetNextWorker(storageStaff[index % storageStaff.Count]); index++; });
            }
        }
        public void AddOrder(Order order)
        {
            Dictionary<IOffer, uint> newDictionary = new(order.GetList);
            _orders.Add(order, newDictionary);
            foreach (var staff in _staff.Where(x => (x is Cook) == true).Where(x => (x.cookProcess != Menu.OfferType.Pizza) || ((x as Cook).PizzaPrepearingStage == Pizza.PrepearingStage.PizzaBase)))
            {
                if((staff as Cook).WorkDone == null)
                {
                    CheckSetWork(staff as Cook);
                }
                
            }
        }
        void CheckSetWork(Cook staff)
        {
            if (staff.cookProcess == Menu.OfferType.Pizza)
            {
                if ((staff as Cook).PizzaPrepearingStage == Pizza.PrepearingStage.PizzaBase)
                {
                    var data = GiveWork(staff.cookProcess);
                    if (data != null)
                    {
                        if (_orders[data.Value.Item1][data.Value.Item2] == 1)
                            _orders[data.Value.Item1].Remove(data.Value.Item2);
                        else
                            _orders[data.Value.Item1][data.Value.Item2]--;
                        staff.SetDishForWork(data.Value.Item1, data.Value.Item2);
                    }
                }
            }
            else
            {
                var data = GiveWork(staff.cookProcess);
                if (data != null)
                {
                    if (_orders[data.Value.Item1][data.Value.Item2] == 1)
                        _orders[data.Value.Item1].Remove(data.Value.Item2);
                    else
                        _orders[data.Value.Item1][data.Value.Item2]--;
                    staff.SetDishForWork(data.Value.Item1, data.Value.Item2);
                }
            }
        }
        (Order, IOffer)? GiveWork(Menu.OfferType offerType)
        {
            foreach (var order in _orders)
            {
                foreach (var item in order.Value)
                {
                    if (offerType == item.Key.OrderType)
                        return (order.Key, item.Key);
                }
            }
            return null;
        }
        void GetWork(IStaff staff, Menu.OfferType process)
        {
            if (staff is Cook)
            {

                CheckSetWork(staff as Cook);
            }
        }
    }
}

using Homework9.MenuElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework9.KitchenData.Staff
{
    internal class StorageStaff : IStaff
    {
        public Menu.OfferType cookProcess { get => Menu.OfferType.None; }
        public string Name { get; private set; }
        public event StaffNotify StaffNotify;

        private Storage _storage;

        public StorageStaff(string name, Storage storage)
        {
            if(storage!=null)
                _storage = storage;
            Name = name;
        }
        public void SetDishForWork(Order orderId, IOffer offer)
        {
            _storage.AddToStorage(orderId, offer);
            StaffNotify.Invoke(this, cookProcess);
        }
    }
}

using Homework9.KitchenData.Staff;
namespace Homework9.KitchenData
{
    internal class Kitchen
    {
        private Manager _manager;
        private List<IStaff> _staff;
        private Storage _storage;
        public event OrderComplete ReadyOrder;
        public Kitchen(Manager manager, IEnumerable<Cook> cooks, Storage storage)
        {
            _manager = manager;
            _staff = new(cooks);
            _storage = storage;
            _storage.OrderCompleted += InvokeEvent;
        }
        public Kitchen()
        {
            _storage = new Storage();
            _storage.OrderCompleted += InvokeEvent;
            _staff = new()
            {
                new Cook("Cook0"),
                new Cook("Cook1"),
                new Cook("Cook2"),
                new Cook("Cook3"),
                new Cook("Cook4"),
                new Cook("Cook5"),
                new StorageStaff("StorageStaff",_storage)
            };
            _manager = new Manager("Manager", _staff);
        }
        public void Order(Order order)
        {
            _manager.AddOrder(order);
        }
        void InvokeEvent(Order order)
        {
            ReadyOrder(order);
        }
        public void StartWork() { }
    }
}

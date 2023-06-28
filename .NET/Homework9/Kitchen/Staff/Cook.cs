using Homework9.MenuElements;
using System;

namespace Homework9.KitchenData.Staff
{
    internal class Cook : IStaff
    {
        public string Name { get; private set; }
        public Menu.OfferType cookProcess { get; private set; }
        public event StaffNotify StaffNotify;

        public Pizza.PrepearingStage? PizzaPrepearingStage { get; private set; }

        private Order _orderId;
        private IOffer _offer;

        private IStaff _nextWorkOnDish;
        public bool? WorkDone
        {
            get;
            private set;
        }
        bool isNextFree = true;
        bool IsNextFree { get => isNextFree; set { if (WorkDone.HasValue && WorkDone.Value) SendDish(); } }

        public Cook(string name)
        {
            Name = name;
            WorkDone = null;
            cookProcess = Menu.OfferType.None;

        }
        public void SetCookProcces(Manager manager, Menu.OfferType process, Pizza.PrepearingStage? stage = null)
        {
            if (manager != null)
            {
                PizzaPrepearingStage = stage;
                cookProcess = process;
            }
        }
        public void Cooking()
        {
            ///process
            WorkDone = true;
            if (isNextFree)
                SendDish();
        }
        public void SetDishForWork(Order orderId, IOffer offer)
        {
            _orderId = orderId;
            _offer = offer;
            WorkDone = false;
            Cooking();
        }
        void SendDish()
        {
            var tmp1 = _orderId;
            var tmp2 = _offer;
            if (_orderId != null && _offer != null)
                if (IsNextFree && WorkDone.HasValue && WorkDone.Value)
                {
                    _offer.WorkedOnDish.Add(this);
                    WorkDone = null;
                    _orderId = null;
                    _offer = null;
                    _nextWorkOnDish.SetDishForWork(tmp1, tmp2);

                    StaffNotify(this, cookProcess);
                }

        }
        public void SetNextWorker(IStaff nextPerson)
        {
            if (_nextWorkOnDish != null)
                _nextWorkOnDish.StaffNotify -= GiveDishToNextPerson;
            _nextWorkOnDish = nextPerson;
            _nextWorkOnDish.StaffNotify += GiveDishToNextPerson;
        }
        void GiveDishToNextPerson(IStaff staff, Menu.OfferType process)
        {
            IsNextFree = true;
        }
        public override string ToString()
        {
            if (PizzaPrepearingStage.HasValue)
                return $"Staff name: {Name}. Works with: {cookProcess}, pizza part: {PizzaPrepearingStage.Value}";
            else
                return $"Staff name: {Name}. Works with: {cookProcess}";
        }
        public enum CookProcess
        {
            None,
            Drinks,
            Pizza,
            IceCream,
            OtherDish,
        }
    }
}

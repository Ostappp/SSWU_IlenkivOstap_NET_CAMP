using Homework9.MenuElements;

namespace Homework9.KitchenData
{
    internal delegate void OrderComplete(Order order); 
    internal class Storage
    {
        public event OrderComplete OrderCompleted;   
        Dictionary<Order, Dictionary<IOffer, uint>> _prepearedOffers_id;
        public Storage()
        {
            _prepearedOffers_id = new Dictionary<Order, Dictionary<IOffer, uint>>();
        }

        public void AddToStorage(Order orderId, IOffer offer)
        { 
            CheckAndAdd:
            if(_prepearedOffers_id.ContainsKey(orderId))
            {
                _prepearedOffers_id[orderId][offer]++;
                if (IsOrderComplete(orderId))
                    IssueOrder(orderId);
            }
            else
            {
                Dictionary<IOffer, uint> newDictionary = new(orderId.GetList.ToDictionary(x => x.Key, x => (uint)0));
                _prepearedOffers_id.Add(orderId, newDictionary);
                goto CheckAndAdd;
            }

        }
        void IssueOrder(Order order)
        {
            OrderCompleted.Invoke(order);
            _prepearedOffers_id.Remove(order);
        }
        bool IsOrderComplete(Order order) 
        {
            foreach (var offer in order.GetList)
            {
                if (_prepearedOffers_id[order][offer.Key] != offer.Value)
                    return false;
            }
            return true;
        }
    }
}

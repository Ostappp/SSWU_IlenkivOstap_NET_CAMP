using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework9.Kitchen.Staff
{
    internal class Manager
    {
        Dictionary<Cook.CookProcess, List<Cook>> _cooks;
        List<Order> _orders;


        public void SetListOfCookers(IEnumerable<Cook> cooks)
        {
            _cooks = new();
            foreach (var cook in cooks)
            {
                if (_cooks.ContainsKey(cook.cookProcess))
                {
                    _cooks[cook.cookProcess].Add(cook);
                }
                else
                {
                    _cooks.Add(cook.cookProcess, new() { cook });
                }
            }
        }
        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }
    }
}

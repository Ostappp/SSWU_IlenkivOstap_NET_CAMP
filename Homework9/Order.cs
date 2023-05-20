using Homework9.MenuElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework9
{
    internal class Order
    {
        int _id;
        Dictionary<IOffer, uint> _offers;

        public int GetId { get => _id; }
        public Dictionary<IOffer, uint> GetList { get => new(_offers); }
        public Order(int id, IEnumerable<KeyValuePair<IOffer, uint>> offers) 
        { 
            _id = id;
            _offers = new (offers);
        }
    }
}

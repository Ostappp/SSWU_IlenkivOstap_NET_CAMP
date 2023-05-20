using Homework9.Kitchen.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework9.Kitchen
{
    internal class Kitchen
    {
        private Manager _manager;
        private List<Cook> _cooks;
        private Storage _storage;

        public Kitchen(Manager manager, IEnumerable<Cook> cooks)
        {
            _manager = manager;
            _cooks = new(cooks);
        }
    }
}

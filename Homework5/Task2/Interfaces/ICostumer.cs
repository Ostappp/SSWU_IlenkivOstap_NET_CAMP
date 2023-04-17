using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5.Task2.Interfaces
{
    internal interface ICostumer
    {
        public List<PackedItems> PurshasedGods { get; set; }
        public string AttendedDepartment { get; }
        public void MoveIn(string department);
        public void MoveOut();
        public void Buy(string itemName, int count);

    }
}

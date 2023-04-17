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
        public bool MoveIn(string department, out string report);
        public void MoveOut(out string report);
        public bool Buy(string itemName, int count, out string report);
        public bool Leave(out string report);
        public string ShowGods();
        public string ShowWays();
        public string ShowItemsToBuy();
        public enum Actions
        {
            Buy,
            MoveIn,
            MoveOut,
            Leave,
            ShowItems,
            GetWays,
            SeeItemsToBuy,
        }

    }
}

using Homework5.Task2.Interfaces;
using System.Linq;

namespace Homework5.Task2
{
    internal class SuperUser : IManager, ICostumer
    {
        List<PackedItems> _purshasedItems;
        Store? _currentStore;
        string _locaionInStore;
        public List<PackedItems> PurshasedGods { get => _purshasedItems; set => _purshasedItems = value; }
        public string AttendedDepartment { get => _locaionInStore; }


        public SuperUser(Store store)
        {
            PurshasedGods = new List<PackedItems>();
            _currentStore = store;

        }

        public string ManageObject(IManagable obj, string commandToExecute, out string report)
        {
            obj.Execute(this, commandToExecute, out report);
            return report;
        }

        public void MoveIn(string department)
        {
            throw new NotImplementedException();
        }

        public void MoveOut()
        {
            _currentStore.PackInBox(this);
            _locaionInStore = string.Join('/', _locaionInStore.Split('/')[..^2]);

        }
        public void Buy(string itemName, int count)
        {
            throw new NotImplementedException();
        }
    }
}

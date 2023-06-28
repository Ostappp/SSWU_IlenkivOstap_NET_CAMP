using Homework5.Task2.Interfaces;
using System.Linq;
using System.Text;

namespace Homework5.Task2
{
    internal class SuperUser : Person, IManager, ICustomer
    {//У цій задачі зробили більше, ніж просили) Молодець.
        List<PackedItems> _purshasedItems;
        Store _currentStore;
        string _locaionInStore;
        public string Manual { get => _currentStore.Manual; }
        public List<PackedItems> PurshasedGods { get => _purshasedItems; set => _purshasedItems = value; }
        public string AttendedDepartment { get => _locaionInStore == null ? "outside" : _locaionInStore; }

        public IManagable ManagedObj { get => _currentStore; }
        public SuperUser()
        {
            PurshasedGods = new List<PackedItems>();
        }
        public SuperUser(Store store)
        {
            PurshasedGods = new List<PackedItems>();
            _currentStore = store;
            _locaionInStore = store.GetName;
        }
        public bool AddStore(Store store)
        {
            if (_currentStore == null)
            {
                _currentStore = store;
                _locaionInStore = store.GetName;
                return true;
            }
            return false;

        }
        public string ManageObject(IManagable obj, string commandToExecute, out string report)
        {
            obj.Execute(this, commandToExecute, out report);
            return report;
        }

        public bool MoveIn(string department, out string report)
        {
            List<string> ways = _currentStore.GetPathIn(this);
            if (ways.Contains(department))
            {
                report = $"You walked in [{department}] deparetment";
                _locaionInStore = _locaionInStore + '/' + department;
                return true;
            }
            report = $"[{department}] deparetment does not exist in current department";
            return false;
        }

        public void MoveOut(out string report)
        {
            _currentStore.PackInBox(this);
            if (_locaionInStore.Contains('/'))
            {
                report = $"You have out of the [{_locaionInStore.Split('/')[^1]}] department";
                _locaionInStore = string.Join('/', _locaionInStore.Split('/')[..^1]);
            }
            else
            {
                report = $"[{_locaionInStore.Split('/')[^1]}] is not depatment. Is is a store name. To leave store use proper command.";
            }

        }
        public bool Buy(string itemName, int count, out string report)
        {
            return _currentStore.SellItem(this, itemName, count, out report);
        }
        public bool Leave(out string report)
        {
            if (AttendedDepartment.Split('/').Length == 1)
            {
                _currentStore.PackInBox(this);
                report = "You have successfully exited the store";
                _currentStore = null;
                _locaionInStore = string.Empty;
                return true;
            }
            report = "To leave the store you must leave all its departments";
            return false;
        }
        public string ShowGods()
        {
            StringBuilder sb = new StringBuilder();
            if (PurshasedGods.Count > 0)
            {
                sb.AppendLine($"Items in property [{PurshasedGods.Count}]:");
                foreach (var item in PurshasedGods)
                {
                    sb.Append($"{("\n"+ item.ToString()).Replace("\n", "\n\t").Replace("\t", "\t\t")}");
                }
            }
            else
                sb.AppendLine($"You have no items in property.");

            return sb.ToString();
        }
        public string ShowWays()
        {
            return string.Join(" | ", _currentStore.GetPathIn(this));
        }
        public string ShowItemsToBuy()
        {
            return _currentStore.ShowItems(this);
        }
    }
}

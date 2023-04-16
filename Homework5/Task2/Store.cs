namespace Homework5.Task2
{
    internal class Store
    {
        List<DepartmentHolder> _departments;
        //public Store(List<DepartmentHolder> departments)
        //{
        //    _departments = departments;
        //}
        bool AddDepartment(Department newDepartment, DepartmentHolder holder, ref string report)
        {
            if (_departments.Contains(holder))
            {
                report += $"Department {holder.GetName} exist.\n";
                _departments.Find(d => d.Equals(holder)).AddDepartment(newDepartment,ref report);
                return true;
            }
            report += $"Can not find {holder.GetName} department.\n";
            return false;
        }
        struct Department
        {
            string _name;
            List<Item> _itemsForSale;
            DepartmentHolder _parentDepartment;
            List<DepartmentHolder>? _departments;
            public string Name { get => _name; }
            public string DepartmentLocation { get => $"{_parentDepartment.GetName}/{Name}"; }

            public Department(string name, List<Item> itemsForSale, DepartmentHolder parentDepartment, List<DepartmentHolder> departments)
            {
                _name = name;
                _parentDepartment = parentDepartment;
                _itemsForSale = itemsForSale;
                _departments = departments;
            }
            //додати ціну
            public Box<Item> SellItems(Item item, int count)
            {
                return new Box<Item>(GetItemPath(item), Enumerable.Repeat(item, count).ToList());
            }
            public string GetItemPath(Item item) => $"{DepartmentLocation}:{item.Name}";

        }
        struct DepartmentHolder
        {
            string _name;
            List<Department> _departments;
            public string GetName { get => _name; }
            public bool AddDepartment(Department department, ref string message)
            {
                if (_departments.Contains(department))
                {
                    message += $"Department {department.Name} already exist in {GetName}.\n";
                    return false;
                }
                    
                _departments.Add(department);
                message += $"Department {department.Name} successfully added to {GetName}.\n";
                return true;
            }
        }
    }
}

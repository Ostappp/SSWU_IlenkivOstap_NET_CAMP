using Homework5.Task2.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace Homework5.Task2
{
    internal class Store : IManagable
    {
        public const string MANUAL =
@"Add new department: add_dep <path> <department-name>

Add new item: add_item <path-to-department>:<item-name> <cost> <sizeX>|<sizeY>|<sizeZ>
            ~~~~~~~~~~~~~~~~~~~~~~~~~!!!WARNING!!!~~~~~~~~~~~~~~~~~~~~~~~~~
The price must be inputed in following rules: [digits],[fractional-digits]

Change department name: change_dep_name <path> <new-name>
Change item price: change_item_price <path-to-department>:<item-name> <new-price>
Remove department: rm <path>
Remove item: rm <path-to-department>:<item-name>";
        string _name;
        string _address;
        DepartmentHolder _rootDepartment;
        public string GetFullName { get => $"{_address}, {_name}"; }
        public string GetName { get => _name; }
        public string Manual => throw new NotImplementedException();

        public Store(IManager manager, string name, string address)
        {
            _address = address;
            _name = name;
            _rootDepartment = new DepartmentHolder(_name, _name);
        }
        public List<string> GetPathIn(ICostumer costumer)
        {
            FindDepartment(costumer.AttendedDepartment, out Department currentDepartment, out string report);
            return currentDepartment.Departments.Departments.Select(x=>x.Name).ToList();
        }
        public string ShowItems(ICostumer costumer)
        {
            string departmentPath = costumer.AttendedDepartment;

            bool isDepartmentExist = FindDepartment(departmentPath, out Department seekedDepartment, out string message);
            StringBuilder sb = new StringBuilder();
            foreach (var item in seekedDepartment.Items)
            {
                sb.AppendLine($"\t\t{item.Key.Name,-16}: {item.Value:c2}");
            }
            if (seekedDepartment.IncludedDepartments.Count > 0)
            {
                sb.AppendLine($"Also wisit other departments:");
                foreach (var deparment in seekedDepartment.IncludedDepartments)
                {
                    sb.Append($"\t{deparment}");
                }
            }

            if (isDepartmentExist)
                return sb.ToString();
            else
                return message;
        }
        public string ShowDepartmentData(ICostumer costumer)
        {
            string departmentPath = costumer.AttendedDepartment;
            FindDepartment(departmentPath, out Department department, out string message);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Info about your location [{departmentPath}]:");
            if (department.Departments.Departments.Count > 0)
            {
                sb.Append($"\tYou can visid folloving departments:");
                foreach (var dep in department.Departments.Departments)
                {
                    sb.Append($"\t{dep.Name}");
                }
            }

            sb.Append("\tYou can buy:");
            foreach (var item in department.Items)
            {
                sb.Append($"\t{item.Key,-64}\tPrice: {item.Value:c2}");
            }

            if (department.ParentDepartment.Path.Contains('/'))
            {
                sb.AppendLine($"\tYou can move out to [{department.ParentDepartment}]");
            }

            return sb.ToString();
        }
        public void PackInBox(ICostumer costumer)
        {
            //пакування відбувається при виході покупця із відділу і викликатиметься ним ззовні
            if (costumer.PurshasedGods.Count == 0)
                return;

            List<PackedItems> itemsToPack = new List<PackedItems>();
            string tmp;
            foreach (var item in costumer.PurshasedGods)
            {
                if (item.GetLabel.Contains(':'))
                {
                    tmp = item.GetLabel.Split(':')[0];
                    if (tmp == costumer.AttendedDepartment)
                        itemsToPack.Add(item);

                }
                else
                {
                    if (item.GetLabel == costumer.AttendedDepartment)
                        itemsToPack.Add(item);
                }
            }
            costumer.PurshasedGods.RemoveAll(itemsToPack.Contains);
            costumer.PurshasedGods.AddRange(itemsToPack);
        }
        public bool SellItem(ICostumer costumer, string itemName, int count, out string report)
        {
            if(FindDepartment(costumer.AttendedDepartment, out Department dep, out string message))
            {
                Item? item = dep.ContainsItem(itemName);
                if(item != null)
                {
                    if (dep.SellItems(costumer, item, count, out message))
                    {
                        report = $"Item [{item}] has been successfully sold.\n\t{message}";
                        return true;
                    }
                    else
                    {
                        report = $"Error when solding item [{item}]\n\t{message}";
                        return false;
                    }
                       
                }
                else
                {
                    report = $"Item [{item}] does not exist in [{costumer.AttendedDepartment}] department";
                    return false;
                }
            }
            else
            {
                report = $"Can not find path to [{costumer.AttendedDepartment}]";
                return false;
            }
        }
        bool FindDepartment(string departmentPath, out Department seekedDepartment, out string report)
        {
            DepartmentHolder holder = _rootDepartment;
            seekedDepartment = new Department();
            bool found = false;
            string[] departmentName = departmentPath.Split('/');
            for (int i = 0; i < departmentName.Length; i++)
            {
                for (int j = 0; j < holder.Departments.Count; j++)
                {
                    if (holder.Departments[j].Name == departmentName[i])
                    {
                        holder = holder.Departments[j].ParentDepartment;
                        if (i == departmentName.Length - 1)
                        {
                            seekedDepartment = holder.Departments[j];
                            found = true;
                        }
                        break;
                    }
                }
            }
            if (found)
            {
                report = $"Department [{departmentPath}] exist";
                return true;
            }
            else
            {
                report = $"Department [{departmentPath}] does not exist";

                return false;
            }
        }
        //структура:
        //DeparmentHolder
        //  @Name
        //  --Deparment1
        //      @DName1
        //      $Items
        //      ---DeparmentHolder1
        //          @Name1
        //          --Deparment1.1
        //              @DName1.1
        //              $Items
        //              ---DeparmentHolder1.1
        //          --Deparment1.2
        //              @DName1.2
        //              $Items
        //              ---DeparmentHolder1.2
        //          --Deparment1.3
        //              @DName1.3
        //              $Items
        //              ---DeparmentHolder1.3
        //  --Deparment2
        //      @DName2
        //      $Items
        //      ---DeparmentHolder2
        //          @Name2
        //          --Deparment2.1
        //              @DName2.1
        //              $Items
        //              ---DeparmentHolder2.1
        //          --Deparment2.2
        //              @DName2.2
        //              $Items
        //              ---DeparmentHolder2.2
        //          --Deparment2.3
        //              @DName2.3
        //              $Items
        //              ---DeparmentHolder2.3

        struct Department
        {
            string name;
            Dictionary<Item, decimal> itemsForSale;
            DepartmentHolder parentDepartment;
            DepartmentHolder departments;
            public string Name { get => name; set => name = value; }
            public string DepartmentLocation { get => $"{parentDepartment.Path}/{Name}"; }
            public DepartmentHolder ParentDepartment { get => parentDepartment; }
            public Dictionary<Item, decimal> Items { get => itemsForSale; }
            public List<string> IncludedDepartments
            {
                get
                {
                    List<string> departmentsNames = new List<string>();
                    foreach (var dep in departments.Departments)
                    {
                        departmentsNames.Add(dep.Name);
                    }
                    return departmentsNames;
                }
            }
            public DepartmentHolder Departments { get => departments; }
            public Department(string name, Dictionary<Item, decimal> itemsForSale, DepartmentHolder parentDepartment)
            {
                this.name = name;
                this.parentDepartment = parentDepartment;
                this.itemsForSale = itemsForSale;
                departments = new DepartmentHolder(name, $"{this.parentDepartment.Path}/{name}");
            }
            public Department(string name, DepartmentHolder parentDepartment)
            {
                this.parentDepartment = parentDepartment;
                this.name = name;
                departments = new DepartmentHolder(name, $"{this.parentDepartment.Path}/{name}");
                itemsForSale = new Dictionary<Item, decimal>();
            }
            public Item? ContainsItem(string itemName)
            {
                foreach (var item in itemsForSale)
                {
                    if (item.Key.Name == itemName)
                        return item.Key;
                }
                return null;
            }
            public bool ChangeName(string newName, out Department dep, out string report)
            {
                if (departments.Departments.Any(d => d.Name == newName))
                {
                    report = $"Department {newName} already exist";
                    dep = this;
                    return false;
                }

                Name = newName;
                departments.Name = newName;
                departments.RefreshPath();
                dep = this;
                report = $"Department name successfully changet from {Name} to {newName}.";
                return true;
            }
            public void RefreshPath()
            {
                departments.RefreshPath();
            }
            public bool AddDepartment(Department newDep, out string report)
            {
                return departments.AddDepartment(newDep, out report);
            }
            public bool SellItems(ICostumer costumer,Item item, int count, out string report)
            {
                if (itemsForSale.ContainsKey(item))
                {
                    report = $"The product [{item.Name}] was sold in amount of {count} pieces. Total cost: {itemsForSale[item] * count:c2}.";
                    PackedItems boxWithItems = new PackedItems(GetItemPath(item), Enumerable.Repeat(item as ObjectWithSize, count).ToList());
                    costumer.PurshasedGods.Add(boxWithItems);
                    return true;
                }
                report = $"The product [{item.Name}] was sold in amount of {count} pieces. Total cost: {itemsForSale[item] * count:c2}.";

                return false;
            }

            public string GetItemPath(Item item) => $"{DepartmentLocation}:{item.Name}";

            public bool AddItem(Item item, decimal cost, out string report)
            {
                if (itemsForSale.ContainsKey(item))
                {
                    report = $"Product [{item.Name}] already sold in department [{Name}].";
                    return false;
                }
                else
                {
                    itemsForSale.Add(item, cost);
                    report = $"Product [{item.Name}] successfully added to department [{Name}] at price [{cost:c2}].";
                    return true;
                }
            }

            public bool ChangeCost(string itemName, decimal newCost, out string report)
            {
                if (itemsForSale.Any(i => i.Key.Name == itemName))
                {
                    Item item = itemsForSale.First(i => i.Key.Name == itemName).Key;
                    report = $"The cost of [{itemName}] has been successfully  changed from [{itemsForSale[item]:c2}] to [{newCost:c2}].";
                    itemsForSale[item] = newCost;
                    return true;
                }
                else
                {
                    report = $"Product [{itemName}] is not sold in [{Name}] departmnet.";
                    return false;
                }
            }

            public bool RemoveItem(string itemName, out string report)
            {
                if (itemsForSale.Any(i => i.Key.Name == itemName))
                {
                    Item item = itemsForSale.First(i => i.Key.Name == itemName).Key;
                    report = $"Product [{item.Name}] has been successfuly removed from department [{Name}].";
                    itemsForSale.Remove(item);
                    return true;
                }
                else
                {
                    report = $"Product [{itemName}] is not sold in [{Name}] departmnet.";
                    return false;
                }
            }

        }
        struct DepartmentHolder
        {
            string name;
            string path;
            List<Department> departments;

            public string Path { get => path; }
            public string Name { get => name; set => name = value; }
            public List<Department> Departments { get => departments; }

            public DepartmentHolder(string name, string path)
            {
                this.path = path;
                this.name = name;
                departments = new List<Department>();
            }
            public DepartmentHolder(string name, string path, Department department)
            {
                this.path = path;
                this.name = name;
                departments = new List<Department>() { department };
            }
            public bool ContainsDepartment(string departmentName) => Departments.Any(d => d.Name == departmentName);
            public void RefreshPath()
            {
                List<string> newPath = path.Split('/').ToList();
                newPath[newPath.Count - 1] = name;
                path = string.Join('/', newPath);
                foreach (var dep in departments)
                {
                    dep.RefreshPath();
                }
            }
            public bool AddDepartment(Department department, out string report)
            {
                if (departments.Contains(department))
                {
                    report = $"Department [{department.Name}] already exist in [{Name}].";
                    return false;
                }

                departments.Add(department);
                report = $"Department [{department.Name}] successfully added to [{Name}].";
                return true;
            }
            public bool RemoveDepartment(string departmentName, out string report)
            {
                if (!departments.Any(d => d.Name == departmentName))
                {
                    report = $"Department [{departmentName}] does not exist in [{Name}].";
                    return false;
                }

                departments.RemoveAll(d => d.Name == departmentName);
                report = $"Department [{departmentName}] successfully deleted from [{Name}].";
                return true;
            }

        }
        public bool Execute(IManager m, string command, out string report)
        {
            report = string.Empty;
            return Interpret(command, ref report);
        }
        bool Interpret(string command, ref string report)
        {
            string[] commands = command.Split("\r\n");
            bool[] results = new bool[commands.Length];
            for (int i = 0; i < commands.Length; i++)
            {
                string commandSelector = commands[i].Split(' ')[0];
                if (CommandStrings.ContainsValue(commandSelector))
                {
                    results[i] = true;
                    string message;
                    if (CommandStrings[ManagementCommands.AddDepartment] == commandSelector)
                    {
                        results[i] = AddDepartment(commands[i], out message);
                    }
                    else if (CommandStrings[ManagementCommands.AddItem] == commandSelector)
                    {
                        results[i] = AddItem(commands[i], out message);
                    }
                    else if (CommandStrings[ManagementCommands.ChangeDepartmentName] == commandSelector)
                    {
                        results[i] = ChangeDepartmentName(commands[i], out message);
                    }
                    else if (CommandStrings[ManagementCommands.ChangeItemCost] == commandSelector)
                    {
                        results[i] = ChangeItemCost(commands[i], out message);
                    }
                    else
                    {
                        results[i] = Remove(commands[i], out message);
                    }
                    report += $"\n\t[{results[i]}]\treport by command [{i}]:\n\t{commands[i]}\n{message}";
                }
                else
                {
                    report += $"\t[{results[i]}]\tInvalid command [{i}]";
                    results[i] = false;
                }
            }
            return results.All(r => r == true);
        }
        bool AddDepartment(string command, out string report)
        {
            string[] args = command.Split(' ')[1..];
            string name = args[1];
            if (string.IsNullOrEmpty(name) || name.Any(character => !char.IsLetter(character)) || string.IsNullOrWhiteSpace(name))
            {
                report = $"\t\tInvalid department name [{name}]";
                return false;
            }
            string message;
            if (args[0].Split('/').Length == 1 || (args[0].Split('/').Length == 2 && args[0].Split('/')[1] == ""))
            {
                DepartmentHolder holder = _rootDepartment;

                if (holder.AddDepartment(new Department(args[1], holder), out message))
                {
                    report = $"\t\tAdding new department to {args[0]}\n\t\t\t{message}";
                    return true;
                }
                report = $"\t\tError when adding new department to {args[0]}\n\t\t\t{message}";
                return false;
            }
            else
            {
                bool isDepartmentExist = FindDepartment(args[0], out Department seekedDepartment, out message);
                if (!isDepartmentExist)
                {
                    report = $"\t\t{message}";
                    return false;
                }


                if (seekedDepartment.AddDepartment(new Department(args[1], seekedDepartment.Departments), out message))
                {
                    report = $"\t\tAdding new department to {args[0]}\n\t\t\t{message}";
                    return true;
                }
                report = $"\t\tError when adding new department to {args[0]}\n\t\t\t{message}";
                return false;
            }

        }
        bool AddItem(string command, out string report)
        {
            string[] args = command.Split(' ')[1..];
            string itemName = args[0].Split(':')[1];
            bool isDepartmentExist = FindDepartment(args[0].Split(':')[0], out Department department, out string message);

            if (!isDepartmentExist)
            {
                report = $"\t\t{message}";
                return false;
            }

            if (args[1].ToCharArray().Any(c => !char.IsDigit(c) && c != ','))
            {
                report = $"\t\tInvalid price [{args[1]}]";
                return false;
            }
            decimal itemCost = decimal.Parse(args[1]);

            if (department.AddItem(new Item(itemName, args[2]), itemCost, out message))
            {
                report = $"\t\tAdding new item to {args[0]}\n\t\t\t{message}";
                return true;
            }
            report = $"\t\tError when adding new item to {args[0]}\n\t\t\t{message}";
            return false;
        }
        bool ChangeDepartmentName(string command, out string report)
        {
            string[] args = command.Split(' ')[1..];
            string name = args[1];
            if (string.IsNullOrEmpty(name) || name.Any(character => !char.IsLetter(character)))
            {
                report = $"\t\tInvalid department new name [{name}]";
                return false;
            }
            bool isDepartmentExist = FindDepartment(args[0], out Department department, out string message);
            DepartmentHolder depHolder = department.ParentDepartment;
            if (!isDepartmentExist)
            {
                report = $"\t\t{message}";
                return false;
            }
            string cuurentName = args[0].Split('/').Last();
            //department = depHolder.Departments.First(x => x.Name == cuurentName);
            if (department.ChangeName(args[1], out department, out message))
            {
                FindDepartment(args[0], out Department tmp, out string tmpMessage);
                depHolder = tmp.ParentDepartment;
                tmp.ParentDepartment.RemoveDepartment(tmp.Name, out tmpMessage);
                depHolder.AddDepartment(department, out tmpMessage);
                report = $"\t\tChangeing department name to {args[1]}\n\t\t\t{message}";
                return true;
            }
            report = $"\t\tError when changing deparment name\n\t\t\t{message}";
            return false;
        }
        bool ChangeItemCost(string command, out string report)
        {
            //формат команди: change_item_cost /vegetables:apple 19.5
            //де / - шлях до Department до якого додамо новий відділ, vegetaples - назва нового DepartmentHolder
            string[] args = command.Split(' ')[1..];
            //arg 1 - path
            //arg 2 - new department holder name
            List<string> departmentsPath = args[0].Split('/').ToList();
            string itemName = args[0].Split(':')[1];
            bool isDepartmentExist = FindDepartment(args[0].Split(':')[0], out Department department, out string message);

            if (!isDepartmentExist)
            {
                report = $"\t\t{message}";
                return false;
            }
            if (args[1].ToCharArray().Any(c => !char.IsDigit(c) && c != ','))
            {
                report = $"\t\tInvalid price [{args[1]}]";
                return false;
            }
            decimal itemCost = decimal.Parse(args[1]);
            if (department.ChangeCost(itemName, itemCost, out message))
            {
                report = $"\t\tChanging item [{itemName}] cost to {itemCost:c2}\n\t\t\t{message}";
                return true;
            }
            report = $"\t\tError when changing item [{itemName}] cost\n\t\t\t{message}";
            return false;
        }
        bool Remove(string command, out string report)
        {
            string[] args = command.Split(' ')[1..];
            List<string> departmentsPath = args[0].Split('/').ToList();
            bool isItemTarget = false;
            string itemName = departmentsPath.Last();
            bool isDepartmentExist = false;
            Department department;
            string message;
            if (departmentsPath.Last().Contains(':'))
            {
                isItemTarget = true;
                isDepartmentExist = FindDepartment(args[0].Split(':')[0], out department, out message);
                itemName = itemName.Split(':')[1];
                departmentsPath[departmentsPath.Count - 1] = departmentsPath[departmentsPath.Count - 1].Split(':')[0];
            }
            else
                isDepartmentExist = FindDepartment(args[0], out department, out message);

            DepartmentHolder holder = department.ParentDepartment;

            if (!isDepartmentExist)
            {
                report = $"\t\t{message}";
                return false;
            }
            if (isItemTarget)
            {
                if (department.RemoveItem(itemName, out message))
                {
                    report = $"\t\tItem [{itemName}] successfuly removed from {args[0].Split(':')[0]}\n\t\t\t{message}";
                    return true;
                }
                report = $"\t\tError when removing [{itemName}] from {args[0].Split(':')[0]}\n\t\t\t{message}";
                return false;
            }
            else
            {
                if (holder.RemoveDepartment(itemName, out message))
                {
                    report = $"\t\tDepartment [{itemName}] successfuly removed from {args[0]}\n\t\t\t{message}";
                    return true;
                }
                report = $"\t\tError when removing [{itemName}] from {args[0]}\n\t\t\t{message}";
                return false;
            }
        }



        public enum ManagementCommands
        {
            AddDepartment,
            AddItem,
            ChangeDepartmentName,
            ChangeItemCost,
            Remove
        }
        public Dictionary<ManagementCommands, string> CommandStrings = new Dictionary<ManagementCommands, string>()
        {
            { ManagementCommands.AddDepartment, "add_dep" },
            { ManagementCommands.AddItem, "add_item" },
            { ManagementCommands.ChangeDepartmentName, "change_dep_name" },
            { ManagementCommands.ChangeItemCost, "change_item_price" },
            { ManagementCommands.Remove, "rm" }
        };
        public override string? ToString()
        {
            return $"Store name: {_name}.\nStore address: {_address}.";
        }
    }
}

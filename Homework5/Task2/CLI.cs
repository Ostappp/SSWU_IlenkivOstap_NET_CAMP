using Homework5.Task2.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Homework5.Task2
{
    internal class CLI<T> where T : Person
    {
        T _person;
        Permissions _personsPermisions;
        UserMode _mode = UserMode.Idle;

        const string MANUAL_CUSTOMER = @"Buy item: Buy <item-name> <count>
[NOTE] <count> is integer type.

Move in depertment deeper: MoveIn <path>

Move out from department: MoveOut

Leave the store: Leave
[NOTE] To leave the store you must leave all nested departments at first

Show items you bought: ShowItems

Show departments you can visit: GetWays

Show items you can buy in department: SeeItemsToBuy";

        const string CLI_MANUAL = @"Close CLI: close

Enter in manager's mode: manager
[Note] to enter this mode you must have required permissions and to be in idle mode

Enter customer's mode: customer
[Note] to enter this mode you must have required permissions and to be in idle mode

Exit from current mode to Idle state: exit

Show which mode is active: status";
        readonly Dictionary<Commands, string> command_Keyword = new Dictionary<Commands, string>
        {
            {Commands.CloseCLI,"close"},
            {Commands.EnterInManagerMode,"manager"},
            {Commands.EnterInCustomerMode,"customer"},
            {Commands.ExitToIdleMode,"exit"},
            {Commands.HasActiveMode,"status"},
        };
        public CLI(T person)
        {
            _person = person;
            EnambePermisions(person);


        }
        void EnambePermisions(T person)
        {
            if (person is IManager)
            {
                if (person is ICustomer)
                    _personsPermisions = Permissions.Both;
                else
                    _personsPermisions = Permissions.Manager;
            }
            else if (person is ICustomer)
                _personsPermisions = Permissions.Customer;
            else
                _personsPermisions = Permissions.None;
        }
        public void OpenCLI()
        {
            string inputCommand = "~~~~~~~~~~~~~~~CLI is activated~~~~~~~~~~~~~~~";
            Console.WriteLine("\n\n\n" + inputCommand.PadLeft((Console.WindowWidth / 2) + (inputCommand.Length / 2)) + "\n\n\n");
            Console.WriteLine(CLI_MANUAL + "\n\n\n");
            Console.Write("# ");
            inputCommand = Console.ReadLine();
            while (inputCommand != command_Keyword[Commands.CloseCLI])
            {

                if (command_Keyword.ContainsValue(inputCommand))
                {
                    if (inputCommand == command_Keyword[Commands.EnterInManagerMode])
                    {
                        if (_mode == UserMode.Manager)
                        {
                            Console.WriteLine("[NOTE]\tCLI is already working in manager mode");
                        }
                        else if (_mode != UserMode.Idle)
                        {
                            Console.WriteLine("[WARNING]\tExit your current CLI mode first");
                        }
                        else
                        {
                            if (_personsPermisions == Permissions.Both || _personsPermisions == Permissions.Manager)
                            {
                                _mode = UserMode.Manager;
                                string info = "~~~~~~~~~~~~~~~MANUAL~~~~~~~~~~~~~~~\n";
                                Console.WriteLine(info.PadLeft((Console.WindowWidth / 2) + (info.Length / 2)));
                                Console.WriteLine("\n"+(_person as IManager).Manual + "\n\n\n");
                                Console.WriteLine("[NOTE]\tCLI is working in manager mode");
                                Console.Write("$ ");
                            }
                            else
                            {
                                Console.WriteLine("You have not manager's permissions");
                            }
                        }
                    }
                    else if (inputCommand == command_Keyword[Commands.EnterInCustomerMode])
                    {
                        if (_mode == UserMode.Customer)
                        {
                            Console.WriteLine("[NOTE]\tCLI is already working in customer mode");
                        }
                        else if (_mode != UserMode.Idle)
                        {
                            Console.WriteLine("[WARNING]\tExit your current CLI mode first");
                        }
                        else
                        {
                            if (_personsPermisions == Permissions.Both || _personsPermisions == Permissions.Customer)
                            {
                                _mode = UserMode.Customer;
                                string info = "~~~~~~~~~~~~~~~MANUAL~~~~~~~~~~~~~~~\n";
                                Console.WriteLine(info.PadLeft((Console.WindowWidth / 2) + (info.Length / 2)));
                                Console.WriteLine("\n" + MANUAL_CUSTOMER + "\n\n\n");
                                Console.WriteLine("[NOTE]\tCLI is working in customer mode");
                                Console.Write((_person as ICustomer).AttendedDepartment + "> ");
                            }
                            else
                            {
                                Console.WriteLine("You have not customer's permissions");
                            }
                        }
                    }
                    else if (inputCommand == command_Keyword[Commands.ExitToIdleMode])
                    {
                        if (_mode == UserMode.Idle)
                        {
                            Console.WriteLine("[NOTE]\tCLI is already working in idle mode");
                        }
                        else
                        {
                            _mode = UserMode.Idle;
                            Console.WriteLine("[NOTE]\tCLI is working in idle mode");
                        }
                        Console.Write("# ");
                    }
                    else
                    {
                        Console.WriteLine($"[INFO]\tCLI is working in [{_mode}] mode");
                        Console.Write("# ");
                    }

                }
                else
                {
                    if (_mode == UserMode.Customer)
                    {
                        CustomerMode(inputCommand);
                        Console.Write((_person as ICustomer).AttendedDepartment + "> ");
                    }
                    else if (_mode == UserMode.Manager)
                    {
                        ManagerMode(inputCommand);
                        Console.Write("$ ");
                    }
                    else
                    {
                        Console.WriteLine("Enter to any CLI mode to properly execute commands");
                        Console.Write("# ");
                    }
                }

                inputCommand = Console.ReadLine();
            }
            CloseCLI();
        }
        void CloseCLI()
        {
            string message = "~~~~~~~~~~~~~~~CLI is deactivated~~~~~~~~~~~~~~~";
            Console.WriteLine("\n\n\n" + message.PadLeft((Console.WindowWidth / 2) + (message.Length / 2)) + "\n\n\n");
        }
        void CustomerMode(string command)
        {
            string action = command.Split(' ')[0].ToLower();
            string commandPart = string.Empty;
            if (command.Split(' ').Length > 1)
                commandPart = command.Split(' ', 2)[1];
            string report = string.Empty;
            ICustomer customer = _person as ICustomer;

            if (action == ICustomer.Actions.SeeItemsToBuy.ToString().ToLower())
            {
                Console.WriteLine(customer.ShowItemsToBuy());
                return;
            }
            if (action == ICustomer.Actions.GetWays.ToString().ToLower())
            {
                Console.WriteLine(customer.ShowWays());
                return;
            }
            if (action == ICustomer.Actions.MoveIn.ToString().ToLower())
            {
                customer.MoveIn(commandPart, out report);
                Console.WriteLine(report);
                return;
            }
            if (action == ICustomer.Actions.MoveOut.ToString().ToLower())
            {
                customer.MoveOut(out report);
                Console.WriteLine(report);
                return;
            }
            if (action == ICustomer.Actions.ShowItems.ToString().ToLower())
            {
                Console.WriteLine(customer.ShowGods());
                return;
            }
            if (action == ICustomer.Actions.Leave.ToString().ToLower())
            {
                customer.Leave(out report);
                Console.WriteLine(report);
                return;
            }
            if (action == ICustomer.Actions.Buy.ToString().ToLower())
            {
                if (int.TryParse(commandPart.Split(' ')[1], out int count))
                    customer.Buy(commandPart.Split(' ')[0], count, out report);
                else
                    report = $"Inavlid value is not integer: [{commandPart.Split(' ')[1]}]";
                Console.WriteLine(report);
                return;
            }

            Console.WriteLine($"Uknnown command: [{command}]");
        }
        void ManagerMode(string command)
        {
            IManager manager = _person as IManager;
            manager.ManageObject(manager.ManagedObj, command, out string report);
            Console.WriteLine("Results:\n" + report);
        }
        enum Permissions
        {
            None,
            Manager,
            Customer,
            Both,
        }
        enum Commands
        {
            EnterInCustomerMode,
            EnterInManagerMode,
            ExitToIdleMode,
            CloseCLI,
            HasActiveMode,
        }
        enum UserMode
        {
            Idle,
            Manager,
            Customer,
        }
    }
}

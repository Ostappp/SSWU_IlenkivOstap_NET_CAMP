using Homework5.Task2.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Homework5.Task2
{
    internal class CLI<T> where T : Person
    {
        T _person;
        Permissions _personsPermisions;
        UserMode _mode = UserMode.Idle;
        
        const string MANUAL_COSTUMER = @"Buy item: Buy <item-name> <count>
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

Enter costumer's mode: costumer
[Note] to enter this mode you must have required permissions and to be in idle mode

Exit from current mode to Idle state: exit

Show which mode is active: status";
        readonly Dictionary<Commands, string> command_Keyword = new Dictionary<Commands, string>
        {
            {Commands.CloseCLI,"close"},
            {Commands.EnterInManagerMode,"manager"},
            {Commands.EnterInCostumerMode,"costumer"},
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
                if (person is ICostumer)
                    _personsPermisions = Permissions.Both;
                else
                    _personsPermisions = Permissions.Manager;
            }
            else if (person is ICostumer)
                _personsPermisions = Permissions.Costumer;
            _personsPermisions = Permissions.None;
        }
        public void OpenCLI()
        {
            string inputCommand = "~~~~~~~~~~~~~~~CLI is activated~~~~~~~~~~~~~~~";
            Console.WriteLine("\n\n\n" + inputCommand.PadRight((Console.WindowWidth / 2) + (inputCommand.Length / 2)) + "\n\n\n");
            Console.WriteLine(CLI_MANUAL + "\n\n\n");
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
                                Console.WriteLine((_person as IManager).Manual+"\n");
                                Console.WriteLine("[NOTE]\tCLI is working in manager mode");
                            }
                            else
                            {
                                Console.WriteLine("You have not manager's permissions");
                            }
                        }
                    }
                    else if (inputCommand == command_Keyword[Commands.EnterInCostumerMode])
                    {
                        if (_mode == UserMode.Costumer)
                        {
                            Console.WriteLine("[NOTE]\tCLI is already working in costumer mode");
                        }
                        else if (_mode != UserMode.Idle)
                        {
                            Console.WriteLine("[WARNING]\tExit your current CLI mode first");
                        }
                        else
                        {
                            if (_personsPermisions == Permissions.Both || _personsPermisions == Permissions.Costumer)
                            {
                                _mode = UserMode.Costumer;
                                Console.WriteLine(MANUAL_COSTUMER + "\n");
                                Console.WriteLine("[NOTE]\tCLI is working in costumer mode");
                            }
                            else
                            {
                                Console.WriteLine("You have not costumer's permissions");
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
                    }
                    else
                    {
                        Console.WriteLine($"[INFO]\tCLI is working in [{_mode}] mode");
                    }

                }
                else
                {
                    if (_mode == UserMode.Costumer)
                    {
                        CustomerMode(inputCommand);
                    }
                    else if (_mode == UserMode.Manager)
                    {
                        ManagerMode(inputCommand);
                    }
                    else
                    {
                        Console.WriteLine("Enter to any CLI mode to properly execute commands");
                    }
                }

                inputCommand = Console.ReadLine();
            }
            CloseCLI();
        }
        void CloseCLI()
        {
            string message = "~~~~~~~~~~~~~~~CLI is deactivated~~~~~~~~~~~~~~~";
            Console.WriteLine("\n\n\n" + message.PadRight((Console.WindowWidth / 2) + (message.Length / 2)) + "\n\n\n");
        }
        void CustomerMode(string command)
        {
            string action = command.Split(' ')[0];
            string commandPart = command.Split(' ')[1];
            string report = string.Empty;
            ICostumer costumer = _person as ICostumer;
            
            if (action == ICostumer.Actions.SeeItemsToBuy.ToString())
            {
                Console.WriteLine(costumer.ShowItemsToBuy());
                return;
            }
            if(action == ICostumer.Actions.GetWays.ToString())
            {
                Console.WriteLine(costumer.ShowWays());
                return;
            }
            if(action == ICostumer.Actions.MoveIn.ToString())
            {
                costumer.MoveIn(commandPart, out report);
                Console.WriteLine(report);
                return;
            }
            if(action == ICostumer.Actions.MoveOut.ToString())
            {
                costumer.MoveOut(out report);
                Console.WriteLine(report);
                return;
            }
            if(action == ICostumer.Actions.ShowItems.ToString())
            {
                Console.WriteLine(costumer.ShowGods());
                return;
            }
            if(action == ICostumer.Actions.Leave.ToString())
            {
                costumer.Leave(out report);
                Console.WriteLine(report);
                return;
            }
            if(action == ICostumer.Actions.Buy.ToString())
            {
                if (int.TryParse(commandPart.Split(' ')[1], out int count))
                    costumer.Buy(commandPart.Split(' ')[0], count, out report);
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
            Costumer,
            Both,
        }
        enum Commands
        {
            EnterInCostumerMode,
            EnterInManagerMode,
            ExitToIdleMode,
            CloseCLI,
            HasActiveMode,
        }
        enum UserMode
        {
            Idle,
            Manager,
            Costumer,
        }
    }
}

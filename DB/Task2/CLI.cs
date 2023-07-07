using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class CLI
    {
        const string INFO = "asd";
        readonly Dictionary<Commands, string> command_Keyword = new Dictionary<Commands, string>
        {
            {Commands.CloseCLI,"close"},
            {Commands.Create,"create"},
            {Commands.Read,"read"},
            {Commands.Update,"update"},
            {Commands.Delete,"delete"},
            {Commands.Info,"info"},
        };
        public void Start()
        {
            string inputCommand = "~~~~~~~~~~~~~~~CLI is activated~~~~~~~~~~~~~~~";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\n" + inputCommand.PadLeft((Console.WindowWidth / 2) + (inputCommand.Length / 2)) + "\n\n\n");
            Info();
            Console.Write("> ");
            inputCommand = Console.ReadLine();
            while (inputCommand != command_Keyword[Commands.CloseCLI])
            {
                if (command_Keyword.ContainsValue(inputCommand))
                {
                    DefineCommand(inputCommand);

                    Console.Write("> ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid command! Type <INFO> to get info about available commands");
                    Console.ResetColor();
                    Console.Write("> ");
                }

                inputCommand = Console.ReadLine();
            }
            CloseCLI();
        }
        void CloseCLI()
        {
            string message = "~~~~~~~~~~~~~~~CLI is deactivated~~~~~~~~~~~~~~~";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n\n" + message.PadLeft((Console.WindowWidth / 2) + (message.Length / 2)) + "\n\n\n");
            Console.ResetColor();
        }
        void Info()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(INFO + "\n\n\n");
            Console.ResetColor();
        }
        void DefineCommand(string command)
        {

        }
        void Create()
        {

        }
        void Read()
        {

        }
        void Update()
        {

        }
        void Delete()
        {

        }
        enum Commands
        {
            Create,
            Read,
            Update,
            Delete,
            CloseCLI,
            Info
        }
    }
}

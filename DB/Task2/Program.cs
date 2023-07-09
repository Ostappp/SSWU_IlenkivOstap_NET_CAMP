using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DBAdapter adapter = new DBAdapter();
            //adapter.RefillComputerStoreDB();
            CLI CLI = new CLI(adapter);
            CLI.OpenCLI();
        }
    }
}

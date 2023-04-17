using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5.Task2.Interfaces
{
    internal interface IManagable
    {
        public bool Execute(IManager manager, string command, out string report);
        public string Manual { get; }
    }
}

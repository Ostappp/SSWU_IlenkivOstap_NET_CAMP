using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10.Task2
{
    internal interface IBuyer
    {
        decimal Buy(ElectronicDevice device);
        decimal Buy(Product product);
    }
}

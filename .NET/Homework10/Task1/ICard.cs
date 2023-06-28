using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10.Task1
{
    internal interface ICard
    {
        public CardType CardName { get; }
        public HashSet<string> FirstNumbers { get; }
        public string Number { get; }
        public HashSet<int> TotalNumCount { get; }
        public enum CardType
        {
            Unknown,
            AmericanExpress,
            MasterCard,
            Visa,
        }
    }
}

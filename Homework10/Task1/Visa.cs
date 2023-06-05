using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10.Task1
{
    internal class Visa : ICard
    {
        private string _number;
        public ICard.CardType CardName { get => ICard.CardType.Visa; }
        public HashSet<string> FirstNumbers { get => new HashSet<string> { "4" }; }
        public string Number { get => _number; }
        public HashSet<int> TotalNumCount { get => new HashSet<int> { 13, 16 }; }

        public Visa(string number)
        {
            number = number.Replace(" ", "");
            if (string.IsNullOrEmpty(number))
                throw new ArgumentNullException(nameof(number));
            bool isCorrectStart = false;
            foreach (var item in FirstNumbers)
            {
                if (number.StartsWith(item))
                    isCorrectStart = true;
            }
            if (!isCorrectStart)
                throw new ArgumentException($"Card number must starts with one of following: {string.Join(", ", FirstNumbers)}");

            if (!TotalNumCount.Contains(number.Length))
                throw new ArgumentException($"Card number must conntain one of following number count: {string.Join(", ", TotalNumCount)}");
            if (!number.ToList().TrueForAll(char.IsDigit))
                throw new ArgumentException("Card number must conntain only numbers");

            _number = new(number);
        }
    }
}

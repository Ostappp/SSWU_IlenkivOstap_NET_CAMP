﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10.Task1
{
    internal class MasterCard : ICard
    {
        private string _number;
        public ICard.CardType CardName { get => ICard.CardType.MasterCard; }
        public HashSet<string> FirstNumbers { get => new HashSet<string> { "51", "52", "53", "54", "55" }; }
        public string Number { get => _number; }
        public HashSet<int> TotalNumCount { get => new HashSet<int> { 16 }; }

        public MasterCard(string number)
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

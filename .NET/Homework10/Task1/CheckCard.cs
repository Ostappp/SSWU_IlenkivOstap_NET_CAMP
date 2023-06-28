using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Homework10.Task1.ICard;

namespace Homework10.Task1
{
    internal class CheckCard
    {
        Dictionary<CardType, (HashSet<string>, HashSet<int>)> _cardsCoreInfo;
        public CheckCard(IEnumerable<ICard> cardTemplates)
        {
            if (!cardTemplates.Any())
                throw new ArgumentNullException(nameof(cardTemplates));
            _cardsCoreInfo = new Dictionary<CardType, (HashSet<string>, HashSet<int>)>();
            foreach (var card in cardTemplates)
            {
                if (!_cardsCoreInfo.ContainsKey(card.CardName))
                {
                    if (card.CardName != CardType.Unknown)
                        _cardsCoreInfo.Add(card.CardName, (card.FirstNumbers, card.TotalNumCount));
                }
            }
        }
        public void AddCardType(ICard cardTemplate)
        {
            if (!_cardsCoreInfo.ContainsKey(cardTemplate.CardName))
            {
                if (cardTemplate.CardName != CardType.Unknown)
                    _cardsCoreInfo.Add(cardTemplate.CardName, (cardTemplate.FirstNumbers, cardTemplate.TotalNumCount));
            }
        }
        public bool GetCardType(string number, out CardType cardType)
        {
            cardType = CardType.Unknown;
            if (string.IsNullOrEmpty(number))
                return false;
            number = number.Replace(" ", "");
            if (Luna(number))
            {
                foreach (var cardBaseInfo in _cardsCoreInfo)
                {
                    if (cardBaseInfo.Value.Item2.Contains(number.Length))
                    {
                        foreach (var numberStarts in cardBaseInfo.Value.Item1)
                        {
                            bool hasSameStart = true;
                            for (int i = 0; i < numberStarts.Length; i++)
                            {
                                if (number[i] != numberStarts[i])
                                    hasSameStart = false;
                            }
                            if (hasSameStart)
                            {
                                cardType = cardBaseInfo.Key;
                                return true;
                            }
                        }
                    }
                }
                return true;
            }

            return false;
        }
        public bool Luna(string number)
        {
            number = number.Replace(" ", "");
            if (string.IsNullOrEmpty(number))
                throw new ArgumentNullException(nameof(number));
            if (!number.ToList().TrueForAll(char.IsDigit))
                throw new ArgumentException("card number must contain only digits");

            List<int> skipOne = new List<int>();
            for (int i = 0; i < number.Length; i += 2)
            {
                skipOne.Add(int.Parse(number[i].ToString()));
            }
            skipOne = skipOne.Select(i => i * 2).ToList();
            while (skipOne.Any(d => d.ToString().Length > 1))
            {
                for (int i = 0; i < skipOne.Count; i++)
                {
                    if (skipOne[i].ToString().Length > 1)
                        skipOne[i] = skipOne[i].ToString().ToList().Select(x => int.Parse(x.ToString())).Sum();
                }
            }
            string endSeq = string.Empty;
            for (int i = 1, j = 0; i < number.Length; i += 2, j++)
            {
                endSeq += skipOne[j].ToString();
                endSeq += number[i];
            }
            if (endSeq.Select(i => int.Parse(i.ToString())).Sum() % 10 == 0)
                return true;
            return false;
        }
    }
}

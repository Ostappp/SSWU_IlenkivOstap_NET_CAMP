using Homework10.Task1;
using Homework10.Task2;

namespace Homework10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //task 1
            AmericanExpress a = new AmericanExpress("378282246310005");
            CheckCard checkCard = new CheckCard(new ICard[] { a, new Visa("4000000000000"), new MasterCard("5100000000000000") });
            Console.WriteLine(checkCard.GetCardType("4003789100205381", out ICard.CardType cardType) + "\t" + cardType + "\n" + checkCard.Luna("4003789100205381"));
            

            //task2
            ElectronicDevice phone1 = new ElectronicDevice("phone1", 32500, (12, 8, 2), 0.12f);
            ElectronicDevice TV = new ElectronicDevice("TV", 165000, (1500, 1000, 30), 10);
            Product banana = new Product("banana", 12, (12, 8, 3), 0.1f, Product.Durability.Medium);
            Product yogurt = new Product("yogurt", 45, (5, 5, 10), 0.045f, Product.Durability.High);
            Product milk = new Product("milk 1l", 50, (20, 8, 8), 0.9f, Product.Durability.Low);
            
            Shop shop = new Shop("shopA", new List<IOffer>() { phone1, TV, banana, yogurt, milk });
           
            ShippingCost shippingCost = new ShippingCost(100, (30, 30, 30));
            
            Console.WriteLine($"\n\nPrices of shipping items from shop <{shop.Name}>");
            foreach (var item in shop.GetProductList)
            {
                Console.WriteLine($"'{item.Name}' costs: {shippingCost.Buy(item)}");
            }
        }
    }
}
using Homework9.KitchenData;
using Homework9.KitchenData.Staff;
using Homework9.MenuElements;

namespace Homework9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Kitchen kitchen = new Kitchen();
            kitchen.StartWork();
            kitchen.ReadyOrder += Print;
            Menu menu = new Menu();
            Dictionary<IOffer, uint> orderContent = new Dictionary<IOffer, uint>()
            {
                { menu.MenuOffer[0],1 },
                { menu.MenuOffer[2],1 },
                { menu.MenuOffer[1],1 },
            };
            Order order = new Order(0, orderContent);
            kitchen.Order(order); 
            Order order1 = new Order(1, new Dictionary<IOffer, uint>() { { menu.MenuOffer[3], 1 } });
            kitchen.Order(order1);
            Console.ReadLine();
        }
        static void Print(Order order)
        {
            Console.WriteLine(order);
        }
    }
}
using System.Text;

namespace Homework9.MenuElements
{
    internal class Menu
    {
        List<IOffer> _menuOffer;
        public List<IOffer> MenuOffer { get => new List<IOffer>(_menuOffer); }
        public Menu(IEnumerable<IOffer> menuOffer)
        {
            if(!menuOffer.Any()) 
                throw new ArgumentNullException();

            _menuOffer = new(menuOffer);
        }
        public Menu()
        {
            _menuOffer = new()
            {
                new Pizza("4 Meat","4 Different meat sorts", Pizza.PizzaSize.Large),
                new Pizza("4 Cheeses","4 Different meat sorts", Pizza.PizzaSize.Large),
                new Juice("Orange juice", new TimeOnly(0,1),"very taste juice"),
                new IceCream("Chocolate ice-cream", new TimeOnly(0,2),"ice-cream with chocolate")
            };
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Menu offer:");
            foreach (var offer in _menuOffer)
            {
                sb.Append("\n"+offer);
            }
            return sb.ToString();
        }
        public enum OfferType
        {
            None,
            Drink,
            Pizza,
            Dessert,
            OtherDish
        }
    }
}

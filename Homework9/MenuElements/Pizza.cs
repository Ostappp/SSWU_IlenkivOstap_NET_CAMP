namespace Homework9.MenuElements
{
    internal class Pizza : IDish
    {
        private readonly string _name;
        public string Name { get { return _name; } }

        private TimeOnly _prepearingTime;
        public TimeOnly PrepearingTime { get => _prepearingTime; }

        private string _description;
        public string Description { get => _description; }
        public Menu.OfferType OrderType { get => Menu.OfferType.Pizza; }

        private decimal _price;
        public decimal Price { get => _price; }

        public PizzaSize Size { get; private set; }

        private Dictionary<PizzaIngredients, uint> _ingredientsMeasuring;
        public Dictionary<PizzaIngredients, uint> IngredientsMeasuring { get => new(_ingredientsMeasuring); }

        public Pizza(string name, TimeOnly preparationTime, string description, decimal price, PizzaSize size, Dictionary<PizzaIngredients, uint> ingredientsMeasuring)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name can not be empty!");

            if (price <= 0)
                throw new ArgumentException("Price can not be negative!");

            if (!ingredientsMeasuring.Any())
                throw new ArgumentNullException("Ingredients measuring can not be empty!");

            _name = name;

            if (string.IsNullOrEmpty(description))
                _description = string.Empty;
            else
                _description = description;

            _prepearingTime = new TimeOnly(preparationTime.Hour, preparationTime.Minute, preparationTime.Second);
            _price = price;
            Size = size;
            _ingredientsMeasuring = ingredientsMeasuring;
        }

        public enum PizzaIngredients
        {

        }
        public enum PizzaSize
        {
            Small,
            Medium,
            Large,
            ExtraLarge
        }
    }
}

namespace Homework10.Task2
{
    internal class Product:IOffer
    {
        string _name;
        public string Name { get => _name; }
        double _price;
        public double Price { get => _price; }
        private (uint, uint, uint) _size;
        public (uint, uint, uint) Size { get => _size; }
        private float _wight;
        public float Wight { get => _wight; }
        private Durability _durability;
        public Durability GetDurability { get => _durability; }
        public Product(string name, double price, (uint, uint, uint) size, float weight, Durability durability)
        {
            if (weight <= 0)
                throw new ArgumentException("Weight can not be less or equal to zero.");
            if (size.Item1 == 0 || size.Item2 == 0 || size.Item3 == 0)
                throw new ArgumentException("Size can not contain zero value.");
            _name = name;
            _price = price;
            _size = size;
            _wight = weight;
            _durability = durability;
        }
        public Product(Product original)
        {
            _name = original.Name;
            _price = original.Price;
            _size = original.Size;
            _wight = original.Wight; 
            _durability = original.GetDurability;
        }

        public enum Durability
        {
            Low,
            Medium,
            High,
        }

        public void Accept(IBuyer buyer)
        {
            buyer.Buy(this);
        }
    }
}

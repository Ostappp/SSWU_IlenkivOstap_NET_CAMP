namespace Homework10.Task2
{
    internal class Shop
    {
        string _name;
        public string Name { get => _name; }
        List<IOffer> _products;
        public List<IOffer> GetProductList { get => new(_products); }
        public Shop(string name, IEnumerable<IOffer> products)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            _name = name;
            _products = new(products);
        }
    }
}

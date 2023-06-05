namespace Homework10.Task2
{
    internal class ElectronicDevice : IOffer
    {
        private string _name;
        public string Name { get => _name; }
        private double _price;
        public double Price { get => _price; }
        private (uint, uint, uint) _size;
        public (uint, uint, uint) Size { get => _size; }
        private float _wight;
        public float Wight { get => _wight; }
        public ElectronicDevice(string name, double price, (uint, uint, uint) size, float weight)
        {
            if (weight <= 0)
                throw new ArgumentException("Weight can not be less or equal to zero.");
            if (size.Item1 == 0 || size.Item2 == 0 || size.Item3 == 0)
                throw new ArgumentException("Size can not contain zero value.");
            _name = name;
            _price = price;
            _size = size;
            _wight = weight;
        }
        public ElectronicDevice(ElectronicDevice original)
        {
            _name = original.Name;
            _price = original.Price;
            _size = original.Size;
            _wight = original.Wight;
        }
        public void Accept(IBuyer buyer)
        {
            buyer.Buy(this);
        }
    }
}

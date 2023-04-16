namespace Homework5.Task2
{
    internal class Item: ObjectWithSize
    {
        readonly string _name;        
        public string Name { get => _name; }

        public Item(string name, double sizeX, double sizeY, double sizeZ):base((sizeX, sizeY, sizeZ))
        {
            _name = name;
        }
    }
}

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
        public Item(string name, string args):base(args)
        {
            _name = name;
        }
        public Item(Item original) : base(original.Size)
        {
            _name = original.Name;
        }
        public override string? ToString()
        {
            return $"Name: {Name}. Params: {base.ToString()}";
        }
    }
}

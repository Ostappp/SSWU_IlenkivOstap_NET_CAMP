namespace Homework5.Task2
{
    internal class PackedItems : ObjectWithSize
    {
        readonly string _label;
        readonly List<ObjectWithSize> _items;
        public string GetLabel { get => _label; }
        public PackedItems(string label, List<ObjectWithSize> item) : base(GetPackedItemsSize(item))
        {
            _label = label;
            _items = item;
        }
        //Розмір такої коробки визначити з умови, що всі товари розташовуються в один ряд по висоті.
        //Якщо правильно розумію, предмети в коробці знаходяться один біля одного по довжині та ширилі (аля матриця)
        //і не можна викладати один на одного зверху.
        static (double, double, double) GetPackedItemsSize(List<ObjectWithSize> items)
        {
            return (items.MaxBy(i => i.Size.Item1).Size.Item1, items.MaxBy(i => i.Size.Item2).Size.Item2, items.Sum(i => i.Size.Item3));
        }
    }
}

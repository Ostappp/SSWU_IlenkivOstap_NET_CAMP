namespace Homework5.Task2
{
    internal class Box<T> : ObjectWithSize where T : ObjectWithSize
    {
        readonly string _label;
        readonly List<T> _items;
        public string GetLabel { get => _label; }
        public Box(string label, List<T> item) : base(GetPackedItemsSize(item))
        {
            _label = label;
            _items = item;
        }
        //Розмір такої коробки визначити з умови, що всі товари розташовуються в один ряд по висоті.
        //Якщо правильно розумію, предмети в коробці знаходяться один біля одного по довжині та ширилі (аля матриця)
        //і не можна викладати один на одного зверху.
        static (double, double, double) GetPackedItemsSize(List<T> item)
        {
            int countInRow = (int)Math.Round(Math.Sqrt(item.Count * item.Count));
            int countInCol = item.Count % countInRow;
            return (item[0].Size.Item1 * countInRow, item[0].Size.Item2, item[0].Size.Item3 * countInCol);
        }
    }
}

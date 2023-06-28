namespace Homework10.Task2
{
    internal interface IOffer
    {
        string Name { get; }
        double Price { get; }
        (uint, uint, uint) Size { get ; }
        float Wight { get; }
        void Accept(IBuyer buyer);
    }
}

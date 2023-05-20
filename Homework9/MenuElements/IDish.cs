namespace Homework9.MenuElements
{
    internal interface IDish : IOffer
    {
        public TimeOnly PrepearingTime { get; }
    }
}

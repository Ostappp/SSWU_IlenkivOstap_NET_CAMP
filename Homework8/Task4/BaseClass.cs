namespace Homework8.Task4
{
    internal class BaseClass
    {
        public delegate string Task4Delegate();
        protected virtual event Task4Delegate Event;
        public BaseClass()
        {
            Event += Execution;
        }
        string Execution()
        {
            return $"{GetType().Name}\ttime: {DateTime.Now}";
        }
    }
}

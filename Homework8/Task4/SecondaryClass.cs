namespace Homework8.Task4
{
    internal class SecondaryClass : BaseClass
    {
        string name;
        public SecondaryClass(string name)
        {
            this.name = name;
            //можна підписуватись та відписуватись
            Event += Execution1;
        }
        /// <summary>
        /// не можна викликати у лдочірніх класах івенти
        /// </summary>
        //public void InvokeDelegate()
        //{
        //    Delegate();
        //}
        string Execution1()
        {
            return $"{GetType().Name}\ttime: {DateTime.Now}";
        }
    }
}
